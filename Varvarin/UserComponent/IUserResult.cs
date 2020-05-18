namespace Varvarin.UserComponent
{
    public interface IUserResult
    {
        bool HasConnectionClosed();
        IUserCloseResult GetCloseResult();
        string GetMessage();
        void ConntectionLostHasBeenLost();
        bool IsConntectionLost();
    }
}
