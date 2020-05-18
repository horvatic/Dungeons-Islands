using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Varvarin.UserComponent;

namespace Varvarin.Game
{
    public class GameLobby : ILobby
    {
        private readonly ConcurrentQueue<string> _messges;
        private readonly List<IUser> _allUsers;
        private readonly CancellationTokenSource lobbyCancellationTokenSource;

        public GameLobby()
        {
            _allUsers = new List<IUser>();
            _messges = new ConcurrentQueue<string>();
            lobbyCancellationTokenSource = new CancellationTokenSource();
        }

        public void StopLobby()
        {
            lobbyCancellationTokenSource.Cancel();
        }

        public void StartLobby()
        {
            var token = lobbyCancellationTokenSource.Token;
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    while (_messges.Count > 0)
                    {
                        var hasMessage = _messges.TryDequeue(out var message);
                        if (!hasMessage)
                            continue;

                        foreach (var user in _allUsers)
                        {
                            await user.SendMessage(message);
                        }
                    }
                }
            });
        }

        public async Task<AddUserToLobbyResult> AddUserToLobby(IUser user)
        {
            _allUsers.Add(user);
            return await Task.FromResult(AddUserToLobbyResult.AddedToLobby);
        }

        public async Task ProcessClientMessage(string message, IUser user)
        {

            _messges.Enqueue(message);
            await Task.CompletedTask;
        }

        public async Task RemoveUser(IUser user)
        {
            _allUsers.Remove(user);
            await Task.CompletedTask;
        }

        public bool IsLobbyEmpty()
        {
            return _allUsers.Count() <= 0;
        }
    }
}
