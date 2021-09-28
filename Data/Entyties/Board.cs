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
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string AutorId { get; set; }
        public User Autor { get; set; }
        public List <Column> Columns { get; set; }
        public List<Departament> Departaments { get; set; }
    }
}
