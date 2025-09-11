
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandNote.Data.Models
{
    /// <summary>
    /// Represents a city entity.
    /// Maps to the 'cities' table.
    /// </summary>
    public class City
    {
        [Key]
        [Column("city_id")]
        public int CityId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("city_name")]
        public string CityName { get; set; } = string.Empty;

        // Foreign key
        [Required]
        [Column("country_id")]
        public int CountryId { get; set; }
    }
}
