using BlogApp.Models;

namespace BlogApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(BlogAppDbContext ctx)
        {
            if (ctx.Categories!.Any())
            {
                return;
            }


            Category tech = new Category
            {
                Title = "Technology",
                Description = "Everything about Technology"
            };

            Category investing = new Category
            {
                Title = "Investing",
                Description = "Everything about Investing"
            };

            Category health = new Category
            {
                Title = "Health",
                Description = "Everything about Health"
            };

            Category sport = new Category
            {
                Title = "Sport",
                Description = "Everything about sport"
            };

            var categories = new Category[]
            {
                tech, investing, health, sport
            };

            ctx.AddRange(categories);
            ctx.SaveChanges();
        }
    }
}