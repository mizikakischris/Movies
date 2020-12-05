using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Movie.Types.Models
{
   [Table("Heroes")]
   public  class Hero
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public byte[] Picture { get; set; }

        public int ActorId { get; set; }

        public Actor Actor { get; set; }
    }
}
