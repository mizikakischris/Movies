using Movie.Types.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Movie.Types.Dtos
{
    [DataContract]
    public class MovieDto
    {
      
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [Required]
        [DataMember(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [DataMember(Name ="release date")]
        public DateTime ReleaseDate{ get; set; }
        [Required]
        [DataMember(Name = "box office")]
        public decimal BoxOffice { get; set; }
        [DataMember(Name = "picture")]
        public byte[] Picture { get; set; }

        [DataMember(Name = "Actors")]
        public ICollection<ActorDto> Actors { get; set; }
    }
}
