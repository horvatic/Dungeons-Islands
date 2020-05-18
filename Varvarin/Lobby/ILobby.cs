using System;
using System.Threading.Tasks;
using Varvarin.UserComponent;

namespace Varvarin.Lobby
{
    public interface ILobby
    {
        void StopLobby();
        void StartLobby();
        Task<AddUserToLobbyResult> AddUserToLobby(IUser user);
        Task RemoveUser(IUser user);
        bool IsLobbyEmpty();
        Task ProcessClientMessage(string message, IUser user);
    }
}