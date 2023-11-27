using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using PlayerHub.Interfaces;
using PlayerHub.Models;
using System.Text.Json;

namespace PlayerHub
{
    public class Player : Hub <ICommunicationHub>
    {
        private readonly ILogger<Player> _logger;
        private readonly IRoomManager _roomManager;
        public Player(ILogger<Player> logger, IRoomManager roomManager)
        {
            _logger = logger;
            _roomManager = roomManager;
        }
        public override Task OnConnectedAsync()
        {
            var user = getUser();

            _logger.LogInformation($"User connected: {user.UserId} ({user.Username})");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = getUser();

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, getGroupId());
            _roomManager.DisconnectUser(user);
            _logger.LogInformation($"User disconnected: {user.UserId} ({user.Username})");
            await base.OnDisconnectedAsync(exception);
        }


        public async Task AssignGroupAsync(string groupId)
        {
            var user = getUser();

            _roomManager.ConnectUser(groupId, user);

            var roomTime = _roomManager.getCurrentTime(groupId);
            await Clients.Caller.ChangeTimeAsync(roomTime);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }

        public async Task PlayAsync()
        {
            await Clients.OthersInGroup(getGroupId()).PlayAsync();
        }
        public async Task PauseAsync()
        {
            await Clients.OthersInGroup(getGroupId()).PauseAsync();
        }

        public async Task ChangeTimeAsync(double time)
        {
            await Clients.OthersInGroup(getGroupId()).ChangeTimeAsync(time);
        }

        public Task UpdateRoomTime(double time)
        {
            _roomManager.setCurrentTime(getGroupId(), time);
            return Task.CompletedTask;
        }


        public async Task waitForAllAsync()
        {
            await Clients.OthersInGroup(getGroupId()).waitForAllAsync();
        }
        public async Task userIsReadyAsync() {

            var group = getGroupId();
            var user = getUser();

            _roomManager.userIsReady(group, user);  

            if (_roomManager.everyOneIsReady(group))
            {
                await Clients.OthersInGroup(group).userIsReadyAsync();
            }        
        }

        private string getGroupId()
        {
            var user = getUser();

            var group = _roomManager.GetRoomId(user);
            if (group == null)
                throw new ArgumentNullException($"Cant find group for user {Context.ConnectionId}");

            return group;
        }

        private User getUser()
        {
            string? userInfo = Context?.User?.Claims?.FirstOrDefault(x => x.Type == "UserInfo")?.Value;
            if (userInfo != null) { 
                var user = JsonSerializer.Deserialize<User>(userInfo);
                if (user != null)
                {
                    return user;
                }
            }

            return _roomManager.getAnonymousUser(Context.ConnectionId);
        }

    }


}