using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Net.Toodledo
{
    public class Task
    {
        
        public int ID {get;set;}                //  <id>1234</id>
        public int ParentID {get;set;}          //  <parent>1122</parent>
        public int Children {get;set;}          //  <children>0</children>
        public string Title {get;set;}          //  <title>Buy Milk</title>
        public string Tag  {get;set;}           //  <tag>After work</tag>
        public int FolderID { get; set;}        //  <folder>123</folder>
        public int ContextID { get; set; }      //  <context id="123">Home</context>
        public int GoalID { get; set; }         //  <goal id="123">Get a Raise</goal>
        public DateTime Added {get;set;}        //  <added>2006-01-23</added>
        public DateTime Modified  {get;set;}    // <modified>2006-01-25 05:12:45</modified>
        public DateTime Due { get; set; }       // <duedate modifier=""></duedate> <duetime>2:00pm</duetime>
        public DateTime Completed {get;set;}        //  <completed></completed>
        public int Repeat {get;set;}            //  <repeat>1</repeat>
        public int Priority {get;set;}          //  <priority>2</priority>
        public int Length {get;set;}            //  <length>20</length>
        public int Timer {get;set;}             //  <timer>0</timer>
        public string Note {get;set;}           //  <note></note>

        public override string ToString()
        {
            return Title;
        }
    }


    // GET
    // http://www.toodledo.com/api.php?method=getTasks;key=YourKey;shorter=130;longer=10;priority=2

    // Adding
    // http://www.toodledo.com/api.php?method=addTask;key=YourKey;
    // title=new years;priority=1;repeat=0;length=30;duedate=2010-01-01
    //<added>12345</added>
    
    
    // Update
    // http://www.toodledo.com/api.php?method=editTask;key=YourKey;id=12345;title=MyTask;completed=1;folder=123
    // <success>1</success>


    // Delete
    // http://www.toodledo.com/api.php?method=deleteTask;key=YourKey;id=12345;

    //public static string EncodePassword(string password)
    //    {
    //        byte[] original_bytes = System.Text.Encoding.ASCII.GetBytes(password);
    //        byte[] encoded_bytes = new MD5CryptoServiceProvider().ComputeHash(original_bytes);
    //        StringBuilder result = new StringBuilder();
    //        for (int i=0; i<encoded_bytes.Length; i ) {
    //            result.Append(encoded_bytes[i].ToString("x2"));
    //        }
    //        return result.ToString();
    //    }
}

/*
 * Getting tasks
 * 
 * 
 
    * title : A text string up to 255 characters. Boolean operators do not work yet, so your search will be for a single phrase. Please encode the & character as %26 and the ; character as %3B.
    * tag : A text string up to 64 characters. Please encode the & character as %26 and the ; character as %3B.
    * folder : The id number of the folder as returned from the "getFolders" API call. 0 is a special number that returns tasks without a set folder.
    * context : The id number of the context as returned from the "getContexts" API call. 0 is a special number that returns tasks without a set context.
    * goal : The id number of the goal as returned from the "getGoals" API call. 0 is a special number that returns tasks without a set goal.
    * priority : An integer that represents the priority.
          o -1 = Negative
          o 0 = Low
          o 1 = Medium
          o 2 = High
          o 3 = Top
    * repeat : An integer that represents how the tasks repeats.
          o 0 = No Repeat
          o 1 = Weekly
          o 2 = Monthly
          o 3 = Yearly
          o 4 = Daily
          o 5 = Biweekly
          o 6 = Bimonthly
          o 7 = Semiannually
          o 8 = Quarterly
          o 9 = With Parent
    * parent : This is used to Pro accounts that have access to subtasks. Set this to the id number of the parent task and you will get its subtasks. The default is 0, which is a special number that returns tasks that do not have a parent.
    * shorter : An integer representing minutes. This is used for finding tasks with a duration that is shorter than the specified number of minutes.
    * longer : An integer representing minutes. This is used for finding tasks with a duration that is longer than the specified number of minutes.
    * before : A date formated as YYYY-MM-DD. Used to find tasks with due-dates before this date.
    * after : A date formated as YYYY-MM-DD. Used to find tasks with due-dates after this date.
    * modbefore : A date-time formated as YYYY-MM-DD HH:MM:SS. Used to find tasks with a modified date and time before this dateand time.
    * modafter : A date-time formated as YYYY-MM-DD HH:MM:SS. Used to find tasks with a modified date and time after this dateand time.
    * compbefore : A date formated as YYYY-MM-DD. Used to find tasks with a completed date before this date.
    * compafter : A date formated as YYYY-MM-DD. Used to find tasks with a completed date after this date.
    * notcomp : Set to 1 to omit completed tasks. Omit variable, or set to 0 to retrieve both completed and uncompleted tasks.
    * id : The id number of the task that you want to fetch. This is useful if you already know the id number and only need to fetch the one task.
*/
/*
Adding Tasks

You can easily add a task to your list with the "addTask" API call. The title field is required, but all other fields are optional.

    * title : A text string up to 255 characters. Please encode the & character as %26 and the ; character as %3B.
    * tag : A text string up to 64 characters. Please encode the & character as %26 and the ; character as %3B.
    * folder : The id number of the folder as returned from the "getFolders" API call. Omit this field or set it to 0 to leave the task unassigned to a folder.
    * context : The id number of the context as returned from the "getContexts" API call. Omit this field or set it to 0 to leave the task unassigned to a context.
    * goal : The id number of the goal as returned from the "getGoals" API call. Omit this field or set it to 0 to leave the task unassigned to a goal.
    * parent : This is used to Pro accounts that have access to subtasks. To create a subtask, set this to the id number of the parent task. The default is 0, which creates a normal task.
    * duedate : A date formated as YYYY-MM-DD with an optional modifier attached to the front. Examples: "2007-01-23" , "=2007-01-23" , ">2007-01-23".
    * duetime : A date formated as HH:MM MM. Examples: 12:34 am, 4:56 pm.
    * repeat : An integer that represents how the tasks will repeat.
          o 0 = No Repeat
          o 1 = Weekly
          o 2 = Monthly
          o 3 = Yearly
          o 4 = Daily
          o 5 = Biweekly
          o 6 = Bimonthly
          o 7 = Semiannually
          o 8 = Quarterly
          o 9 = With Parent
    * length : An integer representing the number of minutes that the task will take to complete.
    * priority : An integer that represents the priority.
          o -1 = Negative
          o 0 = Low
          o 1 = Medium
          o 2 = High
          o 3 = Top
    * note : A text string. Please encode the & character as %26 and the ; character as %3B.
*/


/*

  
  
 
 */