using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Api.Models
{
    public class MovieModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Release Date")]
        public DateTime ReleaseDate{ get; set; }

        [Required]
        [Display(Name = "Box Office")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BoxOffice { get; set; }

        public byte[] Picture { get; set; }
    }
}
