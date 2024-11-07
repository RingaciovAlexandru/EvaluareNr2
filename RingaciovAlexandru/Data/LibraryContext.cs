using Microsoft.EntityFrameworkCore;
using NumePrenume.Models;
using RingaciovAlexandru.Models;
using System.Collections.Generic;

namespace NumePrenume.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
