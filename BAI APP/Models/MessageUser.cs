namespace BAI_APP.Models
{
    public class MessageUser : BaseModel
    {
        public int MessageId { get; set; }
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }
        public Message Message { get; set; }
    }
}
