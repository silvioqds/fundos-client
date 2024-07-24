using System.ComponentModel.DataAnnotations;

namespace Fundos.Web.Client.Models
{
    public class Fundo
    {
        [Required(ErrorMessage = "Codigo is required")]
        [MaxLength(20, ErrorMessage = "Codigo pode haver no máximo 20 caracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Nome is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O Nome deve ter no mínimo 3 e no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "CNPJ is required")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "CodigoTipo is required")]
        public int Codigo_Tipo { get; set; }

        public string TipoFundo { get; set; }

        [Required(ErrorMessage = "Patrimonio is required")]        
        [Range(0, 9999999999999999.99)]
        public decimal? Patrimonio { get; set; }        
    }
}
