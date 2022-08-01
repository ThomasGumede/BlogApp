using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data
{
    public sealed class BlogAppDbContext: DbContext
    {
        public BlogAppDbContext(){ }

        public BlogAppDbContext(DbContextOptions<BlogAppDbContext> options): base(options){ }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
            .HasOne(c => c.Category)
            .WithMany(p => p.Posts)
            .HasForeignKey(k => k.CategoryId)
            .IsRequired().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Comment>()
                .HasOne(p => p.Post)
                .WithMany(c => c.Comments)
                .HasForeignKey(k => k.PostId)
                .IsRequired().OnDelete(DeleteBehavior.Cascade);
        }

    }
}