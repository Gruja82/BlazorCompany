using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCompany.Shared.Dtos
{
    public class Pagination<T>
    {
        public List<T> Data { get; set; } = new();
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
    }
}
