using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashFlowMvc.Models
{
    public class LancamentoCategoriaViewModel
    {
        public int LancamentoId { get; set; }
        public LancamentoViewModel? Lancamento { get; set; }
        public int CategoriaId { get; set; }
        public CategoriaViewModel? Categoria { get; set; }
    }
}