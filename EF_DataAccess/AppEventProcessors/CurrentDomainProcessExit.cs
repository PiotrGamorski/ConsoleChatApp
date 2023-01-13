using ConsoleChatApp.Interfaces;
using System;

namespace ConsoleChatApp.AppEventsProcessor
{
    public class CurrentDomainProcessExit : IProcessEvent
    {
        private readonly IUserService _service;

        public CurrentDomainProcessExit(IUserService service)
        {
            _service = service;
        }

        public void Process(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CommonData.Login))
                _service.SetUserIsLogged(false, CommonData.Login);
        }
    }
}
