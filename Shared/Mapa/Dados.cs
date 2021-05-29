using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebMaps.Shared.Mapa
{
    public class Dados
    {
        public Dados(string fazenda, string talhao, string area)
        {
            this.fazenda = fazenda;
            this.talhao = talhao;
            this.area = area;
        }

        public string fazenda { get; set; }
        public string talhao { get; set; }
        public string area { get; set; }
    }
}
