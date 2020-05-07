using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public partial class tblUser
    {        
            public int UserId { get; set; }
            public string DobStr { get; set; }                  
            public string fileBytes { get; set; }
            public string fileName { get; set; }  
            public bool RemoveProfilePic { get; set; }
    }      
    public partial class tblUserStatistic
    {
        public int level { get; set; }             
        public string Period { get; set; }
        public string StartHour { get; set; }
        public string EndHour { get; set; }
    }    
}
