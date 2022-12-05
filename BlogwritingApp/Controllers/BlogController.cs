using BlogwritingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogwritingApp.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        [AllowAnonymous]
        public ActionResult AllBlogs()
        {
            var context = new BlogDatabaseEntities();
            var data = context.BlogTables.ToList();
            return PartialView(data);
        }
        [AllowAnonymous]
        public ActionResult AllEditableBlogs()
        {
            if (Session["currentUser"] == null)
            {
                ViewBag.ErrorInfo = "Please login Before U Post a Blog!!!";
                return PartialView(null);
            }
            var user = Session["currentUser"] as UserTable;
            var context = new BlogDatabaseEntities();
            var blogs = context.BlogTables.Where(b => b.UserId == user.UserId).ToList();
            return PartialView(blogs);
        }

        [AllowAnonymous]
        public ActionResult AddNew()
        {
            if (Session["currentUser"] == null)
            {
                TempData["ErrorInfo"] = "Please login Before U Post a Blog!!!";
                TempData.Keep();
                return RedirectToAction("Login", "Account");
            }
            var user = Session["currentUser"] as UserTable;
            var blog = new BlogTable();
            blog.UserId = user.UserId;
            blog.UserTable = user;
            return View(blog);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddNew(BlogTable blog)
        {
            var context = new BlogDatabaseEntities();
            context.BlogTables.Add(blog);
            context.SaveChanges();
            return RedirectToAction("Index", "Account");
        }

        //public ActionResult UpdateRec(BlogTable postedData)
        //{
        //    var context = new BlogDatabaseEntities();
        //    var model = context.BlogTables.Where((blog) => blog.UserId == postedData.UserId).FirstOrDefault();
        //    if (model == null) throw new Exception("This is not found to update");
        //    model.Title = postedData.Title;
        //    model.Image = postedData.Image;
        //    model.Details = postedData.Details;
        //    context.SaveChanges();
        //    return RedirectToAction();
        //}
        [AllowAnonymous]
        public ActionResult EditBlogs(string Id)
        {
            var context = new BlogDatabaseEntities();
            var BId = int.Parse(Id);
            var selected = context.BlogTables.SingleOrDefault(b => b.BlogId == BId);
            return View(selected);
        }
        
        [AllowAnonymous]
       // [HttpPost]
        public ActionResult UpdateBlogs(BlogTable blogsTable)
        {
            var context = new BlogDatabaseEntities();
            var selected = context.BlogTables.SingleOrDefault(b => b.BlogId == blogsTable.BlogId);
            if (selected == null)
            {
                throw new System.Exception("Blog is not found");
            }
            selected.Title = blogsTable.Title;
            selected.UserId = blogsTable.UserId;
            selected.Image = blogsTable.Image;
            selected.Details = blogsTable.Details;
            
            context.SaveChanges();
            return RedirectToAction("Index","Account");
        }
        [AllowAnonymous]
        public ActionResult Delete(string id)
        {
            var context = new BlogDatabaseEntities();
            var BId = int.Parse(id);
            var model = context.BlogTables.Where((blog) => blog.BlogId == BId).FirstOrDefault();//SELECT * From EmpTable where Id = empId;
            context.BlogTables.Remove(model);
            context.SaveChanges();
            return RedirectToAction("Index", "Account");
        }
        [AllowAnonymous]
        public ActionResult userBlogs()
        {
            if (Session["currentUser"] == null)
            {
                TempData["ErrorInfo"] = "Please login Before U Post a Blog!!!";
                TempData.Keep();
                return RedirectToAction("Login", "Account");
            }
            var context = new BlogDatabaseEntities();
            var data = context.BlogTables.ToList();
            return View(data);
        }
    }
}