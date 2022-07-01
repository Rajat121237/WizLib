using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WizLib_Model.Models.ViewModels
{
    public class BookAuthorVM
    {
        public BookAuthor BookAuthor { get; set; }
        public Book Book { get; set; }
        public IEnumerable<BookAuthor> BookAuthorList { get; set; }
        public List<SelectListItem> AuthorList { get; set; }
    }
}
