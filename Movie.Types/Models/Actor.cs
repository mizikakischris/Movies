using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Movie.Types.Models
{
   [Table("Actors")]
   public class Actor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "First Name cannot be more than 100 characters")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(200, ErrorMessage = "Last Name cannot be more than 200 characters")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        public byte[] Picture { get; set; }

        public virtual ICollection<MovieActor> MovieActors { get; set; }

        //Reference Navigation Property
        public virtual Character Character { get; set; }
    }
}
