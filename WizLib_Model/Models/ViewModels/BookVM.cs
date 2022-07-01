using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WizLib_Model.Models.ViewModels
{
    public class BookVM
    {
        public Book Book { get; set; }
        public IEnumerable<SelectListItem> PublisherList { get; set; }
    }
}
