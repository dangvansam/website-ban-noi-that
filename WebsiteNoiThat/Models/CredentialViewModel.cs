using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CredentialViewModel
    {
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public List<Role> roles { get; set; }
    }
}
