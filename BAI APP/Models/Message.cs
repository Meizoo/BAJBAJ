using System.Collections.Generic;

namespace BAI_APP.Models
{
    public class Message : BaseModel
    {
        public int SenderId { get; set; }
        public string Content { get; set; }
        public User Sender { get; set; }
        public ICollection<MessageUser> Receivers { get; set; }
    }
}
