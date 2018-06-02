using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstWebApplication.Models;
using System.Data.Entity;

namespace FirstWebApplication.Controllers
{
    public class HomeController : Controller
    {
        //Подключение к базе данных
        PersonContext db = new PersonContext();

        /// <summary>
        /// Метод выполняющийся по умолчанию, при запуске приложения
        /// </summary>
        /// <returns>Возвращает представление 'Index.cshtml'</returns>
        public ActionResult Index()
        {
            //Получение записей типа 'Person' из базы данных
            IEnumerable<Person> persons = db.Persons;            

            //Возвращение представления
            return View(db.Persons);
        }

        /// <summary>
        /// Метод вызывающий форму для добавления записи о человеке
        /// </summary>
        /// <returns>Возвращает форму для заполнения</returns>
        public ActionResult AddForm()
        {
            return View();
        }

        /// <summary>
        /// Метод добавления записи о человеке в базу данных
        /// </summary>
        /// <param name="person">Экземпляр класса 'Person'</param>
        /// <returns></returns>
        public ActionResult AddElement(Person person)
        {
            //Добавление записи в базу данных
            db.Persons.Add(person);

            //Сохранение изменений
            db.SaveChanges();

            //Передача управления методу 'Index'
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Метод срабатывающий при нажатии одной из кнопок 'Редактировать' или 'Удалить'
        /// </summary>
        /// <param name="person">Экземпляр класса 'Person', информация о котором передается в метод</param>
        /// <param name="action">Значение параметра 'name' тэга 'button'</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MyAction(Person person, string action)
        {
            //Параметр 'value' тэга 'button' имет значение 'edit', то управление передается методу 'EditForm'
            if(action == "edit")
            {
                //Ему же и передается идентификатор экземпляра класса 'Person'
                return RedirectToAction("EditForm", "Home", new { person.Id });
            }
            //Если значение параметра 'value' имеет значение 'delete', то управление передается методу 'DeleteElement'
            else if(action == "delete")
            {
                //Ему же и передается экземпляр класса 'Person'
                return RedirectToAction("DeleteElement", "Home", person);
            }

            //Передача управления методу 'Index'
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Метод возвращающий форму редактирования записи в таблице 'Persons'
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <returns>Возвращает форму редактирования</returns>
        public ActionResult EditForm(int id)
        {
            //Поиск записи в таблице с соответствующим идентификатором
            Person p = db.Persons.Find(id);

            //Если найден соответствующий элемент
            if(p != null)
            {
                //Вывод формы с соответствующими данными
                return View(p);
            }
            else
            {
                //В обратном случае возврат результата метода 'HttpNotFound'
                return HttpNotFound();
            }
            
        }

        /// <summary>
        /// Метод редактирования записи
        /// </summary>
        /// <param name="p">Экземпляр класса 'Person' с измененными свойствами</param>
        /// <returns>Передает управление методу 'Index'</returns>
        [HttpPost]
        public ActionResult EditElement(Person p)
        {
            //Поиск в таблице записи с соответствующим идентификаторм
            Person oldPerson = db.Persons.Find(p.Id);
            
            //Если соответствие найдено
            if(oldPerson != null)
            {
                //Присваиванием свойствам записи новых значений
                oldPerson.Name = p.Name;
                oldPerson.Number = p.Number;

                //Сохранение изменений
                db.SaveChanges();

                //Передача управления методу 'Index'
                return RedirectToAction("Index");
            }
            //В обратном случае
            else
            {
                //Возврат результата метода 'HttpNotFound'
                return HttpNotFound();
            }
            
        }

        /// <summary>
        /// Метод удаления записи из таблицы 'Persons
        /// </summary>
        /// <param name="person">Экземпляр класса 'Person', который требуется удалить</param>
        /// <returns>Возврат представления по умолчанию</returns>
        public ActionResult DeleteElement(Person person)
        {
            //Поиск в таблице записи с нужным идентификаторм
            Person p = db.Persons.Find(person.Id);
            
            //Если соответствие найдено
            if(p != null)
            {
                //Удаление записи
                db.Persons.Remove(p);

                //Сохранение изменений
                db.SaveChanges();

                //Передача управления методу 'Index'
                return RedirectToAction("Index");
            }
            else
            {
                //В обратном случае вывод результата метода 'HttpNotFound'
                return HttpNotFound();
            }            
        }

        //Выводит форму с описанием приложения
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Метод высвобождения ресурсов
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}