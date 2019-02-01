using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using LuckyWheel.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using LuckyWheel.BLL.Interfaces;
using LuckyWheel.Model;
using System.Net;

namespace LuckyWheel.BLL
{
    public class WheelService : IWheelService
    {
        private readonly Context context;
        private readonly IUserService userService;
        private readonly ISpinService spinService;
        public WheelService(Context _context, IUserService _userService, ISpinService _spinService)
        {
            context = _context;
            userService = _userService;
            spinService = _spinService;
        }
        public async Task<Result<JObject>> GetWheelSettings()
        {
            var settings = await context.WheelSettings.ToListAsync();
            if (settings == null)
            {
                return Result<JObject>.CreateFailed(
                    HttpStatusCode.InternalServerError, "Αδυναμία Φόρτωσης Τροχού");
            }
            var setting = settings.FirstOrDefault().SettingJSONString;
            JObject settingsJson = JObject.Parse(setting);

            var segments = await context.WheelSegments.ToListAsync();

            if (segments == null)
            {
                return Result<JObject>.CreateFailed(
                    HttpStatusCode.InternalServerError, "Αδυναμία Φόρτωσης Τροχού");
            }

            var array = new JArray();
            foreach (var segment in segments)
            {
                array.Add(
                    new JObject(
                        new JProperty("type", segment.Type),
                        new JProperty("value", segment.Value),
                        new JProperty("win", segment.Win),
                        new JProperty("resultText", segment.ResultText)
                        )
                    );
            }

            var segmentArrayObject = new JProperty("segmentValuesArray", array);
            //settingsJson.GetValue("colorArray").AddAfterSelf(segmentArrayObject);
            settingsJson.Add(segmentArrayObject);
            return Result<JObject>.CreateSuccessful(settingsJson);
        }
        public async Task<Result<int>> GetSpinResult(string userId, double playedAmount)
        {

            var segments = await context.WheelSegments.ToListAsync();
            if (segments == null)
            {
                return Result<int>.CreateFailed(
                    HttpStatusCode.InternalServerError, "Αδυναμία Φόρτωσης Τροχού");
            }
            var segmentCount = segments.Count;
            Random random = new Random();
            var randomResult = random.Next(1, segmentCount);

            var amountSigned = playedAmount * segments[randomResult - 1].Multiplier;

            var updateBalanceResult = await userService.UpdateBalance(userId, amountSigned);
            if (!updateBalanceResult.IsSuccess())
            {
                return Result<int>.CreateFailed(
                   HttpStatusCode.NotAcceptable, updateBalanceResult.ErrorText);
            }
            var spinInsertionResult = spinService.InsertSpin(userId, playedAmount, segments[randomResult]);
            return Result<int>.CreateSuccessful(randomResult);
        }
        public async Task<Result<double>> GetMaxMultyplyer()
        {

            var segments = await context.WheelSegments.ToListAsync();
            if (segments == null)
            {
                return Result<double>.CreateFailed(
                    HttpStatusCode.InternalServerError, "Αδυναμία Φόρτωσης Τροχού");
            }
            var maxMultyplier = 9999.0;
            foreach (var segment in segments)
            {
                if (segment.Multiplier < maxMultyplier)
                {
                    maxMultyplier = segment.Multiplier;
                }
            }
            return Result<double>.CreateSuccessful(maxMultyplier);
        }
    }
}