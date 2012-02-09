using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Models
{
    public interface IBlogRepository
    {
        List<Post> GetPosts();
       
    }
}