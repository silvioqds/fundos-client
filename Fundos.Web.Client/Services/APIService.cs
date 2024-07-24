using Fundos.Web.Client.Models;
using Fundos.Web.Client.Services.Interface;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fundos.Web.Client.Services
{
    public class APIService : IAPIService
    {
        public APIService() { }
        private const string URLApi = "https://localhost:44378/api/v1";
        public async Task<List<TipoFundo>> GetTipoFundosAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{URLApi}/tipofundo"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TipoFundo>>(apiResponse);
                }
            }
        }

        public async Task<List<Fundo>> GetFundosAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{URLApi}/fundo"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Fundo>>(apiResponse);
                }
            }
        }

        public async Task<Fundo> GetFundoAsync(string codigo)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{URLApi}/fundo/{codigo}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Fundo>(apiResponse);
                }
            }
        }

        public async Task<string> DeleteFundoAsync(string codigo)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"{URLApi}/fundo/{codigo}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<dynamic>(apiResponse);

                    return obj.message;
                }
            }
        }

        public async Task<string> UpdateFundoAsync(Fundo fundo)
        {
            var json = JsonConvert.SerializeObject(fundo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync($"{URLApi}/fundo/{fundo.Codigo}", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var responseObj = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                    if (responseObj.errors != null)
                    {
                        string errMessage = string.Empty;                        
                        foreach (var error in responseObj.errors)
                        {
                            var campo = error.Name;
                            var mensagens = error.Value;

                            errMessage = string.Concat(errMessage, " | ", string.Format("Erro no campo {0}:{1}", campo, mensagens));
                        }
                        return errMessage;
                    }
                    return responseObj.message;
                }
            }
            
        }

        public async Task<decimal> MovimentarPatrimonioAsync(string codigo, Movimentacao movimentacao)
        {
            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(movimentacao);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync($"{URLApi}/fundo/{codigo}/patrimonio", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var responseObj = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                    var novoPatrimonio = (decimal)responseObj.newpatrimonio;
                    return novoPatrimonio;
                }
            }
        }

        public async Task<string> CreateFundo(Fundo fundo)
        {
            var json = JsonConvert.SerializeObject(fundo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync($"{URLApi}/fundo", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var responseObj = JsonConvert.DeserializeObject<dynamic>(apiResponse);
                    if (responseObj.errors != null)
                    {
                        string errMessage = string.Empty;                        
                        foreach (var error in responseObj.errors)
                        {
                            var campo = error.Name;
                            var mensagens = error.Value;

                            errMessage = string.Concat(errMessage, " | ", string.Format("Erro no campo {0}:{1}", campo, mensagens));
                        }
                        return errMessage;
                    }
                    return responseObj.message;
                }
            }
        }



    }
}
