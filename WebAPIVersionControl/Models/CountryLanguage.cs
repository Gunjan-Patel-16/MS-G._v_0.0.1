using System.ComponentModel.DataAnnotations;

namespace WebAPIVersionControl.Models
{
    public class CountryLanguage
    {
        [Key]
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string Language { get; set; }
        public string IsOfficial { get; set; }
        public float Percentage { get; set; }

        // Navigation property example (if needed):
        // public virtual Country Country { get; set; }
    }
}
