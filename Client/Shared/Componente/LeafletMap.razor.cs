using BlazorWebMaps.Shared.Configs;
using BlazorWebMaps.Shared.Mapa;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWebMaps.Client.Shared.Componente
{
    public class LeafletMapBase : ComponentBase
    {
        #region Inject
        [Inject]
        public IJSRuntime JS { get; set; }
        #endregion

        #region Propriedades
        public Configuracao Config { get; set; }
        public List<Mapas> MarcacaoUm { get; set; }
        public List<Mapas> MarcacaoDois { get; set; }
        public List<Mapas> MarcacaoTres { get; set; }
        #endregion

        public LeafletMapBase()
        {
            Config = new Configuracao();
            MarcacaoUm = MontarPrimeiraMarcacao();
            MarcacaoDois = MontarSegundaMarcacao();
            MarcacaoTres = MontarTerceiraMarcacao();
        }

        private List<Mapas> MontarPrimeiraMarcacao()
        {
            var result = new List<Mapas>();

            result.Add(new Mapas(-19.98006230129525, -48.75050174688201));
            result.Add(new Mapas(-19.97988123958509, -48.75434365592279));
            result.Add(new Mapas(-19.9793630473937, -48.75421220552353));
            result.Add(new Mapas(-19.97865956100201, -48.75435150997696));
            result.Add(new Mapas(-19.97835151603908, -48.75485300430238));
            result.Add(new Mapas(-19.97840973134366, -48.75523719368764));
            result.Add(new Mapas(-19.97749563091907, -48.75575497765626));
            result.Add(new Mapas(-19.97660641229459, -48.7561052826625));
            result.Add(new Mapas(-19.97620714225813, -48.75621973235034));
            result.Add(new Mapas(-19.97587182653603, -48.75610769741199));
            result.Add(new Mapas(-19.97571794126815, -48.75584458160029));
            result.Add(new Mapas(-19.97534333749529, -48.75583920888977));
            result.Add(new Mapas(-19.97502119851406, -48.75547384423003));
            result.Add(new Mapas(-19.97453498187801, -48.7553121131579));
            result.Add(new Mapas(-19.97397819567833, -48.75520425799537));
            result.Add(new Mapas(-19.97344338745764, -48.75527396774693));
            result.Add(new Mapas(-19.97303789236807, -48.75528244403509));
            result.Add(new Mapas(-19.97286663380627, -48.75510222766017));
            result.Add(new Mapas(-19.97311412880019, -48.75424994890075));
            result.Add(new Mapas(-19.97355105456743, -48.75265649343087));
            result.Add(new Mapas(-19.97432851828836, -48.7497607455932));
            result.Add(new Mapas(-19.98006230129525, -48.75050174688201));

            return result;
        }

        private List<Mapas> MontarSegundaMarcacao()
        {
            var result = new List<Mapas>();

            result.Add(new Mapas(-19.97897341617843, -48.7500842244343));
            result.Add(new Mapas(-19.97845237199517, -48.74401734469281));
            result.Add(new Mapas(-19.97750390982696, -48.74384710913308));
            result.Add(new Mapas(-19.97550152094561, -48.74380996418343));
            result.Add(new Mapas(-19.97467413205581, -48.74381343786755));
            result.Add(new Mapas(-19.97453436258367, -48.74425098614498));
            result.Add(new Mapas(-19.97416602830711, -48.7476071442539));
            result.Add(new Mapas(-19.9743308441694, -48.74845055672814));
            result.Add(new Mapas(-19.97442570392741, -48.74959122149464));
            result.Add(new Mapas(-19.97897341617843, -48.7500842244343));

            return result;
        }

        private List<Mapas> MontarTerceiraMarcacao()
        {
            var result = new List<Mapas>();

            result.Add(new Mapas(-19.97905865893481, -48.7500970484087));
            result.Add(new Mapas(-19.97996307449961, -48.75027507472177));
            result.Add(new Mapas(-19.9811020746387, -48.74983458222321));
            result.Add(new Mapas(-19.98389042524932, -48.74852924713382));
            result.Add(new Mapas(-19.98346366005398, -48.7445897186723));
            result.Add(new Mapas(-19.98318148872516, -48.74450317239178));
            result.Add(new Mapas(-19.9818524302237, -48.74477537426722));
            result.Add(new Mapas(-19.97854631377889, -48.744053340756));
            result.Add(new Mapas(-19.97905865893481, -48.7500970484087));

            return result;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JS.InvokeVoidAsync("InicializaMapa");
                await JS.InvokeVoidAsync("ConfiguracaoMapa", Config.Attribution, Config.MinZoom, Config.MaxZoom);
            }
        }
        
        public async Task AplicarMarcacoes()
        {
            await JS.InvokeVoidAsync("CriaMarcacaoMapa", new Dados("Fazenda 1", "001", "100"), MarcacaoUm);
            await JS.InvokeVoidAsync("CriaMarcacaoMapa", new Dados("Fazenda 1", "002", "200"), MarcacaoDois);
            await JS.InvokeVoidAsync("CriaMarcacaoMapa", new Dados("Fazenda 1", "003", "300"), MarcacaoTres);
        }

        public async Task RemoverMarcacoes() 
        {
            await JS.InvokeVoidAsync("RemoverMarcacaoMapa");
        }

        public async Task AplicarMarcacoesDois() =>
            await JS.InvokeVoidAsync("CriaMarcacaoMapa2");


    }
}
