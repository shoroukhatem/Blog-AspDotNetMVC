using Blog.BLL.Interfaces;
using Blog.DAL.Context;
using Blog.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly BlogDbContext _context;
        public PostRepository(BlogDbContext context) : base(context)
        {
            _context = context;
        }

        public Post GetByID(int? id)
        {
            if (id == null)
            {
                return null; // Or throw an exception or handle the null case differently
            }
            return _context.Posts.Include("PostTags.tag").FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Post> getPinnedPosts()
        {
            return _context.Posts.Where(x=>x.Pinned==true);
        }

        public IEnumerable<Post> getUserPosts(string id)
        {
           var result = _context.Posts.Where(x=>x.UserId==id);
            return result;
        }
    }
}
