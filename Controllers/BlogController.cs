using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogApp.Models;


namespace BlogApp.Controllers
{
    public class BlogController : Controller
    {
        BlogRepository blogRepository = new BlogRepository();
        
        //
        // 


        public ActionResult GetPosts()
        {
            var posts = blogRepository.GetPosts();
            return Json(posts, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Blog/UserPosts/1

        public ActionResult UserPosts(int id)
        {
            var posts = blogRepository.FindPostsByUserId(id);

            return View(posts);
        }

        //
        // GET: /Blog/PostUser

        public ActionResult PostUser()
        {
            return View();
        }

        //
        // POST: /Blog/PostUser

        [HttpPost]
        public ActionResult PostUser(PostUser postUser)
        {
            if (ModelState.IsValid)
            {   
                blogRepository.AddPostUser(postUser);
                blogRepository.Save();

                string name = postUser.Name;
                Session["Name"] = name;
                Session["UserId"] = postUser.PostUserId;
                return RedirectToAction("Page");
            }
            return View(postUser);
        }


        //
        // GET: /Blog/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Blog/Create

        [HttpPost]
        public ActionResult Create(Post post)
        {
            if(ModelState.IsValid)
            {
                post.Name = Session["Name"].ToString();
                post.DateCreated = DateTime.Now;
                int id = Convert.ToInt32(Session["UserId"]);
                post.UserId = id;
           

                blogRepository.Add(post);
                blogRepository.Save();

                return RedirectToAction("Page");
            }
            return View(post);

        }
        //
        // GET: /Blog/Edit/2

        public ActionResult Edit(int id)
        {
            Post post = blogRepository.GetPost(id);

            return View(post);
        }

        //
        // POST: /Blog/Edit/2

        [HttpPost]
        public ActionResult Edit(int id, FormCollection formValues)
        {

            Post post= blogRepository.GetPost(id);
            if (TryUpdateModel(post))
            {
                blogRepository.Save();
                return RedirectToAction("Details", new { id = post.PostId });
            }
            return View(post);
        }

        //
        // GET: /Blog/Details/2

        public ActionResult Details(int id)
        {
            Post post = blogRepository.GetPost(id);

            if (post == null)
                return View("NotFound");
            else
                return View(post);
        }

        //
        // HTTP GET: /Blog/Delete/1

        public ActionResult Delete(int id)
        {

            Post post = blogRepository.GetPost(id);

            if (post == null)
                return View("NotFound");
            else
                return View(post);
        }

        //
        // HTTP POST: /Blog/Delete/1

        [HttpPost]
        public ActionResult Delete(int id, string confirmButton)
        {

            Post post = blogRepository.GetPost(id);

            if (post == null)
                return View("NotFound");
            blogRepository.Delete(post);
            blogRepository.Save();

            return View("Deleted");
        }

        //
        // POST: /Blog/LogOut

        
        public ActionResult LogOut()
        {
            Session["Name"] = null;
            return RedirectToAction("Page");
        }
       
    }
}