using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuckyWheel.Model
{
    public class UserInfo
    {
        [Key]
        [ForeignKey("User")]
        public Guid Id { set; get; }
        virtual public User User { get; set; }
        public string IBAN { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
