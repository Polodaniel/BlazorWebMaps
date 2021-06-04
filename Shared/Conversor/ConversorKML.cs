using BlazorWebMaps.Shared.Mapa;
using Microsoft.AspNetCore.Components.Forms;
using SharpKml.Dom;
using SharpKml.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebMaps.Shared.Conversor
{
    public class ConversorKML
    {
        public async Task<Dados> ObterCoordenadas(IBrowserFile file)
        {
            try
            {
                var Marcacao = new Dados();

                if (file != null)
                {
                    var memoryStream = await MontarMemoryStream(file);

                    var Bytes = MontarBytesArray(memoryStream);

                    var XML = ConverterStringKML(Bytes);

                    KmlFile FileKML;

                    using (var stream = new MemoryStream(ASCIIEncoding.UTF8.GetBytes(XML)))
                    {
                        FileKML = KmlFile.Load(stream);
                    }

                    var Place = FileKML.Root.Flatten().OfType<Placemark>().ToList();

                    Marcacao.fazenda = !Equals(Place.FirstOrDefault(),null) ? Place.FirstOrDefault().Name : "Fazenda não identificada !";

                    foreach (var poly in FileKML.Root.Flatten().OfType<Polygon>().ToList())
                    {
                        var OuterBoundaryList = poly.Flatten().OfType<OuterBoundary>().ToArray();

                        foreach (var outerBoundaryItem in OuterBoundaryList)
                        {
                            var LinearRingList = outerBoundaryItem.Flatten().OfType<LinearRing>().ToArray();

                            foreach (var linearRingItem in LinearRingList)
                            {
                                var CoordinateCollectionList = linearRingItem.Flatten().OfType<CoordinateCollection>().ToArray();

                                foreach (var coordinateCollectionItem in CoordinateCollectionList)
                                {
                                    var PointsList = coordinateCollectionItem.Select(x => x).ToList();

                                    foreach (var pointsItem in PointsList)
                                    {
                                        var mapa = new Mapas();

                                        mapa.latitude = pointsItem.Latitude;
                                        mapa.longitude = pointsItem.Longitude;

                                        Marcacao.Coordenadas.Add(mapa);
                                    }
                                }
                            }
                        }
                    }
                }

                return Marcacao;
            }
            catch (Exception erro)
            {
                var msgErro = erro.Message;

                return null;
            }
        }

        private string ConverterStringKML(byte[] bytes)
        {
            var enc = new ASCIIEncoding();

            return new ASCIIEncoding().GetString(bytes);// enc.GetString(bytes);
        }

        private byte[] MontarBytesArray(MemoryStream memoryStream) =>
            memoryStream.ToArray();

        private async Task<MemoryStream> MontarMemoryStream(IBrowserFile file)
        {
            var memoryStream = new MemoryStream();

            Stream stream = file.OpenReadStream();

            await stream.CopyToAsync(memoryStream);

            stream.Close();

            memoryStream.Close();

            return memoryStream;
        }
    }
}
