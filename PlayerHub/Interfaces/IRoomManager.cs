using PlayerHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerHub.Interfaces
{
    public interface IRoomManager
    {
        void ConnectUser(string roomId, User user);
        void DisconnectUser(User user);
        string GetRoomId(User user);
        void setCurrentTime(string roomId, double time);
        double getCurrentTime(string roomId);
        bool everyOneIsReady(string roomId);
        void userIsReady(string roomId, User user);

        User getAnonymousUser(string connectionId);

    }
}
