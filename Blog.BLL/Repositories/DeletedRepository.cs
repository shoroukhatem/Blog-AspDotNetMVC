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
    public class DeletedRepository : GenericRepository<DeletedPost>, IDeletedRepository
    {
        private readonly BlogDbContext _context;
        public DeletedRepository(BlogDbContext context) : base(context)
        {
            _context = context;
        }

        public void DeleteDeletedPosts()
        {
            var deletedPost = _context.DeletedPosts.Where(x => x.DeletedAt <= DateTime.Now.AddMonths(-1));
            foreach (var post in deletedPost)
            {
                _context.DeletedPosts.Remove(post);
            }
            _context.SaveChanges();
        }

        public DeletedPost GetByID(int? id)
        {
            if (id == null)
            {
                return null; // Or throw an exception or handle the null case differently
            }
            return _context.DeletedPosts.Include("PostTags.tag").FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<DeletedPost> getUserPosts(string id)
        {
            var result = _context.DeletedPosts.Where(x => x.UserId == id);
            return result;
        }

       
    }
}
