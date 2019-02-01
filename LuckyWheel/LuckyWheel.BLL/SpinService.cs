using LuckyWheel.BLL.Interfaces;
using LuckyWheel.DAL;
using LuckyWheel.Model;
using LuckyWheel.Model.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Net;

namespace LuckyWheel.BLL
{
    public class SpinService : ISpinService
    {
        private readonly Context context;

        public SpinService(Context _context)
        {
            context = _context;
        }
        public async Task<Result<List<SpinDTO>>> GetSpins(string userId)
        {
            var user = await context.Users.FindAsync(Guid.Parse(userId));
            if (user == null)
            {
                return Result<List<SpinDTO>>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης χρήστη");
            }
            var spins = await context.Spins.Where(x=>x.User == user).ToListAsync();
            if (spins == null)
            {
                return Result<List<SpinDTO>>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης χρήστη");
            }
            return Result<List<SpinDTO>>.CreateSuccessful(SpinDTO.ToDTO(spins));
        }
        public async Task<Result<bool>> InsertSpin(string userId, double playedAmount, WheelSegment segment)
        {
            var user = await context.Users.FindAsync(Guid.Parse(userId));
            if (user == null)
            {
                return Result<bool>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης υπολοιίου");
            }
            var spin = new Spin();
            spin.PlayedAmount = playedAmount;
            spin.TimeStamp = DateTime.Now;
            spin.Win = segment.Win;
            spin.TotalAmount = playedAmount * segment.Multiplier;
            spin.User = user;
            try
            {
                await context.Spins.AddAsync(spin);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Result<bool>.CreateFailed(
                    HttpStatusCode.NotFound, "Αδυναμία ανάκτησης υπολοιίου");
            }
            return Result<bool>.CreateSuccessful(true);
        }
    }
}
