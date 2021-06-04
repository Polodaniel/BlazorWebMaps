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

        public async Task UploadFilesAsync(InputFileChangeEventArgs e)
        {
            foreach (var file in e.GetMultipleFiles())
            {
                files.Add(file);

                var Arquivo = await ManipularArquivoAsync(file);

                await leafletMap.MarcarMapa(Arquivo);
            }
        }

        private async Task<Dados> ManipularArquivoAsync(IBrowserFile file)
        {
            var result = await new ConversorKML().ObterCoordenadas(file);

            return result;
        }

        public async Task LimparLista() 
        {
            files = new List<IBrowserFile>();

            await leafletMap.RemoverMarcacoes();
        }
    }
}
