using LibraryApi;
using LibraryApi.Domian;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class BooksController : Controller
    {
        LibraryDataContext Context;

        public BooksController(LibraryDataContext context)
        {
            Context = context;
        }

        [HttpGet("books")]
        public ActionResult GetAllBooks()
        {
            // var response = Context.Books.ToList(); // serializing 

            var response = new GetBooksResponse();
            response.Data = Context.Books
                .Select(b => new BookSummaryItem { Id = b.Id, Title = b.Title, Author = b.Author })
                .ToList();
;           return Ok(response);
        }
    }
}
