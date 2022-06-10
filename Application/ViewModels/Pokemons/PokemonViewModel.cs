using Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class PokemonViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Debes ingresar el nombre del Pokemon")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Debes ingresar una URL para la Foto")]
        public string Foto { get; set; }
        [Required(ErrorMessage = "Debes seleccionar una Region")]
        public int RegionId { get; set; }
        [Required(ErrorMessage = "Debes seleccionar el Tipo Primario")]
        public int PrimaryTypeId { get; set; }
        public int SecondaryTypeId { get; set; }


        //Navigation Property
        public Region Region { get; set; }
        public Tipo TipoPrimary { get; set; }
        public Tipo TipoSecondary { get; set; }
    }
}
