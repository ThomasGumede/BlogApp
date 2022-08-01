using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BlogApp.Models;

namespace BlogApp.Data
{
    public class MovieRepository: IMovieRepository
    {
        public MovieRepository(BlogAppDbContext context, IWebHostEnvironment environment)
        { 
            _context = context ?? throw new NullReferenceException();
            _env = environment; 
        }

        private readonly BlogAppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public IEnumerable<Post> BlogList(int? categoryId)
        {
            if(categoryId != null)
            {
                return _context.Posts.Where(category => category.CategoryId == categoryId).Include(c => c.Category).AsNoTracking().ToList();
            }else{
                return _context.Posts.Include(c => c.Category).AsNoTracking().ToList();
            }
        }

        public Post GetBlog(int id)
        {
            
            return _context.Posts.Include(category => category.Category).Include(comment => comment.Comments).FirstOrDefault(post => post.Id == id);
        }

        public void ModifyBlog(Post post)
        {
             _context.Entry(post).State = EntityState.Modified;
        }

        public void CreateBlog(Post post)
        {
            _context.Posts.Add(post);
        }

        public void DeleteBlog(int id)
        {
            Post post = _context.Posts.Find(id);
        if (post.Photo != null)
        {
            if (System.IO.File.Exists($"{_env.WebRootPath}/{post.Photo}")) System.IO.File.Delete($"{_env.WebRootPath}/{post.Photo}");
        }
            _context.Posts.Remove(post);

        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}