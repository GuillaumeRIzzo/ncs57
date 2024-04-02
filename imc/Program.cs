// See https://aka.ms/new-console-template for more information
using System;

namespace Vde
{
   class Program
   {
        static void Main(string[] args)
        {
            double taille;
            double poid;

            /*Console.WriteLine("Entrez votre taille");
            string? enter = Console.ReadLine();*/

            try
            {
                Console.WriteLine("Entrez votre taille");
                taille = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Entrez votre poid");
                poid = Convert.ToDouble(Console.ReadLine());

                Vde.Imc imc = new Imc(taille, poid);

                Console.WriteLine(imc.Result());
                Console.ReadLine();
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }

            /*while (!double.TryParse(enter, out _)) 
            {
                Console.WriteLine("Entrez votre taille en chiffre numérique");
                enter = Console.ReadLine();
            }
            taille = Convert.ToDouble(enter);

            Console.WriteLine("Entrez votre poid");
            enter = Console.ReadLine();

            while (!double.TryParse(enter, out _)) 
            {
                Console.WriteLine("Entrez votre poid en chiffre numérique");
                enter = Console.ReadLine();
            
            }
                poid = Convert.ToDouble(enter);
                Vde.Imc imc = new Imc(taille, poid);

                Console.WriteLine(imc.Result());
                Console.ReadLine();
            */

        }
    }
}