using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizLib_Model.Models;

namespace WizLib_DataAccess.FluentConfig
{
    public class FluentAuthorConfig : IEntityTypeConfiguration<Fluent_Author>
    {
        public void Configure(EntityTypeBuilder<Fluent_Author> modelBuilder)
        {
            //Author
            modelBuilder.HasKey(x => x.Author_Id);
            modelBuilder.Property(p => p.FirstName).IsRequired();
            modelBuilder.Property(p => p.LastName).IsRequired();
            modelBuilder.Ignore(p => p.FullName);
        }
    }
}
