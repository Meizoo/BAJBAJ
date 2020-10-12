using System;
using System.Collections.Generic;

namespace BAI_APP.Models
{
    public class User : BaseModel
    {
        public int FailedCount { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Age { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public DateTime? FailedLoginDate { get; set; }
        public DateTime? LoginDate { get; set; }
        public Role Role { get; set; }
        public ICollection<MessageModerator> Messages { get; set; }
    }
}
