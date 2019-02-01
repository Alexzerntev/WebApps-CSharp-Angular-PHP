using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LuckyWheel.Model.DTO
{
    public class DepositeCodeDTO
    {   public DepositeCodeDTO(DepositCode depositeCode)
        {
            UsingCode = depositeCode.UsingCode;
            IsUsed = depositeCode.IsUsed;
            Amount = depositeCode.Amount;
        }
        public string UsingCode { set; get; }
        public bool IsUsed { get; set; }
        public double Amount { get; set; }

        public static List<DepositeCodeDTO> ToDTO(List<DepositCode> depositecodes)
        {
            return depositecodes.Select(x => new DepositeCodeDTO(x)).ToList();
        }
    }
}
