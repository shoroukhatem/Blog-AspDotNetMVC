using Blog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {

        T GetById(int? id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Detach(T entity);
        void Attach(T entity);
    }
}
