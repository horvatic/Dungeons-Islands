using System;
using System.Threading.Tasks;

namespace Varvarin.UserComponent
{
    public interface IUser
    {
        string GetUserName();
        void SetUserName(string newName);
        Task<IUserResult> ReceiveMessage();
        Task SendMessage(string message);
        Task CloseUserConnection(IUserCloseResult result);
        bool IsAlive();
    }
}
