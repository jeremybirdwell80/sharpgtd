using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Net.Toodledo
{
    public class Folder
    {
        // Get Folder service
        // http://www.toodledo.com/api.php?method=getFolders;key=YourKey

        // Add
        // http://www.toodledo.com/api.php?method=addFolder;key=YourKey;title=MyFold;private=1

        // Update
        // http://www.toodledo.com/api.php?method=editFolder;key=YourKey;id=12345;title=MyFolder

        // Delete
        // http://www.toodledo.com/api.php?method=deleteFolder;key=YourKey;id=12345;

        // <folder id="123" private="0" archived="0">Shopping</folder>
        public int ID { get; set; }
        public bool Private { get; set; }
        public bool Archived { get; set; }
        // 32 char
        public string Title { get; set; }
    }
}
