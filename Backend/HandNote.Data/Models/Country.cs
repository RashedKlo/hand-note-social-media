
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandNote.Data.Models
{
    /// <summary>
    /// Represents a country entity.
    /// Maps to the 'countries' table.
    /// </summary>
    public class Country
    {
        [Key]
        [Column("country_id")]
        public int CountryId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("country_name")]
        public string CountryName { get; set; } = string.Empty;
    }
}
