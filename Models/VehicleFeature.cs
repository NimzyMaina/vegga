using System.ComponentModel.DataAnnotations.Schema;

namespace vegga.Models
{
    [Table("VehicleFeatures")]
    public class VehicleFeature
    {
        public int VehicleId { get; set; }
        public int FeatureId { get; set; }

        // Navigation props

        public Vehicle Vehicle { get; set; }
        public Feature Feature { get; set; }
    }
}