using Microsoft.AspNetCore.Identity;

namespace Watchlist.Data
{
    public class Utilisateur : IdentityUser
    {
        public Utilisateur() : base()
        {
            this.ListeFilms = new HashSet<FilmUtilisateur>();
        }

        public string Prenom { get; set; }
        public virtual ICollection<FilmUtilisateur> ListeFilms { get; set; }
    }
}
