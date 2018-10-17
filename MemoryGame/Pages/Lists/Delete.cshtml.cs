using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MemoryGame.Models;
using Microsoft.AspNetCore.Authorization;
using MemoryGame.Infra;

namespace MemoryGame.Pages.Lists
{
    public class DeleteModel : ApplicationPageBase
    {
        public DeleteModel(MemoryGameContext context):base(context)
        {
            Header = "Delete";
        }

        [BindProperty]
        public List List { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List = await _context.List.FirstOrDefaultAsync(m => m.ID == id);

            if (List == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List = await _context.List.FindAsync(id);

            if (List != null)
            {
                _context.List.Remove(List);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
