using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public ITagRepository TagRepository { get; set; }
        public IPostRepository PostRepository { get; set; }
        public IDeletedRepository DeletedPostRepository { get; set; }
        public int Complete();
    }
}
