using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGallery.Models
{
    public class Image
    {
	    public int Id { get; set; }
		[Required]
		[MinLength(3)]
		[MaxLength(50)]
		[Display(Name = "Image name")]
	    public string Name { get; set; }
	    [Display(Name = "Image description")]
		[Required]
		[MinLength(25)]
		[MaxLength(500)]
		public string Description { get; set; }
		[Required]
	    public string Url { get; set; }
	    public DateTime Created { get; set; }
		[Required]
	    public string Author { get; set; }
    }
}
