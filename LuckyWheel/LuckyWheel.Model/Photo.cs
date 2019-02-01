using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LuckyWheel.Model
{
    public class Photo
    {
        [Key]
        [ForeignKey("User")]
        public Guid Id { set; get; }
        virtual public User User { set; get; }
        public byte[] PhotoFile { get; set; }
    }
}
