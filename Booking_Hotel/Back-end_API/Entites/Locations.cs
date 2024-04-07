using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_API.Entites
{
    [Table("Locations_tbl")]
    public class Locations:Base
    {
        public string LocationName { get; set; }
        public string Description { get; set; }
    }
}
