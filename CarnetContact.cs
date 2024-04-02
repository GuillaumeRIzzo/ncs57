using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionContact
{
    internal class CarnetContact
    {
        private List<Contact> contacts = [];

        public void AddContact(Contact newcontact)
        {
            contacts.Add(newcontact);
        }

        public void ShowAllContact()
        {
            foreach (var contact in contacts)
            {
                Console.WriteLine("Nom : " + contact.Nom + "\nPrénom : " + contact.Prenom + "\nE-mail : " + contact.Email + "\nTéléphone : " + contact.Phone);
            }
        }

        public Contact SearchContactByName(string nom)
        {
            return contacts.Find(contact => contact.Nom.Equals(nom));
        }

        public void RemoveContact(string nom, string prenom)
        {
            var contactASupprimer = contacts.Find(c => c.Nom.Equals(nom, StringComparison.OrdinalIgnoreCase) && c.Prenom.Equals(prenom, StringComparison.OrdinalIgnoreCase));
            if (contactASupprimer != null)
            {
                contacts.Remove(contactASupprimer);
                Console.WriteLine("Contact supprimé avec succès.");
            }
            else
            {
                Console.WriteLine("Aucun contact trouvé avec ce nom et prénom.");
            }
        }
    }
}
