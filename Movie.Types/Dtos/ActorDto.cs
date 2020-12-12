using Movie.Types.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Movie.Types.Dtos
{
    [DataContract]
    public class ActorDto
    {
        [DataMember(Name="Id")]
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


        //An Actor can appear multiple times in the Movies table
        [DataMember(Name = "Movies")]
        public  List<MovieModel> Movies { get; set; }

        //Reference Navigation Property
        public Character Hero { get; set; }
    }
}
