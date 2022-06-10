using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Tipo
    {
        public int Id { get; set; }
        public string TypeName { get; set; }

        public ICollection<Pokemon> PokemonsPrimaryType { get; set; }
        public ICollection<Pokemon> PokemonsSecondaryType { get; set; }

    }
}
