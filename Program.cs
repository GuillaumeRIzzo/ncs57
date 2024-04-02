// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

namespace GestionContact
{
    class Program
    {
        static void Main(string[] args)
        {
            CarnetContact carnet = new CarnetContact();
            try
            {
                string? entrer;

                do
                {
                    Console.WriteLine("Que voulez-vous faire : \nAjoutez un contact : add ");
                    Console.WriteLine("Affichez tous les contacts : show");
                    Console.WriteLine("Rechercher un contact : search");
                    Console.WriteLine("Supprimer un contact : remove");
                    Console.WriteLine("Quittez : leave ?");

                    entrer = Console.ReadLine();

                    switch (entrer)
                    {
                        case "add":
                            Console.WriteLine("Saisir le nom du contact");
                            string nom = Console.ReadLine();

                            Console.WriteLine("Saisir le prénom du contact");
                            string prenom = Console.ReadLine();

                            Console.WriteLine("Saisir l'e-mail du contact");
                            string email = Console.ReadLine();

                            Console.WriteLine("Saisir le téléphone du contact");
                            string phone = Console.ReadLine();

                            Contact contact = new Contact(nom, prenom, email, phone);
                            carnet.AddContact(contact);
                            Console.WriteLine("Le contact a été ajouté.");
                            break;
                        case "show":
                            carnet.ShowAllContact();
                            break;
                        case "search":
                            Console.WriteLine("Saisir le nom du contact à chercher");
                            string nomChercher = Console.ReadLine();
                            carnet.SearchContactByName(nomChercher);
                            break;
                        case "remove":
                            Console.WriteLine("Saisir le nom du contact à supprimer");
                            string nomSuppression = Console.ReadLine();
                            Console.WriteLine("Entrez le prénom du contact à supprimer:");
                            string prenomSuppression = Console.ReadLine();
                            carnet.RemoveContact(nomSuppression, prenomSuppression);
                            break;
                        case "leave":
                            Console.WriteLine("Fin du programme.");
                            break;
                        default:
                            Console.WriteLine("Choix invalide. Veuillez sélectionner une option valide.");
                            break;
                    }
                }
                while (entrer != null && entrer != "leave");
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }
            catch  (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
