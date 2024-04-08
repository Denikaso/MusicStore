using MusicStoreLibrary;
using System;
using TestStore;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            SectionBD sectionDB = new SectionBD();
            CategoryBD categoryBD = new CategoryBD();
            // Тест метода Read
            Console.WriteLine("All Sections:");
            var sections = sectionDB.Read();
            foreach (var section in sections)
            {
                Console.WriteLine("ID: " + section.Id + " Title: " + section.Title);
            }

            Console.WriteLine("All Categories:");
            var categories = categoryBD.Read();
            foreach (var category in categories)
            {
                Console.WriteLine("ID: " + category.Id + " Title: " + category.Title + " Section: " + category.SectionId);
            }


            //// Тест метода Create
            //int newSectionId = sectionDB.Create("New Section");
            //Console.WriteLine("New Section ID: " + newSectionId);



            //// Тест метода SearchById
            //int searchId = 1; // Идентификатор для поиска
            //var sectionById = sectionDB.SearchById(searchId);
            //if (sectionById != null)
            //    Console.WriteLine("Section by ID " + searchId + ": " + sectionById.Title);
            //else
            //    Console.WriteLine("Section with ID " + searchId + " not found");

            //// Тест метода SearchByTitle
            //string searchTitle = "New Section"; // Название для поиска
            //var sectionByTitle = sectionDB.SearchByTitle(searchTitle);
            //if (sectionByTitle != null)
            //    Console.WriteLine("Section with Title '" + searchTitle + "' found. ID: " + sectionByTitle.Id);
            //else
            //    Console.WriteLine("Section with Title '" + searchTitle + "' not found");

            //// Тест метода UpdateTitle
            //int sectionToUpdateId = 2; // Идентификатор секции для обновления
            //string newTitle = "Updated Section Title";
            //int updateResult = sectionDB.UpdateTitle(sectionToUpdateId, newTitle);
            //Console.WriteLine("Update Result: " + updateResult);

            //// Тест метода Delete
            //int sectionToDeleteId = 3; // Идентификатор секции для удаления
            //int deleteResult = sectionDB.Delete(sectionToDeleteId);
            //Console.WriteLine("Delete Result: " + deleteResult);
        }
    }
}
