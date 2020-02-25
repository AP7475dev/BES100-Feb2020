﻿namespace LibraryApi.Models
{
    public class GetBooksResponse : HttpCollection<BookSummaryItem>
    {
    }

    public class BookSummaryItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; } 
    }
}