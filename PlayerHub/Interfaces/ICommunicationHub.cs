using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerHub
{
    public interface ICommunicationHub
    {
        Task AssignGroupAsync();
        Task PlayAsync();
        Task PauseAsync();
        Task ChangeTimeAsync(double time);
        Task UpdateRoomTime(double time);
        Task waitForAllAsync();
        Task userIsReadyAsync();
    }
}
