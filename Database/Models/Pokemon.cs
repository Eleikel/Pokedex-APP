using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Pokemon
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Foto { get; set; }


        #region ForeignKey
        public int RegionId { get; set; }
        public int PrimaryTypeId { get; set; }
        public int SecondaryTypeId { get; set; }


        //Navigation Property
        public Region Region { get; set; }
        public Tipo TipoPrimary { get; set; }
        public Tipo TipoSecondary { get; set; }

        #endregion
    }
}
