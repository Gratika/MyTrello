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
    public class MyBoardsController : Controller
    {
        UserManager<User> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager <User> _signInManager;
        private readonly ApplicationDbContext _context;
        public MyBoardsController(ApplicationDbContext context, 
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
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Boards.Include(b => b.Autor);
            return View(await applicationDbContext.ToListAsync());
        }
        //oткрытие конкретной доски
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var board = await _context.Boards
                .Include(board => board.Autor)
                .Include(board => board.Columns)               
                .ThenInclude(colmn=>colmn.Cards)
                 .ThenInclude(card => card.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AutorId")] Board board)
        {
            board.AutorId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                _context.Add(board);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index");
        }


    }
}
