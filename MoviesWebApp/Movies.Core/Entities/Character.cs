using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Core.Entities
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
