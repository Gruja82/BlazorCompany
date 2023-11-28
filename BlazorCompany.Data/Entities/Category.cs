using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCompany.Data.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; } = default!;
        [Required]
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = string.Empty;
        public virtual ICollection<Product>? Products { get; set; }
        public  virtual ICollection<Material>? Materials { get; set; }
    }
}
