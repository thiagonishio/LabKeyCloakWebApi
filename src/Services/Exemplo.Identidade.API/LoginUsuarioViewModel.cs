using System;
using System.ComponentModel.DataAnnotations;

namespace Exemplo.Identidade.API
{
    public class LoginUsuarioViewModel
    {   
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}
