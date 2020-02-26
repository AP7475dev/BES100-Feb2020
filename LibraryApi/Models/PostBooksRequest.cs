using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Models
{
    public class PostBooksRequest : IValidatableObject
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } // "title"; "Where the Crawdads Sing",
        [Required]
        public string Author { get; set; } //  "author": "TBD",
        public string Genre { get; set; } = "None Specified";  // "genre": "Mystery",
        [Range(1, int.MaxValue)]
        public int NumberOfPages { get; set; }     // "numberOfPages": "200"

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Title.ToLower() == "it" && Author.ToLower() == "king")
            {
                yield return new ValidationResult("That book is inappropriate for school",
                    new string[] { "Title", "Author" });
            }
        }
    }

    /*
     * {
    "Title": [
        "That book is inappropriate for school"
    ],
    "Author": [
        "That book is inappropriate for school"
    ]
    }
    */

    // to get from Postman, copy/ paste special/ paste JSON as classes
    // {
    // "title": "Where the Crawdads Sing",
    // "author": "TBD",
    // "genre": "Mystery",
    // "numberOfPages": 200
    // }

    // public class Rootobject
    // {
    //    public string title { get; set; }
    //    public string author { get; set; }
    //    public string genre { get; set; }
    //    public int numberOfPages { get; set; }
    // }

}
