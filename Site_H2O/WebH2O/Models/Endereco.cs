using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebH2O.Models
{
    public class Endereco
    {
        public Int32 cod_endereco { get; set; }
        public String logradouro { get; set; }
        public Int32 numero { get; set; }
        public String CEP { get; set; }
        public String complemento { get; set; }
        public Int32 cod_bairro { get; set; }
        public Int32 cod_usuario { get; set; }
    }
}