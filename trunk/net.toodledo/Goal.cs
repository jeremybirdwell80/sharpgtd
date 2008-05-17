using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Net.Toodledo
{
    public class Goal
    {

        // <goal id="123" level="0" contributes="0">Get a Raise</goal>
        public int ID { get; set; }
        public int Level { get; set; }
        public int ContributesID { get; set; }
        public string Title { get; set; }

        // Get
        // http://www.toodledo.com/api.php?method=getGoals;key=YourKey
        // Adding
        // http://www.toodledo.com/api.php?method=addGoal;key=YourKey;title=MyGoal;level=1;contributes=12345
        // Response:  <added>12345</added>   // ID
        //
        // Delete
        // http://www.toodledo.com/api.php?method=deleteGoal;key=YourKey;id=12345;
    }
}
