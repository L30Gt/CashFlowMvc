using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashFlowMvc.Models.Enuns;
using CashFlowMvc.Models;

namespace CashFlowMvc.Models
{
    public class LancamentoViewModel
    {
        public int Id { get; set; }
        public TipoLancamentoEnum TipoLancamento { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime DataLancamento { get; set; }


        public List<LancamentoCategoriaViewModel> LancamentoCategorias { get; set; }

    }
}