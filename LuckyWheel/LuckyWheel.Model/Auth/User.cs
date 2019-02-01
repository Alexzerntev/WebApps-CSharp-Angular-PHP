using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LuckyWheel.Model
{
    public class User : IdentityUser<Guid>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public override Guid Id { get; set; }
        virtual public UserInfo UserInfo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Balance { get; set; }
        virtual public Photo Photo { get; set; }
        virtual public ICollection<Transaction> Transactions { get; set; }
        virtual public ICollection<Spin> Spins { get; set; }
        virtual public ICollection<DepositCode> DepositCodes { get; set; }
    }
}
