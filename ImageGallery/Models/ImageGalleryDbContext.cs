using Microsoft.EntityFrameworkCore;

namespace ImageGallery.Models
{
	public class ImageGalleryDbContext : DbContext
    {
	    public ImageGalleryDbContext(DbContextOptions options) : base(options)
	    {
		    
	    }

		public DbSet<Image> Images { get; set; }
    }
}
