using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebH2O.Models
{
    public class Usuario
    {
        public Int32 cod_usuario { get; set; }
        public String nome { get; set; }
        public String email { get; set; }
        public String cpf { get; set; }
        public String rg { get; set; }
        public String senha { get; set; }
        public String token { get; set; }
        
    }
}