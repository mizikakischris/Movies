using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Types.Models
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }

        public virtual MovieModel Movie { get; set; }
        public virtual Actor Actor { get; set; }
    }
}
