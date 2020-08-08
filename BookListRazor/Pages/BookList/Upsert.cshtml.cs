using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;

        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;

        }

        [BindProperty]

        public Book Book { get; set; }

        public async Task<IActionResult> OnGet(int? Id)
        {
            Book = new Book();
            if(Id==null)
			{
                return Page();
			}

            Book = await _db.Book.FirstOrDefaultAsync(u => u.id == Id);
            if(Book==null)
			{
                return NotFound();
			}

            return Page();
        }

        public async Task<IActionResult> Onpost()
        {
            if (ModelState.IsValid)
            {
               if(Book.id==0)
				{
                    _db.Book.Add(Book);
				}
               else
				{
                    _db.Book.Update(Book);
				}

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            return RedirectToPage();
        }
    }
}
    
