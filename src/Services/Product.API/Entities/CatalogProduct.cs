using Contracts.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.API.Entities
{
    public class CatalogProduct : EntityAuditBase<long>
    {
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string No { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string Summary { get; set; }

        [Required]
        public string Description { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal? Price { get; set; }
    }
}
