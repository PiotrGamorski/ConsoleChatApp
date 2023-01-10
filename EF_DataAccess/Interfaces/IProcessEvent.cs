using System;

namespace ConsoleChatApp.Interfaces
{
    interface IProcessEvent
    {
        void Process(object sender, EventArgs e);
    }
}
