using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Movie.Types.Dtos
{
    [DataContract]
    public class MovieDto
    {
       // [Required]
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [Required]
        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        [Required]
        [DataMember(Name ="release date")]
        public DateTime ReleaseDate{ get; set; }
        [Required]
        [DataMember(Name = "box office")]
        public decimal BoxOffice { get; set; }
        [DataMember(Name = "picture")]
        public byte[] Picture { get; set; }
    }
}
