using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteNoiThat
{
    [Serializable]
    public class UserLogin
    {
     
        public int UserId { get; set; }
        public string Username { get; set; }
        public string GroupId { get; set; }

    }
}