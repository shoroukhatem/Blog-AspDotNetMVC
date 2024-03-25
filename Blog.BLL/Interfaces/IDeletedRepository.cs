using Blog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces
{
    public interface IDeletedRepository:IGenericRepository<DeletedPost>
    {
        public IEnumerable<DeletedPost> getUserPosts(string id);
          public DeletedPost GetByID(int? id);
        public void DeleteDeletedPosts();
    }
}
