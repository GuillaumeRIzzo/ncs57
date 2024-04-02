using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vde
{
    class Imc
    {
        public double Taille { get; set; }
        public double Poid { get; set; }

        public Imc(double taille, double poid)
        {
            Taille = taille;
            Poid = poid;
        }

        public double Calc()
        {
            double imc = Poid / (Taille * Taille) * 10000;
            return imc;
        }

        public string Result()
        {
            string result;

            switch (this.Calc())
            {
                case > 40:
                    result = "Obésité morbide ou massive";
                    break;
                case > 35:
                    result = "Obésité sévère";
                    break;
                case > 30:
                    result = "Obésité modérée";
                    break;
                case > 25:
                    result = "Surpoid";
                    break;
                case > 18.5:
                    result = "Corpulence normale";
                    break;
                default:
                    result = "Insuffisance pondérale (maigreur)";
                    break;
            }
            return result;
        }
    }
}
