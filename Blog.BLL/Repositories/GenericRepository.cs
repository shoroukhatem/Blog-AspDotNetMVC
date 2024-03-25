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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly BlogDbContext _context;

        public GenericRepository(BlogDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Attach(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Detach(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public IEnumerable<T> GetAll()
       => _context.Set<T>().ToList();

        public T GetById(int? id)
         => _context.Set<T>().Find(id);

        public void Update(T entity)
        {

            _context.Set<T>().Update(entity);
            // return _context.SaveChanges();
        }
    }
}
