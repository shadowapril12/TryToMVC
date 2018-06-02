using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstWebApplication.Models
{
    /// <summary>
    /// Класс записи о человеке в телефонной книге
    /// </summary>
    public class Person
    {
        //Идентификатор
        public int Id { get; set; }

        //Имя
        public string Name { get; set; }

        //Номер телефона
        public string Number { get; set; }
    }
}