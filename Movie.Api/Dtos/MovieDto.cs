using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Movie.Api.Dtos
{
    [DataContract]
    public class MovieDto
    {
       // [Required]
        [DataMember(Name = "Id")]
        public int Id { get; set; }
        [Required]
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        
        [Required]
        [DataMember(Name ="Release Date")]
        public DateTime ReleaseDate{ get; set; }
        [Required]
        [DataMember(Name = "Box Office")]
        public decimal BoxOffice { get; set; }
        [DataMember(Name = "Picture")]
        public byte[] Picture { get; set; }
    }
}
