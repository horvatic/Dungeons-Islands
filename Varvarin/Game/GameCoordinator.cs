
using System.Threading.Tasks;
using Varvarin.UserComponent;

namespace Varvarin.Game
{
    public class GameCoordinator
    {
        private readonly ILobby _lobby;

        public GameCoordinator(ILobby lobby)
        {
            _lobby = lobby;
            _lobby.StartLobby();
        }

        public async Task RunUserSession(IUser user)
        {
            var result = await user.ReceiveMessage();
            while (!result.HasConnectionClosed() && !result.IsConntectionLost())
            {
                
                await _lobby.ProcessClientMessage(result.GetMessage(), user);
                result = await user.ReceiveMessage();
            }

            await _lobby.RemoveUser(user);

            if (!result.IsConntectionLost())
                await user.CloseUserConnection(result.GetCloseResult());
        }
    }
}
