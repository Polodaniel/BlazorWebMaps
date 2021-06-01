using BlazorWebMaps.Shared.Configs;
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

        public async Task UploadFilesAsync(InputFileChangeEventArgs e)
        {
            foreach (var file in e.GetMultipleFiles())
            {
                files.Add(file);

                var Arquivo = await ManipularArquivoAsync(file);
            }
        }

        private async Task<int> ManipularArquivoAsync(IBrowserFile file)
        {
            var memoryStream = new MemoryStream();

            await file.OpenReadStream().CopyToAsync(memoryStream);

            var array = memoryStream.ToArray();

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(file.Name);

            XDocument dox = XDocument.Parse(doc.OuterXml);
            XNamespace ns = dox.Root.Name.Namespace;

            return 0;
        }

        public void LimparLista() =>
            files = new List<IBrowserFile>();
    }
}
