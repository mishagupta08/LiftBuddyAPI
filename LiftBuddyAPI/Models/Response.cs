using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiftBuddyAPI.Models
{
    
        public class UserResponse
        {
            /// <summary>
            /// gets or sets status
            /// </summary>
            public bool Status { get; set; }

            /// <summary>
            /// gets or sets response value
            /// </summary>
            public string ResponseValue { get; set; }

            public int UserId { get; set; }

        }

        public class UserLoginResponse
        {

            public bool Status { get; set; }

            public string ResponseValue { get; set; }

            public string IsRegistered { get; set; }

            public int UserId { get; set; }

        }


        public class RecordResponse
        {
            public bool Status { get; set; }
            public string ResponseValue { get; set; }
            public int RecordId { get; set; }
        }

        public class Response
        {
            /// <summary>
            /// gets or sets status
            /// </summary>
            public bool Status { get; set; }

            /// <summary>
            /// gets or sets response value
            /// </summary>
            public string ResponseValue { get; set; }

        }
    
}