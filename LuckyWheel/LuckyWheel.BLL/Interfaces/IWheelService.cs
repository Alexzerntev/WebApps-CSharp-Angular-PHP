using LuckyWheel.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LuckyWheel.BLL.Interfaces
{
    public interface IWheelService
    {
        Task<Result<JObject>> GetWheelSettings();
        Task<Result<int>> GetSpinResult(string userId, double playedAmount);
        Task<Result<double>> GetMaxMultyplyer();
    }
}
