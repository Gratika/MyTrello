using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Trello.Data.Entities;
using Trello.Models;

namespace Trello.Data.Seeds
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            #region  Создание стандартного пользователя и групп

            string adminEmail = "admin@admin.com";
            string password = "QweAsdZxc!23";
            User admin;
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("employee") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("employee"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
            else
            {
                admin = await userManager.FindByNameAsync(adminEmail); 
            }
            #endregion

            #region DemoBoard
            Board test = dbContext.Boards.Where(b => b.Name == "Demo board").First();
            if (test != null) return;
            Board board = new Board() { Autor = admin, Name="Demo board" };
            Comment comment = new Comment() { Autor = admin, Name = "DemoComment" };
            Card card = new Card() { Autor = admin, Name = "DemoCard" };
            Column toDo = new Column() { Autor = admin, Name = "toDo" };
            toDo.Cards.Add(card);
            Column Doing = new Column() { Autor = admin, Name = "Doing" };
            Column Done = new Column() { Autor = admin, Name = "Done" };
            board.Columns.Add(Done);
            board.Columns.Add(toDo);
            board.Columns.Add(Doing);
            await dbContext.Boards.AddAsync(board);
            await dbContext.SaveChangesAsync();
            #endregion
        }
    }
}
