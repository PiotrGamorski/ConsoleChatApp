namespace ConsoleChatApp.Interfaces
{
    public interface IUserService
    {
        bool IsLoginValid(string login);
        void SetUserIsLogged(bool isLogged, string login);
    }
}
