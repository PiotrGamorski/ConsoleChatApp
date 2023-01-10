using System;

namespace ConsoleChatApp.Entities
{
    public partial class Message
    {
        public Message() { }

        protected Message(string text, int messsageRecipientId, int messsageReciverId)
        {
            Text = text;
            MesssageRecipientId = messsageRecipientId;
            MesssageReciverId = messsageReciverId;
            DateCreated = DateTime.Now;
        }

        public int id { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }

        public int MesssageRecipientId { get; set; }
        public Person MesssageRecipient { get; set; }

        public int MesssageReciverId { get; set; }
        public Person MesssageReciver { get; set; }
    }
}
