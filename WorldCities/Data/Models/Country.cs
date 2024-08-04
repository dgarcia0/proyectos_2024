using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
        public virtual List<City> Cities { get; set; }
        #endregion
    }
}
