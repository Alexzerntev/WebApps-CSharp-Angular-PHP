using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LuckyWheel.Model.DTO
{
    public class SpinDTO
    {
        public SpinDTO() { }
        public SpinDTO(Spin spin)
        {
            PlayedAmount = spin.PlayedAmount;
            Win = spin.Win;
            TotalAmount = spin.TotalAmount;
            TimeStamp = spin.TimeStamp;
        }
        public double PlayedAmount { set; get; }
        public bool Win { set; get; }
        public double TotalAmount { set; get; }
        public DateTime TimeStamp { set; get; }

        public static List<SpinDTO> ToDTO(List<Spin> spins)
        {
            return spins.Select(x => new SpinDTO(x)).ToList();
        }
    }
}
