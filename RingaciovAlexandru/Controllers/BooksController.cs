using Microsoft.AspNetCore.Mvc;
using NumePrenume.Data;
using RingaciovAlexandru.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NumePrenume.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
        }

        // Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Denumire,Autor,Editura,Pret")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Denumire,Autor,Editura,Pret")] Book book)
        {
            if (id != book.ID) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Filters
        public IActionResult FilterByPrice()
        {
            var books = _context.Books.Where(b => b.Pret >= 150 && b.Pret <= 500).ToList();
            return View("Index", books);
        }

        public IActionResult FilterByAuthor()
        {
            var books = _context.Books.Where(b => !IsVowel(b.Autor[0])).ToList();
            return View("Index", books);
        }

        public IActionResult FilterByMaxPrice()
        {
            var maxPrice = _context.Books.Max(b => b.Pret);
            var books = _context.Books.Where(b => b.Pret == maxPrice).ToList();
            return View("Index", books);
        }

        public IActionResult FilterByMinPrice()
        {
            var minPrice = _context.Books.Min(b => b.Pret);
            var books = _context.Books.Where(b => b.Pret == minPrice).ToList();
            return View("Index", books);
        }

        private bool IsVowel(char ch)
        {
            return "AEIOUaeiou".IndexOf(ch) >= 0;
        }
    }
}
