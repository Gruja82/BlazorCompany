using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCompany.Data.Entities
{
    public class PurchaseDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PurchaseId { get; set; }
        [Required]
        public int MaterialId { get; set; }
        [Required]
        public double QtyMaterial { get; set; }
        public virtual Purchase Purchase { get; set; } = default!;
        public virtual Material Material { get; set; } = default!;
    }
}
