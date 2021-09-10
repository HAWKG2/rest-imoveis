using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Imoveis
    {
        public string tipoImovel { get; set; }
        public double valorVenda { get; set; }
        public double valorLocacao { get; set; }
        public double areaEdificada { get; set; }
        public string endereco { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string cep { get; set; }
        public string id { get; set; }
    }
}