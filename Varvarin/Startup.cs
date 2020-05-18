using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Varvarin.Game;
using Varvarin.UserComponent;

namespace Varvarin.Web
{
    public class Startup
    {
        const int BUFFER_SIZE = 4 * 1024;

        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                ReceiveBufferSize = BUFFER_SIZE
            };
            app.UseWebSockets(webSocketOptions);
            var deafultLobby = new GameLobby();
            var lobbyCoordinator = new GameCoordinator(deafultLobby);

            app.Use(async (context, next) =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var socket = await context.WebSockets.AcceptWebSocketAsync();
                    var user = new User(socket, BUFFER_SIZE);
                    deafultLobby.AddUserToLobby(user).GetAwaiter().GetResult();
                    await lobbyCoordinator.RunUserSession(user);
                }
                else
                {
                    context.Response.StatusCode = 405;
                }
            });
        }
    }
}
