using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Net.Toodledo
{
    public class TaskDataService
    {
        private string _key;
        public TaskDataService(string key)
        {
            _key = key;
        }

        public IEnumerable<Task> GetTasksForFolder(int folderId)
        {
            return LoadTasks(string.Format("{0}?method=getTasks;key={1};Folder={2}", Session.TOODLEDO_URL, _key, folderId));
        }

        public IEnumerable<Task> GetTasksForContext(int contextId)
        {
             return LoadTasks(string.Format("{0}?method=getTasks;key={1};Context={2}", Session.TOODLEDO_URL, _key, contextId));
        }

        public XDocument GetTaskDocument(string url)
        {
            return XDocument.Load(url);
        }

        public IEnumerable<Task> LoadTasks(string url)
        {
            return LoadTasks(GetTaskDocument(url));
        }
        public IEnumerable<Task> GetTasks()
        {
            IEnumerable<Task> tasks;
            //XDocument loaded = XDocument.Load(Session.TOODLEDO_URL + "?method=getTasks;key=" + _key);
            tasks=LoadTasks(Session.TOODLEDO_URL + "?method=getTasks;key=" + _key);
            // Query the data and write out a subset of contacts
            //tasks = from c in loaded.Descendants("task")
            //        select new Task
            //        {
            //            ID = (int)c.Element("id"),
            //            ParentID=(int?) c.Element("parent")?? 0,
            //            Children = (int)c.Element("children"),
            //            Title = (string)c.Element("title"),
            //            Tag = (string)c.Element("tag"),
            //            FolderID = (int)c.Element("folder"),
            //            Repeat = (int)c.Element("repeat"),
            //            Length = (int)c.Element("length"),
            //            Priority = (int)c.Element("priority"),
            //            Note = (string)c.Element("note"),
            //            Timer = (int)c.Element("timer"),
            //            Added = DateTime.Parse(c.Element("added").Value),
            //            Modified = DateTime.Parse(c.Element("modified").Value),
            //            Completed = parseTime(c.Element("completed").Value),
            //            Due = parseTime(c.Element("duedate") + " " + c.Element("duetime")),
            //            ContextID = (int)c.Element("context").Attribute("id"),
            //            GoalID = (int)c.Element("goal").Attribute("id"),
            //        };                
                return tasks;     
        }

        public IEnumerable<Task>  LoadTasks(XDocument taskDoc)  
        {
            IEnumerable<Task> tasks;
            tasks = from c in taskDoc.Descendants("task")
                select new Task
                {
                    ID = (int)c.Element("id"),
                    ParentID = (int?)c.Element("parent") ?? 0,
                    Children = (int)c.Element("children"),
                    Title = (string)c.Element("title"),
                    Tag = (string)c.Element("tag"),
                    FolderID = (int)c.Element("folder"),
                    Repeat = (int)c.Element("repeat"),
                    Length = (int)c.Element("length"),
                    Priority = (int)c.Element("priority"),
                    Note = (string)c.Element("note"),
                    Timer = (int)c.Element("timer"),
                    Added = DateTime.Parse(c.Element("added").Value),
                    Modified = DateTime.Parse(c.Element("modified").Value),
                    Completed = parseTime(c.Element("completed").Value),
                    Due = parseTime(c.Element("duedate") + " " + c.Element("duetime")),
                    ContextID = (int)c.Element("context").Attribute("id"),
                    GoalID = (int)c.Element("goal").Attribute("id"),
                };
            return tasks;     

        }
        public int AddTask(string title)
        {
            return AddTask(title, null, 0, 0, 0, 0, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, 0, null, 0, 0, 0, false, null);
        }


        public int AddTask(string title, IEnumerable<KeyValuePair<string, string>> parameterDictionary)
        {
       
            string apiCall = string.Format("?method=addTask;key={0};title={1};", _key,title);
            StringBuilder sb = new StringBuilder(apiCall);
            foreach (var kvp in parameterDictionary)
            {
                sb.AppendFormat("{0}={1};",kvp.Key,kvp.Value);
            }
            XDocument loaded = XDocument.Load(Session.TOODLEDO_URL + sb.ToString());
            int newId;
            newId = (int)loaded.Element("added");
            return newId;
        }

        public int AddTask(string title, int folder, int context)
        {
            return AddTask(title, null,folder, context, 0, 0, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, 0, null, 0, 0, 0, false, null);
        }
               
        public int AddTask(string title, string tag, int folder, int context, int goal, int parent, DateTime dueDate,
             DateTime startDate, DateTime dueTime, int repeat, string repeatAdvanced, int status, int length,
            int priority, Boolean star, string note)
        {
            var parameterList = new Dictionary<string, string>();
            if (title == null) throw new ArgumentNullException("Title cannot be null.");


            if (tag != null) parameterList.Add("tag", tag);
            if (note != null && note != string.Empty) parameterList.Add("note", note);
            if (folder != 0) parameterList.Add("folder", folder.ToString());
            if (context != 0) parameterList.Add("context", context.ToString());
            if (goal != 0) parameterList.Add("goal", goal.ToString());
            if (parent != 0) parameterList.Add("parent", parent.ToString());
            if (dueDate != DateTime.MinValue) parameterList.Add("duedate", dueDate.ToString());
            if (startDate != DateTime.MinValue) parameterList.Add("startdate", startDate.ToString());          
            int newId;
            newId = AddTask(title, parameterList);
            return newId;
        }
        DateTime parseTime(string date)
        {
            DateTime ret;
            DateTime.TryParse(date, out ret);
            return ret;
        }
    }
}
