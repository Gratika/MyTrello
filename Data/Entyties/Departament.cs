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
    public class Departament
    { 
        public int Id { get; set; }
        public string Name { get; set; }


       /* public string BossId { get; set; }
        public User Boss { get; set; }
        public List <User> Teams { get; set; }*/
        public List<Board> Boards { get; set; }
    }
}
