using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebMaps.Shared.Mapa
{
    public class Mapas
    {
        public Mapas() { }

        public Mapas(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
