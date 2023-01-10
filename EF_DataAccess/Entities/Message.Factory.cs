namespace ConsoleChatApp.Entities
{
    public partial class Message
    {
        public static class Factory
        {
            public static Message Create(string text, int messsageRecipientId, int messsageReciverId)
            {
                return new Message(text, messsageRecipientId, messsageReciverId);
            }
        }
    }
}
