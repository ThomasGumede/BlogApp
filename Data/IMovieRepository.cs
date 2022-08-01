using System;
using System.Collections.Generic;
using System.Linq;
using BlogApp.Models;

namespace BlogApp.Data
{
    public interface IMovieRepository
    {
        IEnumerable<Post> BlogList(int? categoryId);
        Post GetBlog(int id);

        void ModifyBlog(Post post);

        void CreateBlog(Post post);

        void DeleteBlog(int id);

        void Save();
    }
}