using System.ComponentModel.DataAnnotations;

namespace WebAPIVersionControl.Models
{
    public class Country
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Continent { get; set; }
        public string Region { get; set; }
        public float SurfaceArea { get; set; }
        public int? IndepYear { get; set; }
        public int Population { get; set; }
        public float? LifeExpectancy { get; set; }
        public decimal? GNP { get; set; }
        public decimal? GNPOld { get; set; }
        public string LocalName { get; set; }
        public string GovernmentForm { get; set; }
        public string? HeadOfState { get; set; }
        public int? Capital { get; set; }
        public string Code2 { get; set; }

        // Navigation property example (if needed):
        // public virtual City CapitalCity { get; set; }
    }
}
