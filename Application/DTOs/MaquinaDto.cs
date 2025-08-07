﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
   public class MaquinaDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public bool? Activo { get; set; }
    }
}
