using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAI_APP.Models
{
    public class Message : BaseModel
    {
        public string SenderId { get; set; }
        public string Content { get; set; }
        public User Sender { get; set; }
        public ICollection<MessageModerator> Moderators { get; set; }
    }
}
