using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerHub.Interfaces
{
    public interface IRoomManager
    {
        void ConnectUser(string roomId, string connectionId);
        void DisconnectUser(string connectionId);
        string GetRoomId(string connectionId);
        void setCurrentTime(string roomId, double time);
        double getCurrentTime(string roomId);
        bool everyOneIsReady(string roomId);
        void userIsReady(string roomId, string connectionId);

    }
}
