using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
