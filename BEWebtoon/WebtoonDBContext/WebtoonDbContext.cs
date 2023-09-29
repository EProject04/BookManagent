using BEWebtoon.Models;
using Microsoft.EntityFrameworkCore;

namespace BEWebtoon.WebtoonDBContext
{
    public class WebtoonDbContext :DbContext
    {
        public WebtoonDbContext(DbContextOptions<WebtoonDbContext> options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
    }
}
