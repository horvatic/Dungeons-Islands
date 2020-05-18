using System.Net.WebSockets;

namespace Varvarin.UserComponent
{
    public interface IUserCloseResult
    {
        WebSocketCloseStatus CloseStatus { get; }
        string CloseStatusDescription { get; }
    }
}
