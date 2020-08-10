using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BtmManager.Models
{
    public class Projekt
    {
        [Key]
        public int ProjektId { set; get; }
        [Required]
        public string BtmBestandsbuchNr { get; set; }
        [Required]
        public int Stufenanzahl { get; set; }
        public string Produktbezeichnung { get; set; }
        [Required]
        public int ProduktNr { get; set; }
        public DateTime Zeitraum { get; set; }

        public IList<Stufe> Stufen { get; set; }
    }
}
