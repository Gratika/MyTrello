using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trello.Data;
using Trello.Data.Entities;

namespace Trello.Controllers
{
    public class MyColumnController : Controller
    {
        UserManager<User> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager <User> _signInManager;
        private readonly ApplicationDbContext _context;
        public MyColumnController(ApplicationDbContext context, 
                                  UserManager<User> userManager,
                                  RoleManager<IdentityRole> roleManager,
                                  SignInManager<User> signInManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;

        }
        public IActionResult Privacy()
        {
            return View();
        }
       /* public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Boards.Include(b => b.Autor);
            return View(await applicationDbContext.ToListAsync());
        }*/
        //oткрытие конкретной карточки в колонке
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .Include(card => card.Autor)
                .Include(card => card.Comments)
                .ThenInclude(comment => comment.Autor)
                .FirstOrDefaultAsync(c => c.Id == id);
                
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }
        
       

        // POST: Columns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BoardId,AutorId")] Column column)
        {
            column.AutorId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                _context.Add(column);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }            
            //ViewData["BoardId"] = new SelectList(_context.Boards, "Id", "Name", column.BoardId);
            return View(column);
        }


    }
}
