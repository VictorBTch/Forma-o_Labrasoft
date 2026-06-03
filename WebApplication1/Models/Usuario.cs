using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Senha { get; set; }

        public Usuario() { }

        public Usuario(string Email, string Senha)
        {
            this.Email = Email;
            this.Senha = Senha;
        }

    }
}