using Blog.BLL.Interfaces;
using Blog.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly BlogDbContext _context;
        public ITagRepository TagRepository { get ; set ; }
        public IPostRepository PostRepository { get; set; }
        public IDeletedRepository DeletedPostRepository { get; set; }

        public UnitOfWork(BlogDbContext context)
        {
            _context = context;
            TagRepository = new TagRepository(context);
            PostRepository = new PostRepository(context);
            DeletedPostRepository =new DeletedRepository(context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }


    }
}
