using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashFlowMvc.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        
        public string Username { get; set; } = string.Empty;
        
        public string PasswordString { get; set; } = string.Empty;
        
        public string Token { get; set; } = string.Empty;
        
    }
}