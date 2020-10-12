using BAI_APP.Models;
using System.Collections.Generic;
using System.Linq;

namespace BAI_APP.ViewModels
{
    public class MessageVM : Message
    {
        public MessageVM()
        {

        }

        public MessageVM(Message message)
        {
            this.SenderId = message.SenderId;
            this.Content = message.Content;
            this.Sender = message.Sender;
            this.Moderators = message.Moderators;
            this.ModeratorsId = message.Moderators?.Select(x => x.ModeratorId).ToList() ?? new List<string>();
        }
        public List<string> ModeratorsId { get; set; }
    }
}
