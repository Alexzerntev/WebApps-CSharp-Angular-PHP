using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuckyWheel.Model
{
    public abstract class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; protected set; }
    }
}
