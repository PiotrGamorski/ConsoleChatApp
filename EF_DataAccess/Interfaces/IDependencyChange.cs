using System;

namespace ConsoleChatApp.Interfaces
{
    public interface IDependencyChange
    {
        public string TableName { get; }
        public string SqlCommand { get; }
        void OnDependencyChange(object sender, EventArgs e);
    }
}
