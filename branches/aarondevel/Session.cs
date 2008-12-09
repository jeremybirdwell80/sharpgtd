using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Net.Toodledo
{
    public class Session
    {
        // Get Login Info
        
        // http://www.toodledo.com/api.php?method=getUserid;email=me@example.com;pass=12345
        // <userid>123456abcdef</userid>

        // Get Token
        // www.toodledo.com/api.php?method=getToken;userid=YourUserID
        // <token>td12345678901234</token>

        // Make key
        // key = md5( md5(password)+token+myuserid );

        string key;
        public string Key
        {
            get { if (key == null) key = md5(md5(Password) + Token + UserID); return key; }
        }
        string token;
        public string Token { get { if (token == null) token = XGetToken(); return token; } }
        string userID;
        public string UserID { get { if (userID == null) userID = XGetUserID(); return userID; } }

        string email;
        public string Email { get { return email; } set { email = value; userID = null; token = null; key = null; } }
        string password;
        public string Password { get { return password; } set { password = value; userID = null; token = null; key = null; } }

        public static string md5(string password)
        {
            byte[] original_bytes = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] encoded_bytes = new MD5CryptoServiceProvider().ComputeHash(original_bytes);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < encoded_bytes.Length; i++)
            {
                result.Append(encoded_bytes[i].ToString("x2"));
            }
            return result.ToString();
        }

        string XGetToken()
        {
            XDocument loaded = XDocument.Load(@"http://www.toodledo.com/api.php?method=getToken;userid="+UserID);
            return loaded.Element("token").Value;
            
        }
        string XGetUserID()
        {
            XDocument loaded = XDocument.Load(@"http://www.toodledo.com/api.php?method=getUserid;email="+Email+";pass=" + Password);
            return loaded.Element("userid").Value;
        }


        IEnumerable<Folder> folders;
        public IEnumerable<Folder> Folders
        {
            get
            {
                if (contexts == null)
                {
                    XDocument loaded = XDocument.Load(@"http://www.toodledo.com/api.php?method=getFolders;key=" + Key);
                    // Query the data and write out a subset of contacts
                    folders = from c in loaded.Descendants("folder")
                               select new Folder
                               {
                                   ID = (int)c.Attribute("id"),
                                   Private = (bool)c.Attribute("private"),
                                   Archived = (bool)c.Attribute("archived"),
                                   Title = (string)c.Value
                               };
                }
                return folders;

            }
        }

        IEnumerable<Task> tasks;
        public IEnumerable<Task> Tasks
        {
            get
            {
                if (tasks == null)
                {
                    XDocument loaded = XDocument.Load(@"http://www.toodledo.com/api.php?method=getTasks;key=" + Key);
                    // Query the data and write out a subset of contacts
                    tasks = from c in loaded.Descendants("task")
                               select new Task
                               {
                                   ID = (int)c.Element("id"),
                                   ParentID= (int)c.Element("parent"),
                                   Children= (int)c.Element("children"),
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
                                   GoalID= (int)c.Element("goal").Attribute("id"),
                               };

   


                }
                return tasks;

            }
        }

        DateTime parseTime(string date)
        {
            DateTime ret;
            DateTime.TryParse(date,out ret);
            return ret;
        }

        IEnumerable<Goal> goals;
        public IEnumerable<Goal> Goals
        {
            get
            {
                if (goals == null)
                {
                    XDocument loaded = XDocument.Load(@"http://www.toodledo.com/api.php?method=getGoals;key=" + Key);
                    goals = from c in loaded.Descendants("goal")
                               select new Goal
                               {
                                   ID = (int)c.Attribute("id"),
                                   Level = (int)c.Attribute("level"),
                                   ContributesID = (int)c.Attribute("contributes"),
                                   Title = (string)c.Value
                               };
                }
                return goals;

            }

        }

        IEnumerable<Context> contexts;
        public IEnumerable<Context> Contexts
        {
            get 
            {
                if (contexts == null)
                {
                    XDocument loaded = XDocument.Load(@"http://www.toodledo.com/api.php?method=getContexts;key=" + Key);
                    // Query the data and write out a subset of contacts
                    contexts = from c in loaded.Descendants("context")
                               select new Context
                               {
                                   ID = (int)c.Attribute("id"),
                                   Title = (string)c.Value
                               };
                }
                return contexts;

            }

        }
    }
}
