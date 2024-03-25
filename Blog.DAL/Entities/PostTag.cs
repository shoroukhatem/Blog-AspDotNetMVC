using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Entities
{
    public class PostTag
    {
        public Post post { get; set; }
        [ForeignKey("post")]
        public int PostId { get; set; }
        public Tag tag{ get; set; }
        [ForeignKey("tag")]
        public int TagId { get; set; }
    }
}
