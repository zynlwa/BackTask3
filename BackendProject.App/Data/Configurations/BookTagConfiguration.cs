using BackendProject.App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendProject.App.Data.Configurations
{
    public class BookTagConfiguration:IEntityTypeConfiguration<BookTag>
    {
        public void Configure(EntityTypeBuilder<BookTag> builder)
        {
            builder.HasKey(bt => new { bt.BookId, bt.TagId });
        }
    }
}
