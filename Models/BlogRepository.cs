using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogApp.ViewModels;


namespace BlogApp.Models
{
    public class BlogRepository : IBlogRepository
    {
        BlogContext db = new BlogContext();

        // Query Methods

        public List<MyCounts> GetPopularTopics(int count)
        {
            var posts = from p in db.Posts
                        group p by p.Topic into myGroup
                        orderby myGroup.Count()
                        descending
                        select new MyCounts
                        {
                            Topic = myGroup.Key,
                            Count = myGroup.Count()
                        };
            return posts.Take(count).ToList();
        }
        public List<MyCounts> GetAllTopics()
        {
            var posts = from p in db.Posts
                        group p by p.Topic into myGroup
                        orderby myGroup.Count()
                        descending
                        select new MyCounts
                        {
                            Topic = myGroup.Key,
                            Count = myGroup.Count()
                        };
            return posts.ToList();
        }

        public List<Post> GetPosts()
        {
            return (from p in db.Posts
                    orderby p.DateCreated descending
                    select p).Take(10).ToList();
        }

        public List<Post> UpdatePosts(int val, string topic)
        {
            return (from p in db.Posts
                    where p.PostId > val && p.Topic == topic
                    orderby p.DateCreated descending
                    select p).ToList();
        }
        
        

        public IQueryable<Post> FindAllPosts()
        {
            return db.Posts;
        }

        public List<Post> GetRecentPosts(string topic, int id)
        {
            int page_limit = 10;

            return (from d in db.Posts
                    where d.Topic == topic
                    orderby d.DateCreated
                    descending
                    select d).Skip((id - 1) * page_limit).Take(page_limit).ToList();

        }

        public IQueryable<Post> FindRecentPosts(int id)
        {
            int count = (from d in db.Posts select d).Count();
            int page_limit = 10;

            return (from d in db.Posts
                    orderby d.DateCreated
                    descending
                    select d).Skip((id - 1) * page_limit).Take(10);
            
        }

        public IQueryable<Post> FindPostsByUserId(int id)
        {
            return (from d in db.Posts
                    where d.UserId == id
                    select d);
        }
    
        public int FindCount(string topic)
        {
            int count = (from d in db.Posts 
                         where d.Topic == topic
                         select d).Count();
            return count;
        }

        public int FindUserCount()
        {
            int count = (from d in db.PostUsers select d).Count();
            return count;
        }

        public Post GetPost(int id)
        {
            return db.Posts.Find(id);
        }

        public PostUser GetPostUser(int id)
        {
            return db.PostUsers.Find(id);
        }

        public Vote FindVoteById(int id, int sId, string uName)
        {
            Vote vote = db.Votes.OrderByDescending(s => s.VoteId)
                        .FirstOrDefault(d => d.PostId == id && (d.SessionId == sId || d.Username == uName));
            return vote;
        }

        //Insert/Delete Methods

        public void Add(Post post)
        {
            db.Posts.Add(post);
        }

        public void AddVote(Vote vote)
        {
            db.Votes.Add(vote);
        }

        public void AddPostUser(PostUser postUser)
        {
            db.PostUsers.Add(postUser);
        }


        public void Delete(Post post)
        {
            db.Posts.Remove(post);
        }

        //Save Method

        public void Save()
        {
            db.SaveChanges();
        }



    }
}