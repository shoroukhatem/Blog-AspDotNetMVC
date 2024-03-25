using Blog.BLL.Interfaces;
using Blog.DAL.Context;
using Blog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Repositories
{
    public class TagRepository : GenericRepository<Tag>, ITagRepository
    {
        private readonly BlogDbContext _context;
        public TagRepository(BlogDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
