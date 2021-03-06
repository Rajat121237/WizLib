using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WizLib_Model.Models
{

    public class Book
    {
        [Key]
        public int Book_Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string ISBN { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public double Price { get; set; }


        //[ForeignKey("Category")]
        //public int Category_Id { get; set; }
        //public Category Category { get; set; }


        [ForeignKey("BookDetail")]
        public int? BookDetail_Id { get; set; }
        public BookDetail BookDetail { get; set; }

        [ForeignKey("Publisher")]
        public int Publisher_Id { get; set; }
        public Publisher Publisher { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
