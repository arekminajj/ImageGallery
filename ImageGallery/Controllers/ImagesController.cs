using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ImageGallery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImageGallery.Controllers
{
	public class ImagesController : Controller
    {
	    private readonly ImageGalleryDbContext _context;

	    public ImagesController(ImageGalleryDbContext context)
	    {
		    _context = context;
	    }

        public IActionResult Index()
        {
	        return View(_context.Images.ToList());
        }

	    public IActionResult Error()
	    {
		    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	    }

		//just for fun-test
	    public IActionResult Current()
	    {
		    return Content(Thread.CurrentThread.CurrentCulture.EnglishName);
	    }

		[HttpGet]
	    public IActionResult Create()
	    {
		    return View();
	    }

		//[HttpPost]
	 //   public IActionResult Post(Image image)
	 //   {
		    
	 //   }

	    [HttpPost]
	    [ValidateAntiForgeryToken]
	    public async Task<IActionResult> Create([Bind("Id,Name,Description,Url,Author")] Image images)
	    {
		    if (ModelState.IsValid)
		    {
				images.Created = DateTime.Now;
			    _context.Add(images);
			    await _context.SaveChangesAsync();
			    return RedirectToAction(nameof(Index));
		    }
		    return View(images);
	    }

		public async Task<IActionResult> Details(int? id)
	    {
		    if (id == null)
		    {
			    return NotFound();
		    }

		    var images = await _context.Images
			    .SingleOrDefaultAsync(m => m.Id == id);
		    if (images == null)
		    {
			    return NotFound();
		    }

		    return View(images);
	    }

		//TODO: Create new better way to do that, this is just for test yet
		[Route("details/name/{name}")]
	    public async Task<IActionResult> FindByName(string name)
	    {
		    if (name == null)
		    {
			    return NotFound();
		    }

		    var nameimages = await _context.Images.SingleOrDefaultAsync(n => n.Name == name);
		    if (nameimages==null)
		    {
			    return NotFound();
		    }

		    return View(nameimages);
	    }

	}
}