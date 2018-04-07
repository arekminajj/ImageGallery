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

		
	    public async Task<IActionResult> Index(string searchString)
	    {
		    var images = from m in _context.Images
			    select m;

		    if (!String.IsNullOrEmpty(searchString))
		    {
			    if (searchString.Length > 1)
			    {
				    images = images.Where(s => s.Name.Contains(searchString));
				}
		    }

		    return View(await images.ToListAsync());
	    }

		//public IActionResult Index()
		//{
		// return View(_context.Images.ToList());
		//}

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
		[Route("/add-new")]
	    public IActionResult Create()
	    {
		    return View();
	    }

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

		[Route("random-image")]
	    public async Task<IActionResult> Random()
		{
			int count = Convert.ToInt32(_context.Images.ToList().Count);
			Random rnd = new Random();
			int randomint = rnd.Next(1, count + 1);

			var images = await _context.Images
				.SingleOrDefaultAsync(m => m.Id == randomint);

			if (images == null)
			{
				return NotFound();
			}

			return View(images);
		}
	}
}