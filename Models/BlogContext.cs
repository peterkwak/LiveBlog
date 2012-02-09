using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace BlogApp.Models
{
    public class BlogContext : DbContext
    {   
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostUser> PostUsers { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }

    
    
}