using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Trello.Models;

namespace Trello.Data.Entities
{
    public class User: IdentityUser 
    {
        //IdentityUser - стандартный класс, содержащий все поля для регистрации и работы с пользователем
        //уже есть в предке и является стрингом
        //public int Id { get; set; }
        public string Name { get; set; }
        
        //связи
        public List<Board> Boards { get; set; }
        public List<Column> Columns { get; set; }
        public List<Card> Cards { get; set; }
        public  List<Card> CardTeams { get; set; }
        public List<Comment> Comments { get; set; }
       /* public int DepartamentId { get; set; }
        public Departament Departament { get; set; }       
        public Departament MyDep { get; set; }*/

    }
}
