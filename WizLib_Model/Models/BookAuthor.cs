
using System.ComponentModel.DataAnnotations.Schema;

namespace WizLib_Model.Models
{
    public class BookAuthor
    {
        [ForeignKey("Book")]
        public int Book_Id { get; set; }

        [ForeignKey("Author")]
        public int Author_Id { get; set; }

        public Author Author { get; set; }
        public Book Book { get; set; }
    }
}
