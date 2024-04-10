using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Watchlist.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Watchlist.Models;

namespace Watchlist.Controllers
{
    [Authorize]
    public class FilmsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Utilisateur> _gestionnaire;

        public FilmsController(ApplicationDbContext context, UserManager<Utilisateur> gestionnnaire)
        {
            _context = context;
            _gestionnaire = gestionnnaire;
        }

        [HttpGet]
        public async Task<string> RecupererIdUtilisateurCourant()
        {
            Utilisateur utilisateur = await GetCurrentUserAsync();
            return utilisateur?.Id;
        }

        private Task<Utilisateur> GetCurrentUserAsync() =>
        _gestionnaire.GetUserAsync(HttpContext.User);

        // GET: Films
        public async Task<IActionResult> Index()
        {
            var idUtilisateur = await RecupererIdUtilisateurCourant();
            var modele = await _context.Film.Select(x =>
                    new ModeleVueFilm
                    {
                        IdFilm = x.Id,
                        Titre = x.Titre,
                        Annee = x.Annee
                    }).ToListAsync();
            foreach (var item in modele)
            {
                var m = await _context.FilmUtilisateur.FirstOrDefaultAsync(x =>
                           x.IdUtilisateur == idUtilisateur && x.IdFilm == item.IdFilm);
                if (m != null)
                {
                    item.PresentDansListe = true;
                    item.Note = m.Note;
                    item.Vu = m.Vu;
                }
            }
            return View(modele);
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // GET: Films/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titre,Annee")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titre,Annee")] Film film)
        {
            if (id != film.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Film.FindAsync(id);
            if (film != null)
            {
                _context.Film.Remove(film);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _context.Film.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<JsonResult> AjouterSupprimer(int id, int valret)
        {
            var idUtilisateur = await RecupererIdUtilisateurCourant();
            if (valret == 1)
            {
                // s'il existe un enregistrement dans FilmsUtilisateur qui contient à la fois l'identifiant de l'utilisateur
                // et celui du film, alors le film existe dans la liste de films et peut
                // être supprimé
                var film = _context.FilmUtilisateur.FirstOrDefault(x =>
                        x.IdFilm == id && x.IdUtilisateur == idUtilisateur);
                if (film != null)
                {
                    _context.FilmUtilisateur.Remove(film);
                    valret = 0;
                }

            }
            else
            {
                // le film n'est pas dans la liste de films, nous devons donc
                // créer un nouvel objet FilmUtilisateur et l'ajouter à la base de données.
                var filmUser = new FilmUtilisateur
                {
                    IdUtilisateur = idUtilisateur,
                    IdFilm = id,
                    Vu = false,
                    Note = 0,
                    Film = _context?.Film?.FirstOrDefault(x => x.Id == id),
                    User = (Utilisateur) _context?.Users?.FirstOrDefault(x => x.Id == idUtilisateur)
                };
                _context.FilmUtilisateur.Add(filmUser);
                valret = 1;
            }
            // nous pouvons maintenant enregistrer les changements dans la base de données
            await _context.SaveChangesAsync();
            // et renvoyer notre valeur de retour (-1, 0 ou 1) au script qui a appelé
            // cette méthode depuis la page Index
            return Json(valret);
        }
    }
}
