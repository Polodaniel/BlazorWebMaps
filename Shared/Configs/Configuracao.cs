using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebMaps.Shared.Configs
{
    public class Configuracao
    {
        public Configuracao()
        {
            this.Template = "https://api.maptiler.com/maps/hybrid/256/{z}/{x}/{y}@2x.jpg?key=OhKLq5wlAdK90y0vDvPY";
            this.Attribution = "Gerado por <a href=\"https://gr3b.com\">GR3B - Soluções Tecnológicas</a>";
            this.MinZoom = 1;
            this.MaxZoom = 20;
        }

        public string Template { get; set; }

        public string Attribution { get; set; }

        public int MinZoom { get; set; }

        public int MaxZoom { get; set; }
    }
}
