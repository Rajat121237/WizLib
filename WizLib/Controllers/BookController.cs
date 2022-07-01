using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;
using WizLib_Model.Models.ViewModels;

namespace WizLib.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //Eager Loding
            List<Book> objBooks = _db.Books.Include(u => u.Publisher).Include(u => u.BookAuthors).ThenInclude(u => u.Author).ToList();

            //Explicit Loading
            //List<Book> objBooks = _db.Books.ToList();
            //foreach (var book in objBooks)
            //{
            //    //book.Publisher = _db.Publishers.FirstOrDefault(x => x.Publisher_Id == book.Publisher_Id);

            //    _db.Entry(book).Reference(u => u.Publisher).Load();
            //    _db.Entry(book).Collection(u => u.BookAuthors).Load();

            //    foreach (var bookAuth in book.BookAuthors)
            //    {
            //        _db.Entry(bookAuth).Reference(u => u.Author).Load();
            //    }
            //}

            return View(objBooks);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            BookVM obj = new BookVM();
            obj.PublisherList = _db.Publishers.Select(s => new SelectListItem
                                    { 
                                        Text = s.Name,
                                        Value = s.Publisher_Id.ToString()
                                    });

            if (id == null) return View(obj);

            obj.Book = _db.Books.FirstOrDefault(b => b.Book_Id == id);
            if (obj.Book == null) return NotFound();


            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookVM obj)
        {
            if (obj.Book.Book_Id == 0)
                _db.Books.Add(obj.Book);
            else
                _db.Books.Update(obj.Book);

            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            BookVM bookVM = new BookVM();
            if (id == null) return View(bookVM);

            //Eager Loading
            bookVM.Book = _db.Books.Include(u => u.BookDetail).FirstOrDefault(b => b.Book_Id == id);

            //Explicit Loading
            //bookVM.Book = _db.Books.FirstOrDefault(b => b.Book_Id == id);
            //bookVM.Book.BookDetail = _db.BookDetails.FirstOrDefault(bd => bd.BookDetail_Id == bookVM.Book.BookDetail_Id);
            if (bookVM.Book == null) return NotFound();

            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(BookVM bookVM)
        {
            if (bookVM.Book.BookDetail.BookDetail_Id == 0)
            {
                _db.BookDetails.Add(bookVM.Book.BookDetail);
                _db.SaveChanges();

                var bookFromDb = _db.Books.FirstOrDefault(x => x.Book_Id == bookVM.Book.Book_Id);
                bookFromDb.BookDetail_Id = bookVM.Book.BookDetail.BookDetail_Id;
                _db.SaveChanges();
            }
            else
            {
                _db.BookDetails.Update(bookVM.Book.BookDetail);
                _db.SaveChanges();
            }
             return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var objFromDb = _db.Books.FirstOrDefault(x => x.Book_Id == id);
            _db.Books.Remove(objFromDb);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult PlayGround()
        {
            //var bookTemp = _db.Books.FirstOrDefault();
            //bookTemp.Price = 100;

            //var bookCollection = _db.Books;
            //double totalPrice = 0;

            //foreach (var book in bookCollection)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookList = _db.Books.ToList();
            //foreach (var book in bookList)
            //{
            //    totalPrice += book.Price;
            //}

            //var bookCollection2 = _db.Books;
            //var bookCount1 = bookCollection2.Count();

            //var bookCount2 = _db.Books.Count();

            IEnumerable<Book> bookList1 = _db.Books;
            var filteredBook1 = bookList1.Where(p => p.Price > 100).ToList();

            IQueryable<Book> bookList2 = _db.Books;
            var filteredBook2 = bookList2.Where(p => p.Price > 100).ToList();




            //Updating Related Entities
            var bookFromDb1 = _db.Books.Include(u => u.BookDetail).FirstOrDefault(X => X.Book_Id == 3);
            bookFromDb1.BookDetail.NumberOfChapters = 101;
            _db.SaveChanges();

            var bookFromDb2 = _db.Books.Include(u => u.BookDetail).FirstOrDefault(x => x.Book_Id == 3); ;
            bookFromDb2.BookDetail.NumberOfPages = 102;
            _db.Books.Update(bookFromDb2);
            _db.SaveChanges();

            var bookFromDb3 = _db.Books.Include(u => u.BookDetail).FirstOrDefault(x => x.Book_Id == 3); ;
            bookFromDb3.BookDetail.Weight = 103;
            _db.Books.Attach(bookFromDb3);
            _db.SaveChanges();


            var categoryFromDb = _db.Categories.FirstOrDefault();
            _db.Entry(categoryFromDb).State = EntityState.Modified;
            _db.SaveChanges();

            //Views
            var viewList = _db.BookDetailsFromViews.ToList();
            var viewList1 = _db.BookDetailsFromViews.FirstOrDefault();
            var viewList2 = _db.BookDetailsFromViews.Where(u => u.Price > 100).ToList();

            //Raw SQL
            //Limitations: You cannot fetch some columns, You have to fetch the all columns
            var bookRaw = _db.Books.FromSqlRaw("Select * From dbo.Books").ToList();

            //Sql Injection Attack Prone
            int id = 3;
            var bookTemp1 = _db.Books.FromSqlInterpolated($"Select * from dbo.Books where Book_Id = {id}").ToList();
            var bookSproc = _db.Books.FromSqlInterpolated($"EXEC dbo.getAllBookDetails {id}").ToList();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult ManageAuthors(int id)
        {
            BookAuthorVM obj = new BookAuthorVM()
            {
                BookAuthorList = _db.BookAuthors.Include(u => u.Book).Include(u => u.Author).Where(u => u.Book_Id == id).ToList(),
                Book = _db.Books.FirstOrDefault(u => u.Book_Id == id),
                BookAuthor = new BookAuthor() { Book_Id = id }
            };
            List<int> tempListOfAssignedAuthors = obj.BookAuthorList.Select(u => u.Author_Id).ToList();
            //NOT IN CLAUSE IN LINQ
            var tempList = _db.Authors.Where(u => !tempListOfAssignedAuthors.Contains(u.Author_Id)).ToList();
            obj.AuthorList = tempList.Select(i => new SelectListItem() { Text = i.FullName, Value = i.Author_Id.ToString() }).ToList();
            return View(obj);
        }

        [HttpPost]
        public IActionResult ManageAuthors(BookAuthorVM bookAuthorVM)
        {
            if(bookAuthorVM.BookAuthor.Book_Id != 0 && bookAuthorVM.BookAuthor.Author_Id != 0)
            {
                _db.BookAuthors.Add(bookAuthorVM.BookAuthor);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookAuthorVM.BookAuthor.Book_Id });
        }

        [HttpPost]
        public IActionResult RemoveAuthors(int authorId, BookAuthorVM bookAuthorVM)
        {
            int bookId = bookAuthorVM.Book.Book_Id;
            BookAuthor bookAuthor = _db.BookAuthors.FirstOrDefault(u => u.Book_Id == bookId && u.Author_Id == authorId);

            _db.BookAuthors.Remove(bookAuthor);
            _db.SaveChanges();

            return RedirectToAction(nameof(ManageAuthors), new { @id = bookId });
        }

    }
}
