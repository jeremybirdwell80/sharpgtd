using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Net.Toodledo
{
    public class Context
    {
        // Get
        // http://www.toodledo.com/api.php?method=getContexts;key=YourKey
        // Delete
        // http://www.toodledo.com/api.php?method=deleteContext;key=YourKey;id=12345;
        // Add Service
        // http://www.toodledo.com/api.php?method=addContext;key=YourKey;title=MyContext
        // Response: <added>12345</added>

        public int ID { get; set; }
        // 32
        public string Title { get; set; }

        
    }
}
