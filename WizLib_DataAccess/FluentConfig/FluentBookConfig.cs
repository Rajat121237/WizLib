using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizLib_Model.Models;

namespace WizLib_DataAccess.FluentConfig
{
    public class FluentBookConfig : IEntityTypeConfiguration<Fluent_Book>
    {
        public void Configure(EntityTypeBuilder<Fluent_Book> modelBuilder)
        {
            //Book

            modelBuilder.HasKey(x => x.Book_Id);
            modelBuilder.Property(p => p.ISBN).IsRequired().HasMaxLength(15);
            modelBuilder.Property(p => p.Title).IsRequired();
            modelBuilder.Property(p => p.Price).IsRequired();
            //Setting up One to one relation between Book & BookDetail
            modelBuilder.HasOne(x => x.Fluent_BookDetail).WithOne(x => x.Fluent_Book).HasForeignKey<Fluent_Book>(x => x.BookDetail_Id);
            //Setting up One to many relation between Book & Publisher
            modelBuilder.HasOne(x => x.Fluent_Publisher).WithMany(x => x.Fluent_Books).HasForeignKey(x => x.PublisherId);
        }
    }
}
