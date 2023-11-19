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
        private Dictionary<string, string> UserGroups = new Dictionary<string, string>();
        private Dictionary<string, double> RoomTime = new Dictionary<string, double>();
        private Dictionary<string, List<UserReady>> RoomReady = new Dictionary<string, List<UserReady>>();
        public void ConnectUser(string roomId, string connectionId)
        {
            UserGroups.Add(connectionId, roomId);

            if (RoomReady.ContainsKey(roomId))
            {
                RoomReady[roomId].Add(new UserReady { userId = connectionId });
            }
            else
            {
                RoomReady.Add(roomId, new List<UserReady> { new UserReady { userId = connectionId } }); 
            }
        }

        public void setCurrentTime(string roomId, double time)
        {
            RoomTime[roomId] = time;
        }

        public void DisconnectUser(string connectionId)
        {

            if (UserGroups.ContainsKey(connectionId))
            {
                var roomId = UserGroups[connectionId];

                if (RoomReady.ContainsKey(roomId))
                {
                    var userReady = RoomReady[roomId].FirstOrDefault(x => x.userId == connectionId);
                    if (userReady != null)
                        RoomReady[roomId].Remove(userReady);
                }

                UserGroups.Remove(connectionId);
            }
        }

        public string GetRoomId(string connectionId)
        {
            if (UserGroups.ContainsKey(connectionId))
            {
                return UserGroups[connectionId];
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

            return RoomReady[roomId].All(x => x.isReady);
        }

        public void userIsReady(string roomId, string connectionId)
        {
            if (!RoomReady.ContainsKey(roomId))
            {
                return;
            }

            var userReady = RoomReady[roomId].FirstOrDefault(x => x.userId == connectionId);
            if(userReady != null) 
                userReady.isReady = true;
        }
    }
}
