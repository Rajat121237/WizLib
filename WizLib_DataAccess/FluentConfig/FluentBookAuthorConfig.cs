using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizLib_Model.Models;

namespace WizLib_DataAccess.FluentConfig
{
    public class FluentBookAuthorConfig : IEntityTypeConfiguration<Fluent_BookAuthor>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookAuthor> modelBuilder)
        {
            //BookAuthor
            modelBuilder.HasKey(k => new { k.Book_Id, k.Author_Id });
            modelBuilder.HasOne(x => x.Book).WithMany(x => x.Fluent_BookAuthors).HasForeignKey(x => x.Book_Id);
            modelBuilder.HasOne(x => x.Author).WithMany(x => x.Fluent_BookAuthors).HasForeignKey(x => x.Author_Id);
        }
    }
}
