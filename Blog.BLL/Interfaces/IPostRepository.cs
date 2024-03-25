using Blog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        IEnumerable<Post> getUserPosts(string id);
        IEnumerable<Post> getPinnedPosts();
        Post GetByID(int? id);
    }
}
