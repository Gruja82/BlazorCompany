﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCompany.Shared.Dtos
{
    public class BaseDto
    {
        public int Id { get; set; }
        public IFormFile? Image { get; set; }
    }
}
