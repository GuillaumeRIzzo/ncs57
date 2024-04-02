using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContact
{
    internal class Contact
    {
        public string Nom { get; set; }

        public string Prenom { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public Contact(string nom, string prenom, string email, string phone)
        {
            Nom = nom;
            Prenom = prenom;
            Email = email;
            Phone = phone;
        }
    }
}
