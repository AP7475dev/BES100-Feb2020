using LibraryApi;
using LibraryApi.Domian;
using LibraryApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class BooksController : Controller
    {
        LibraryDataContext Context;

        public BooksController(LibraryDataContext context) //injecting into controller
        {
            Context = context;
        }

        [HttpPut("books/{id:int}/numberofpages")]
        public async Task<ActionResult> UpdateNumberOfPages(int id, [FromBody] int newPages)
        {
            var book = await GetBooksInInventory()
                .Where(b => b.Id == id)
                .SingleOrDefaultAsync();
            if (book == null)
            {
                return NotFound();
            } else
            {
                book.NumberOfPages = newPages;
                await Context.SaveChangesAsync();
                return NoContent();
            }
        }

        [HttpDelete("books/{id:int}")]
        public async Task<ActionResult> RemoceABook(int id)
        {
            var book = await GetBooksInInventory()
                .Where(b => b.Id == id)
                .SingleOrDefaultAsync();

            if (book != null)
            {
                book.InInventory = false;
                await Context.SaveChangesAsync();
            }
            return NoContent(); // 204 No content
        }

        /// <summary>
        /// Add a Book to the Inventory 
        /// </summary>
        /// <param name="bookToAdd">The details of the book to add</param>
        /// <returns></returns>
        [HttpPost("books")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetABookResponse>> AddABook([FromBody] PostBooksRequest bookToAdd)
        {
            // 1. Validate - if not send a 400 bad request
            // 2. Add to database.
            // 3. Return 201 Created status code 
            //    that contains location header with the URL of the newly created resouce
            //    nice to do - just attach a copy of whatever they would get by following the location header.

        // 1. Decide if it is worthy. (validate it)
        //    If Not, send a 400 Bad Request
        // 2. Add it the database.
        // 3. Return:
        //    a 201 Created 
        //    Location header with the URL of the newly created resource (like a birth announcment)
        //    Just attach a copy of whatever they would get by following the location header. 

        if(!ModelState.IsValid)
            {
            return BadRequest(ModelState);
            }

            var book = new Book
            {
                Title = bookToAdd.Title,
                Author = bookToAdd.Author,
                Genre = bookToAdd.Genre,
                InInventory = true,
                NumberOfPages = bookToAdd.NumberOfPages
            };

            Context.Books.Add(book);
            await Context.SaveChangesAsync();
            var response = new GetABookResponse
            {
                Id = book.Id,
                Author = book.Author,
                Genre = book.Genre,
                Title = book.Title,
                NumberOfPages = book.NumberOfPages
            };
            return CreatedAtRoute("books#getabook", new { id = book.Id }, book);
        }

        [HttpGet("books/{id:int}", Name = "books#getabook")]
        public async Task<ActionResult> GetABook(int id)
        {
            var response = await GetBooksInInventory()
                .Where(b => b.Id == id)
                .Select(b => new GetABookResponse
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Genre = b.Genre,
                    NumberOfPages = b.NumberOfPages
                }).SingleOrDefaultAsync();

            if (response == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpGet("books")]
        public async Task<ActionResult> GetAllBooks([FromQuery] string genre = "all")
        {
            // var response = Context.Books.ToList(); // serializing 

            var response = new GetBooksResponse();
            var data = GetBooksInInventory();
            
            if(genre != "all")
            {
                data = data.Where(b => b.Genre == genre); // building query
            }
            response.Data = await data
                .Select(b => new BookSummaryItem { Id = b.Id, Title = b.Title, Author = b.Author })
                .ToListAsync();
            response.Genre = genre;
;           return Ok(response);
        }

        private IQueryable<Book> GetBooksInInventory()
        {
            return Context.Books.Where(b => b.InInventory);
        }
    }
}
/*
 * localhost:1337/books?genre=Fiction
 * [HttpGet("shoes")]
        public ActionResult GetSomeShoes([FromQuery] string color = "All")
        {
            return Ok($"Getting you shoes of color {color}");
        }
        // localhost:1337/shoes?color=blue
*/
