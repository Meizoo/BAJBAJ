using System.ComponentModel.DataAnnotations.Schema;

namespace BAI_APP.Models
{
    public class MessageModerator : BaseModel
    {
        [ForeignKey("Message")]
        public string MessageId { get; set; }
        [ForeignKey("Moderator")]
        public string ModeratorId { get; set; }
        public User Moderator { get; set; }
        public Message Message { get; set; }
    }
}
