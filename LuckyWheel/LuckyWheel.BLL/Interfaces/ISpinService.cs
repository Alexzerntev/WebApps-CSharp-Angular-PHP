using LuckyWheel.Model;
using LuckyWheel.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuckyWheel.BLL.Interfaces
{
    public interface ISpinService
    {
        Task<Result<List<SpinDTO>>> GetSpins(string userId);

        Task<Result<bool>> InsertSpin(string userId, double playedAmount, WheelSegment segment);
    }
}
