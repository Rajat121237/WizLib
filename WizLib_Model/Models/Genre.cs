using System.ComponentModel.DataAnnotations.Schema;
namespace WizLib_Model.Models
{
    [Table("tbl_Genres")]
    public class Genre
    {
        public int GenreId { get; set; }
        [Column("Name")]
        public string GenreName { get; set; }
    }
}
