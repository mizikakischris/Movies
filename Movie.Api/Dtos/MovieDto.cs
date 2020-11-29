using System;

namespace Movie.Api.Dtos
{
    public class MovieDto
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate{ get; set; }
        public decimal BoxOffice { get; set; }
        public byte[] Picture { get; set; }
    }
}
