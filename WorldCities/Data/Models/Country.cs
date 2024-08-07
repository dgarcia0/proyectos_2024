using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WorldCities.Data.Models
{
    public class Country
    {
        public Country() { }

        #region Properties
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public int TotCities
        {
            get { return (Cities != null) ? Cities.Count : _TotCities; }
            set { _TotCities = value; }
        }
        private int _TotCities = 0;
        [JsonIgnore]
        public virtual List<City> Cities { get; set; }
        #endregion
    }
}
