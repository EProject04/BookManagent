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
        public DbSet<BookFollow> BookFollows { get; set; }
        public DbSet<CategoryBook> CategoryBooks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }

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
                    .HasForeignKey<UserProfile>(a => a.AuthorId)
                    .HasConstraintName("FK_UserProfile_Author");
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(d => d.Roles)
                    .WithMany(p => p.Users)
                     .HasForeignKey(a => a.RoleId)
                    .HasConstraintName("FK_User_Role");

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
                entity.HasOne(f => f.Books)
                    .WithMany(b => b.Followings)
                    .HasForeignKey(f => f.BookId)
                    .HasConstraintName("FK_Following_Book");

                entity.HasOne(f => f.UserProfiles)
                    .WithMany(u => u.Followings)
                    .HasForeignKey(f => f.UserId)
                    .HasConstraintName("FK_Following_UserProfiles");
            });
            modelBuilder.Entity<BookFollow>(entity =>
            {
                entity.HasOne(d => d.Authors)
                    .WithMany(p => p.BookFollows)
                    .HasForeignKey(a => a.AuthorId)
                    .HasConstraintName("FK_AuthorBook_Authors");

                entity.HasOne(d => d.Books)
                    .WithMany(p => p.BookFollows)
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
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, RoleName = "Admin", Description="Admin" },
                new Role { Id = 2, RoleName = "Author", Description= "Author" },
                new Role { Id = 3, RoleName = "User", Description="User" }
                );
            modelBuilder.Entity<User>().HasData(
                new User { Id=1, Username="SA", Password="1", Email="SuperAdmin@gmail.com", RoleId = 1}
                );
            modelBuilder.Entity<UserProfile>().HasData(
                new UserProfile { Id = 1, FistName="Super", LastName="Admin", Address="Ha noi", Email = "SuperAdmin@gmail.com" }
                );
        }
    }
}
