using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCompany.Data.Entities
{
    public class Production
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; } = default!;
        [Required]
        public DateTime ProductionDate { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int QtyProduct { get; set; }
        public virtual Product Product { get; set; } = default!;
    }
}
