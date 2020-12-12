using Movie.Types.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie.Types.Models
{
    [Table("Movies")]
    public class MovieModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Title can not be more than 200 characters")]
        public string Title { get; set; }

        [Required]
        [Display(Name ="Release Date")]
        public DateTime? ReleaseDate{ get; set; }

        [Required]
        [Display(Name = "Box Office")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BoxOffice { get; set; }

        public byte[] Picture { get; set; }

        public virtual ICollection<MovieActor> MovieActors { get; set; }

        [NotMapped]
        public ICollection<ActorDto> Actors { get; set; }

    }
}
