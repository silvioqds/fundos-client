using Fundos.Web.Client.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fundos.Web.Client.Services.Interface
{
    public interface IAPIService
    {
        Task<List<TipoFundo>> GetTipoFundosAsync();

        Task<List<Fundo>> GetFundosAsync();

        Task<Fundo> GetFundoAsync(string codigo);

        Task<string> DeleteFundoAsync(string codigo);

        Task<string> UpdateFundoAsync(Fundo fundo);
        Task<string> CreateFundo(Fundo fundo);

        Task<decimal> MovimentarPatrimonioAsync(string codigo, Movimentacao movimentacao);

    }
}
