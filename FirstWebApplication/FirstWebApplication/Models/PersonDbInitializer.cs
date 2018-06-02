using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace FirstWebApplication.Models
{
    /// <summary>
    /// Класс создающий при запуски приложения две записи в базе данных
    /// </summary>
    public class PersonDbInitializer : DropCreateDatabaseAlways<PersonContext>
    {
        //Переопределение метода 'Seed'. Передача в него контекста
        protected override void Seed(PersonContext db)
        {
            //Дабавление записи в базу данных
            db.Persons.Add(new Person() { Name = "Вася", Number = "233234"});
            db.Persons.Add(new Person() { Name = "Петя", Number = "322343"});

            //Вызов родительского метода 'Seed'
            base.Seed(db);
        }
    }
}