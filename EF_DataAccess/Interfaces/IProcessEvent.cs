using System;

namespace ConsoleChatApp.Interfaces
{
    public interface IProcessEvent
    {
        void Process(object sender, EventArgs e);
    }
}
