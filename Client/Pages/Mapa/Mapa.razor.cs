using BlazorWebMaps.Client.Shared.Componente;
using BlazorWebMaps.Shared.Configs;
using BlazorWebMaps.Shared.Conversor;
using BlazorWebMaps.Shared.Mapa;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BlazorWebMaps.Client.Pages.Mapa
{
    public class MapaBase : ComponentBase
    {
        [Inject]
        public ISnackbar Snackbar { get; set; }

        public IList<IBrowserFile> files = new List<IBrowserFile>();

        public LeafletMap leafletMap;

        public List<MapaAuxiliar> ListaDados { get; set; }

        public MapaBase()
        {
            ListaDados = new List<MapaAuxiliar>();
        }

        public async Task UploadFilesAsync(InputFileChangeEventArgs e)
        {
            foreach (var file in e.GetMultipleFiles())
            {
                files.Add(file);

                var Arquivo = await ManipularArquivoAsync(file);

                if (!Equals(Arquivo.Coordenadas, null) && Arquivo.Coordenadas.Count > 0)
                {
                    Arquivo.Coordenadas.ForEach(item =>
                    {
                        ListaDados.Add(new MapaAuxiliar(Arquivo.fazenda, item.latitude, item.longitude));
                    });
                }

                await leafletMap.MarcarMapa(Arquivo);
            }
        }

        private async Task<Dados> ManipularArquivoAsync(IBrowserFile file) =>
             await new ConversorKML().ObterCoordenadas(file);

        public async Task LimparLista()
        {
            files = new List<IBrowserFile>();

            ListaDados = new List<MapaAuxiliar>();

            await leafletMap.RemoverMarcacoes();
        }
    }

    public class MapaAuxiliar
    {
        public MapaAuxiliar(string fazenda, double latitude, double longitude)
        {
            this.fazenda = fazenda;
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public string fazenda;
        public double latitude;
        public double longitude;
    }
}
