using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Movie.Types.Dtos
{
    [DataContract]
   public class ActorsByMovieDto
    {

        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [DataMember]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [DataMember(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        [DataMember(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [DataMember(Name = "Picture")]
        public byte[] Picture { get; set; }

        [Required]
        public int CharacterId { get; set; }
    }
}
