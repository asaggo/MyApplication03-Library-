using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MyApplication03_Library_.Context;
using MyApplication03_Library_.Models;

namespace MyApplication03_Library_.Controllers
{
    public class HomeController : Controller
    {
        //to access to the database (tables, views)
        private LibraryDB db = new LibraryDB();

        // GET: Home
        public ActionResult Index()
        {
            int maxListCount = 5;
            int pageNum = 1;
            string keyword = Request.QueryString["keyword"] ?? string.Empty;
            string searchOption = Request.QueryString["searchOption"] ?? string.Empty;
            int totalNum = 0;

            if (Request.QueryString["page"] != null)
                pageNum = Convert.ToInt32(Request.QueryString["page"]);

            var books = new List<Book>();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                books = db.Books.ToList();
                totalNum = books.Count();
            }
            else
            {
                //search word, search option
                switch (searchOption)
                {
                    case "title":
                        books = db.Books.Where(x => x.title.Contains(keyword)).ToList();
                        totalNum = books.Count();
                        break;
                    case "writer":
                        books = db.Books.Where(x => x.writer.Contains(keyword)).ToList();
                        totalNum = books.Count();
                        break;
                    case "publisher":
                        books = db.Books.Where(x => x.publisher.Contains(keyword)).ToList();
                        totalNum = books.Count();
                        break;
                }
            }

            books = books.OrderBy(x => x.book_num)
                    .Skip((pageNum - 1) * maxListCount)
                    .Take(maxListCount).ToList();

            ViewBag.Page = pageNum;
            ViewBag.TotalNum = totalNum;
            ViewBag.MaxListCount = maxListCount;
            ViewBag.SearchOption = searchOption;
            ViewBag.Keyword = keyword;
            

            return View(books); // <- db.Books = access to the table
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "book_num,title,writer,summary,publisher,published_date")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "book_num,title,writer,summary,publisher,published_date")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
