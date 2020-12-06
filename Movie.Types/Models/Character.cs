using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Movie.Types.Models
{
   [Table("Characters")]
   public  class Character
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "First Name cannot be more than 100 characters")]
        public string Name { get; set; }

        public string Hero { get; set; }
        public byte[] Picture { get; set; }

        public int ActorId { get; set; }

        public virtual  Actor Actor { get; set; }
    }
}
