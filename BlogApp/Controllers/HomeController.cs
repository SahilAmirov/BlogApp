using BlogApp.Context;
using BlogApp.Models.Dtos;
using BlogApp.Models.Entities;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<PostDto> list = new List<PostDto>();
            BlogAppDbContext blogAppDbContext = new BlogAppDbContext();
            List<Post> posts = blogAppDbContext.Posts.OrderByDescending(x=>x.PublishDate).ToList();
            for (int i = 0; i < posts.Count; i++)
            {
                PostDto index = new PostDto();
                index.Post = posts[i];
                index.Writer = blogAppDbContext.Writers.Where(x => x.ID == posts[i].WriterID).FirstOrDefault();
                index.Category = blogAppDbContext.Categories.Where(x=>x.ID == posts[i].CategoryID).FirstOrDefault();
                list.Add(index);
            }
            return View(list);
        }

        [HttpGet]
        public IActionResult PostDetails(int id)
        {
            BlogAppDbContext blogAppDbContext = new BlogAppDbContext();
            Post post = blogAppDbContext.Posts.Where(x => x.ID == id).FirstOrDefault();
            PostDto detail = new PostDto();
            detail.Post = post;
            detail.Writer = blogAppDbContext.Writers.Where(x => x.ID == post.WriterID).FirstOrDefault();
            detail.Category = blogAppDbContext.Categories.Where(x => x.ID == post.CategoryID).FirstOrDefault();

            return View(detail);
        }


        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Register(Writer writer)
        {
            BlogAppDbContext blogAppDbContext = new BlogAppDbContext();
            blogAppDbContext.Writers.Add(writer);
            blogAppDbContext.SaveChanges();
            HttpContext.Response.Cookies.Append("writerID", writer.ID.ToString());
            return RedirectToAction("WritersPostsEdit");
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password)
        {
            BlogAppDbContext blogAppDbContext = new BlogAppDbContext();
            Writer writer = blogAppDbContext.Writers.Where(x=>x.Email == Email && x.Password == Password).FirstOrDefault();
            if (writer != null)
            {
                HttpContext.Response.Cookies.Append("writerID", writer.ID.ToString());
                return RedirectToAction("WritersPostsEdit");
            }
            ViewBag.LoginError = "Wrong Credentials!";
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("writerID");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult WritePost()
        {
            if (HttpContext.Request.Cookies["writerID"] == null)
            {
                return RedirectToAction("login");
            }
            
            return View();
        }

        [HttpPost]
        public IActionResult WritePost(Post post)
        {
            post.WriterID = Convert.ToInt32(HttpContext.Request.Cookies["writerID"]);
            post.PublishDate = DateTime.Now;
            post.CategoryID = 1;
            BlogAppDbContext blogAppDb =new BlogAppDbContext();
            blogAppDb.Posts.Add(post);
            blogAppDb.SaveChanges();
            return RedirectToAction("WritersPostsEdit");
        }


        [HttpGet]
        [Route("home/WritersPosts/{writerID}")]
        public IActionResult WritersPosts(int writerID)
        {
            List<PostDto> list = new List<PostDto>();
            PostDto index0 = new PostDto();
            list.Add(index0);
            BlogAppDbContext blogAppDbContext = new BlogAppDbContext();
            List<Post> posts = blogAppDbContext.Posts.Where(x => x.WriterID == writerID).OrderByDescending(x => x.PublishDate).ToList();
            for (int i = 0; i < posts.Count; i++)
            {
                PostDto index = new PostDto();
                index.Post = posts[i];
                index.Category = blogAppDbContext.Categories.Where(x => x.ID == posts[i].CategoryID).FirstOrDefault();
                list.Add(index);
            }
            list[0].Writer = blogAppDbContext.Writers.Where(x => x.ID == writerID).FirstOrDefault();
            return View(list);
        }

        [HttpGet]
        [Route("home/WritersPostsEdit")]
        public IActionResult WritersPostsEdit()
        {
            int writerID = Convert.ToInt32(HttpContext.Request.Cookies["writerID"]);
            if(writerID==0)
                return RedirectToAction("Login");
            List <PostDto> list = new List<PostDto>();
            PostDto index0 = new PostDto();
            list.Add(index0);
            BlogAppDbContext blogAppDbContext = new BlogAppDbContext();
            List<Post> posts = blogAppDbContext.Posts.Where(x => x.WriterID == writerID).OrderByDescending(x => x.PublishDate).ToList();
            for (int i = 0; i < posts.Count; i++)
            {
                PostDto index = new PostDto();
                index.Post = posts[i];
                index.Category = blogAppDbContext.Categories.Where(x => x.ID == posts[i].CategoryID).FirstOrDefault();
                list.Add(index);
            }
            list[0].Writer = blogAppDbContext.Writers.Where(x => x.ID == writerID).FirstOrDefault();
            return View(list);
        }

        [HttpGet]
        public IActionResult EditPost(int id)
        {
            int writerID = Convert.ToInt32(HttpContext.Request.Cookies["writerID"]);
            BlogAppDbContext blogAppDbContext = new BlogAppDbContext();
            Post post = blogAppDbContext.Posts.Where(x => x.ID == id).FirstOrDefault();
            if(post.WriterID != writerID) { return View("Error"); }
            return View(post);
        }

        [HttpPost]
        public IActionResult EditPost(Post post)
        {
            BlogAppDbContext blogAppDbContext = new BlogAppDbContext();
            post.PublishDate = DateTime.Now;
            //post.Category = blogAppDbContext.Categories.Where(x => x.ID == post.CategoryID).FirstOrDefault();
            blogAppDbContext.Posts.Update(post);
            blogAppDbContext.SaveChanges();
            ViewBag.PostSaved = "Changes Saved";
            return RedirectToAction("WritersPostsEdit");
            //return View(post);
        }
    }
}
