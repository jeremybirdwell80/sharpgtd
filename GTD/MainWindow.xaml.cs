using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Net.Toodledo;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace GTD
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Session session;
        WaitCursor wait;
        BackgroundWorker worker;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Window1_Loaded);

            wait = new WaitCursor();
            contentArea.Children.Add(wait);

            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            // Some of this was derived from the example here
            //http://blogs.interknowlogy.com/johnbowen/archive/2007/06/20/20458.aspx

            // Map close command
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close,
                            new ExecutedRoutedEventHandler(delegate(object sender, ExecutedRoutedEventArgs args) { this.Close(); })));
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CollectionViewSource v = new CollectionViewSource();
            v.Source = from c in session.Tasks
                       where c.Completed == DateTime.MinValue
                       select c;
            Binding bin = new Binding();
            bin.Source = v;

            v.SortDescriptions.Add(new SortDescription("Tag", ListSortDirection.Ascending));
            v.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
            v.GroupDescriptions.Add(new PropertyGroupDescription("Tag"));
            // v.GroupDescriptions.Add(new PropertyGroupDescription("ContextID"));

            BindingOperations.SetBinding(tasks, ListBox.ItemsSourceProperty, bin);
            wait.Visibility = Visibility.Collapsed;
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var t = session.Tasks;
        }

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            DoLogin();
        }

        private void DoLogin()
        {
            Login l = new Login();
            l.Owner = this;
            l.ShowDialog();

            if (l.DialogResult.HasValue && l.DialogResult.Value == true)
            {
                session = new Session();
                session.Email = l.UserName;
                session.Password = l.Password;
                ShowNotCompleted();
            }

        }

        private void ShowNotCompleted()
        {
            wait.Visibility = Visibility.Visible;
            worker.RunWorkerAsync();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
