using BEWebtoon.Models;
using Microsoft.EntityFrameworkCore;
using BEWebtoon.Models.Domains.Interfaces;

namespace BEWebtoon.WebtoonDBContext
{
    public class WebtoonDbContext :DbContext
    {
        public WebtoonDbContext(DbContextOptions<WebtoonDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<CategoryBook> CategoryBooks { get; set; }
        public DbSet<Category> Categories { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var modified = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified ||
                            e.State == EntityState.Added ||
                            e.State == EntityState.Deleted);
            foreach (var item in modified)
            {
                switch (item.State)
                {
                    case EntityState.Modified:
                        if (item.Entity is IDateTracking modifiedEntity)
                            modifiedEntity.LastModifiedDate = DateTime.Now;
                        break;
                    case EntityState.Added:
                        if (item.Entity is IDateTracking addedEntity)
                            addedEntity.CreatedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasOne(d => d.Users)
                    .WithOne(p => p.UserProfiles)
                    .HasForeignKey<UserProfile>(u => u.Id)
                    .HasConstraintName("FK_UserProfile_User");

                entity.HasOne(d => d.Authors)
                    .WithOne(p => p.UserProfiles)
                    .HasForeignKey<UserProfile>(a => a.Id)
                    .HasConstraintName("FK_UserProfile_Author");
            });
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(d => d.UserProfiles)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(a => a.UserId)
                    .HasConstraintName("FK_Comment_UserProfiles");

                entity.HasOne(d => d.Books)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(a=> a.BookId)
                    .HasConstraintName("FK_Book_UserProfiles");

            });
            modelBuilder.Entity<Following>(entity =>
            {
                entity.HasOne(d => d.UserProfiles)
                    .WithMany(p => p.Followings)
                    .HasForeignKey(a => a.UserId)
                    .HasConstraintName("FK_Following_UserProfiles");

                entity.HasOne(d => d.Books)
                    .WithMany(p => p.Followings)
                    .HasForeignKey(a => a.BookId)
                    .HasConstraintName("FK_Following_Book");

            });
            modelBuilder.Entity<AuthorBook>(entity =>
            {
                entity.HasOne(d => d.Authors)
                    .WithMany(p => p.AuthorBooks)
                    .HasForeignKey(a => a.AuthorId)
                    .HasConstraintName("FK_AuthorBook_Authors");

                entity.HasOne(d => d.Books)
                    .WithMany(p => p.AuthorBooks)
                    .HasForeignKey(a => a.BookId)
                    .HasConstraintName("FK_AuthorBook_UserProfiles");

            });
            modelBuilder.Entity<CategoryBook>(entity =>
            {
                entity.HasOne(d => d.Categories)
                    .WithMany(p => p.CategoryBooks)
                    .HasForeignKey(a => a.CategoryId)
                    .HasConstraintName("FK_Category_CategoryBook");

                entity.HasOne(d => d.Books)
                    .WithMany(p => p.CategoryBooks)
                    .HasForeignKey(a => a.BookId)
                    .HasConstraintName("FK_Book_CategoryBook");

            });
        }
    }
}
