using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogApp.Models;

namespace BlogApp.Controllers
{
    public class HomeController : Controller
    {
        BlogRepository blogRepository = new BlogRepository();
        
        public ActionResult Index()
        {
            var topics = blogRepository.GetPopularTopics(5);
            return View(topics);
        }

        public ActionResult About()
        {
            return View();
        }

        //
        // GET: /Home/GetRecentPosts

        public ActionResult GetRecentPosts(string topic, int id)
        {
            var posts = blogRepository.GetRecentPosts(topic, id);
            return Json(posts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BlogTest(int id)
        {
            return View();
        }

        // 
        // GET: /Home/LoginForm

        public ActionResult LoginForm()
        {
            return PartialView("LoginForm");
        }

        //
        // POST: /Home/LoginForm

        [HttpPost]
        public ActionResult LoginForm(string name)
        {
            PostUser postUser = new PostUser()
            {
                Name = name,
                
            };
            
            blogRepository.AddPostUser(postUser);
            blogRepository.Save();

            Session["Name"] = name;
            Session["UserId"] = postUser.PostUserId;
            return Json(true);

        }

        //
        // GET: /Home/AddPost

        public ActionResult AddPost()
        {
            return PartialView();
        }

        //
        // POST: /Home/AddPost

        [HttpPost]
        public ActionResult AddPost(string body, string topic)
        {
            Post post = new Post(){
                Body = body,
                Topic = topic,
                Votes = 0,
            };
            if (Request.IsAuthenticated)
            {
                post.Name = User.Identity.Name;
                post.UserId = 0;
            }
            else
            {
                post.Name = Session["Name"].ToString();
                int id = Convert.ToInt32(Session["UserId"]);
                post.UserId = id;
            }
            
            post.DateCreated = DateTime.Now;
            

            blogRepository.Add(post);
            blogRepository.Save();

            return Json(true);

        }

        //
        // GET: /Home/AddPostTopic

        public ActionResult AddPostTopic()
        {
            return PartialView();
        }

        //
        // POST: /Home/AddPostTopic

        [HttpPost]
        public ActionResult AddPostTopic(string body, string topic)
        {
            Post post = new Post()
            {
                Body = body,
                Topic = topic,
                Votes = 0
            };
            post.DateCreated = DateTime.Now;        
            post.Name = User.Identity.Name;
            post.UserId = 0;

            blogRepository.Add(post);
            blogRepository.Save();

            return Json(true);

        }

        public ActionResult UpdatePosts(int val, string topic)
        {
            var posts = blogRepository.UpdatePosts(val, topic);
            if (posts == null)
            {
                return Json(false);
            }
            else
            {
                return Json(posts, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // POST: /Home/VoteChange
        [HttpPost]
        public ActionResult VoteChange(int id, int change)
        {
            var post = blogRepository.GetPost(id);
            post.Votes = post.Votes + change;

            Vote vote = new Vote()
                {
                    PostId = id,
                    Votes = change
                };
            if (Request.IsAuthenticated)
            {
                var uName = User.Identity.Name;
                vote.Username = uName;
            }
            else
            {
                int sId = Convert.ToInt32(Session["UserId"]);
                vote.SessionId = sId;
            }
            blogRepository.AddVote(vote);
            blogRepository.Save();
            return Json(post);
        }


        //
        // POST: /Home/VoteReset
        [HttpPost]
        public ActionResult VoteReset(int id, int change)
        {
            var post = blogRepository.GetPost(id);
            post.Votes = post.Votes + change;

            Vote vote = new Vote()
            {
                PostId = id,
                Votes = 0
            };
            if (Request.IsAuthenticated)
            {
                var uName = User.Identity.Name;
                vote.Username = uName;
            }
            else
            {
                int sId = Convert.ToInt32(Session["UserId"]);
                vote.SessionId = sId;
            }
            blogRepository.AddVote(vote);
            blogRepository.Save();
            return Json(post);
        }

        public ActionResult FindVoteById(int id, int sId, string uName)
        {
            Vote vote = blogRepository.FindVoteById(id, sId, uName);
            if (vote != null)
            {
                return Json(vote, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // POST: /Home/LogOut

        [HttpPost]
        public ActionResult LogOut()
        {
            Session["Name"] = null;
            Session["UserId"] = null;
            return Json(true);
        }

        
    }
}
