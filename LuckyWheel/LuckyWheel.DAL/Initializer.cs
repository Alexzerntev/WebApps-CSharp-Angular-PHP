using LuckyWheel.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LuckyWheel.DAL
{
    public static class Initializer
    {
        public static void Initialize(Context context, UserManager<User> userManager)
        {
            context.Database.EnsureCreated();
            

            // Look for any users or wheel settings
            if (context.Users.Any() || context.WheelSettings.Any())
            {
                return;   // DB has been seeded
            }

            var ws = new WheelSetting();
            ws.SettingJSONString = GetDefaultSettingsJSONString();
            context.WheelSettings.Add(ws);
            context.SaveChanges();

            List<WheelSegment> segments = new List<WheelSegment>();
            for (int i = 0; i < 3; i++)
            {
                var segment1 = new WheelSegment();
                segment1.ResultText = "You win!";
                segment1.Value = "win x"+(1.5+i);
                segment1.Win = true;
                segment1.Multiplier = (i + 1.5);
                segment1.Type = "string";
                segments.Add(segment1);
                
                var segment2 = new WheelSegment();
                segment2.ResultText = "You lose!";
                segment2.Value = "lose x" + (1.5 + i);
                segment2.Win = false;
                segment2.Multiplier = -(i + 1.5);
                segment2.Type = "string";
                segments.Add(segment2);
            }
            context.AddRange(segments);

            context.SaveChanges();

            //users seed
            for (int i = 0; i < 10; i++)
            {
                User u = new User();
                var ui = new UserInfo();
                ui.RegistrationDate = DateTime.Now;

                u.Email = "user" + i + "@gmail.com";
                u.UserName = u.Email;
                u.Balance = 100;
                u.FirstName = "User" + i;
                u.LastName = "Userovic" + i;

                List<Transaction> tl = new List<Transaction>();
                for (int j = 0; j < 10; j++)
                {
                    Transaction t = new Transaction();
                    t.Amount = i * 10;
                    t.Source = j % 2 == 0 ? TransactionSource.Bank : TransactionSource.DepositCode;
                    t.Type = j % 2 == 0 ? TransactionType.Deposit : TransactionType.Withdrow;
                    t.Date = DateTime.Now;
                    t.User = u;
                    tl.Add(t);
                }
                List<DepositCode> dkl = new List<DepositCode>();
                for (int k = 0; k < 2; k++)
                {
                    DepositCode dk = new DepositCode();
                    dk.User = u;
                    dk.IsUsed = false;
                    dk.Amount = ((k+2)*10);
                    dk.UsingCode = Guid.NewGuid().ToString().Substring(0,8);
                    dkl.Add(dk);
                }
                u.DepositCodes = dkl;
                u.Transactions = tl;
                //context.Transactions.AddRange(tl);

                ui.User = u;
                context.UserInfo.Add(ui);
                var result = userManager.CreateAsync(u,"123456");
            }
            System.Threading.Thread.Sleep(5000);
            context.SaveChanges();
            //wheel settings seed
        }

        public static string GetDefaultSettingsJSONString()
        {
            var dir = Directory.GetCurrentDirectory().Replace("API", "DAL");
            var path = Path.Combine(dir, "wheel_data.json");
            using (StreamReader r = new StreamReader(path))
            {
                return r.ReadToEnd();
            }
        }
    }
}
