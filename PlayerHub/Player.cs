using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using PlayerHub.Interfaces;

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
            _logger.LogInformation($"User connected: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, getGroupId());
            _roomManager.DisconnectUser(Context.ConnectionId);
            _logger.LogInformation($"User disconnected: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }


        public async Task AssignGroupAsync(string groupId)
        {
            _roomManager.ConnectUser(groupId, Context.ConnectionId);

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

            _roomManager.userIsReady(group, Context.ConnectionId);  

            if (_roomManager.everyOneIsReady(group))
            {
                await Clients.OthersInGroup(group).userIsReadyAsync();
            }        
        }

        private string getGroupId()
        {
            var group = _roomManager.GetRoomId(Context.ConnectionId);
            if (group == null)
                throw new ArgumentNullException($"Cant find group for user {Context.ConnectionId}");

            return group;
        }

    }


}