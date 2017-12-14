using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebH2O.Models
{
    public class Leitura
    {
        public Int32 cod_leitura { get; set; }
        public DateTime hora { get; set; }
        public Int32 consumo { get; set; }
        public Int32 cond_dispositivo { get; set; }
       
    }
}