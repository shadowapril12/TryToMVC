using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FirstWebApplication.Models
{
    /// <summary>
    /// Класс контекста
    /// </summary>
    public class PersonContext : DbContext
    {
        public PersonContext() : base("DefaultConnection")
        {

        }

        //Сущность записи о человеке в базе данных
        public DbSet<Person> Persons { get; set; }
    }
}