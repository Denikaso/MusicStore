# MusicStore-BackendModels

Модели данных и CRUD операции для сервиса - музыкального магазина, работа над которым проводилась в рамках учебной практики.

## Обзор

Этот репозиторий содержит:

* **Модели сущностей:** Определения классов для основных сущностей музыкального магазина (например, `Category`, `Product`, `Customer`, `Review`, `Order`, ...).
* **CRUD классы:**  Классы для реализации операций CRUD (создание, чтение, обновление, удаление) для каждой модели сущности.

##  Технологии

* **[СУБД]**:  MS SQL Server
* **[Язык программирования]**:  C#
* **[ORM или библиотека для работы с базой данных]**:  LinqToDB

##  Примеры

###  Модель  `Product`
```csharp
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreLibrary
{
    [Table("Product")]
    public class Product
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column("SubcategoryId")]
        public int SubcategoryId { get; set; }        
        [Association(ThisKey = nameof(SubcategoryId), OtherKey = nameof(Subcategory.Id))]
        public Subcategory Subcategory { get; set; }
        [Column("Title"), NotNull]
        public string Title { get; set; }
        [Column("Description"), NotNull]
        public string Description { get; set; }
        [Column("Price")]
        public double Price { get; set; }
        [Column("UnitsInCart")]
        public int UnitsInCart { get; set; }
        [Column("UnitsInStock")]
        public int UnitsInStock { get; set; }
        [Column("Rating")]
        public double Rating { get; set; }
        [Column("Picture"), NotNull]
        public string Picture { get; set; }
    }
}
```


CRUD класс ProductBD
```csharp      
using LinqToDB;
using LinqToDB.DataProvider.SqlServer;
using MusicStoreLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore
{
    public class ProductBD
    {
        private const string CONNECTION_STRING = @"Server=SERVER_NAME;DataBase=DATABASE_NAME;Trusted_Connection=True;";

          ublic int Create(int subcategory, string title, string description, double price, int unitsInCart, int unitsInStock, double rating, string picture)
    {
        using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
        {                
            if (subcategory != null && SearchByTitle(title) == null)
            {
                return db.GetTable<Product>()
                    .Value(p => p.SubcategoryId, subcategory)
                    .Value(p => p.Title, title)
                    .Value(p => p.Description, description)
                    .Value(p => p.Price, price)
                    .Value(p => p.UnitsInCart, unitsInCart)
                    .Value(p => p.UnitsInStock, unitsInStock)
                    .Value(p => p.Rating, rating)
                    .Value(p => p.Picture, picture)
                    .Insert();
            }
            else
            {
                return -1;
            }
        }
    }

    public List<Product> Read()
    {
        using (var db = SqlServerTools.CreateDataConnection(CONNECTION_STRING))
        {
            return db.GetTable<Product>()
                     .LoadWith(p => p.Subcategory)
                     .ThenLoad(subcategory => subcategory.Category)
                     .ThenLoad(category => category.Section)
                     .ToList();
        }
    }

    // ... Остальные методы CRUD 

  }
}
```
##  Связанные репозитории

*  **[Контроллеры для моделей](https://github.com/Denikaso/MusicStoreAPI)**
*  **[Фронтентд на Vue.js](https://github.com/Denikaso/music-store)**
