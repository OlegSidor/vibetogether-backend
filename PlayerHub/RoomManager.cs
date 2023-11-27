using PlayerHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerHub.Interfaces
{
    public class RoomManager : IRoomManager
    {
        private Dictionary<User, string> UserGroups = new Dictionary<User, string>();
        private Dictionary<string, User> AnonymousUsers = new Dictionary<string, User>();
        private Dictionary<string, double> RoomTime = new Dictionary<string, double>();
        private Dictionary<string, List<User>> RoomReady = new Dictionary<string, List<User>>();
        public void ConnectUser(string roomId, User user)
        {
            UserGroups.Add(user, roomId);

            if (RoomReady.ContainsKey(roomId))
            {
                RoomReady[roomId].Add(user);
            }
            else
            {
                RoomReady.Add(roomId, new List<User> { user }); 
            }
        }

        public void setCurrentTime(string roomId, double time)
        {
            RoomTime[roomId] = time;
        }

        public void DisconnectUser(User user)
        {

            if (UserGroups.ContainsKey(user))
            {
                var roomId = UserGroups[user];

                if (RoomReady.ContainsKey(roomId))
                {
                    var userReady = RoomReady[roomId].FirstOrDefault(x => x.UserId == user.UserId);
                    if (userReady != null)
                        RoomReady[roomId].Remove(userReady);
                }

                UserGroups.Remove(user);
            }
            if (AnonymousUsers.ContainsValue(user))
            {
                var connectionId = AnonymousUsers.First(x => x.Value.Equals(user)).Key;
                AnonymousUsers.Remove(connectionId);
            }
        }

        public string GetRoomId(User user)
        {
            if (UserGroups.ContainsKey(user))
            {
                return UserGroups[user];
            }

            return null;
        }

        public double getCurrentTime(string roomId)
        {
            if (RoomTime.ContainsKey(roomId))
            {
                return RoomTime[roomId];
            }

            return 0;
        }

        public bool everyOneIsReady(string roomId)
        {
            if (!RoomReady.ContainsKey(roomId))
            {
                return false;
            }

            return RoomReady[roomId].All(x => x.IsReady);
        }

        public void userIsReady(string roomId, User user)
        {
            if (!RoomReady.ContainsKey(roomId))
            {
                return;
            }

            var userReady = RoomReady[roomId].FirstOrDefault(x => x.UserId == user.UserId);
            if(userReady != null) 
                userReady.IsReady = true;
        }

        public User getAnonymousUser(string connectionId)
        {
            if(AnonymousUsers.ContainsKey(connectionId))
                return AnonymousUsers[connectionId];

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = "Anonymous"
            };

            AnonymousUsers.Add(connectionId, user);
            return user;
        }
    }
}
