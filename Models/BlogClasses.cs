using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class PostUser
    {
        public int PostUserId { get; set; }
        public string Name { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Topic { get; set; }
        public int UserId { get; set; }
        [StringLength(5000)]
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get; set; }
        public int Votes { get; set; }
    }

    public class Vote
    {
        public int VoteId { get; set; }
        public int Votes { get; set; }
        public int PostId { get; set; }
        public int SessionId { get; set; }
        public string Username { get; set; }
    }
}