using Blog.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Context
{
	public class BlogDbContext: IdentityDbContext<ApplicationUser>
	{
        public BlogDbContext(DbContextOptions<BlogDbContext>options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Tag>().HasIndex(u => u.Name).IsUnique();
            builder.Entity<PostTag>().HasKey(x => new {x.TagId,x.PostId});
            base.OnModelCreating(builder);
        }
        public DbSet<Tag>Tags { get; set; }
        public DbSet<Post>Posts { get; set; }
        public DbSet<DeletedPost>DeletedPosts { get; set; }
        public DbSet<PostTag>PostTags { get; set; }
       // public DbSet<PostTag>DeletedPostTags { get; set; }

    }
}
