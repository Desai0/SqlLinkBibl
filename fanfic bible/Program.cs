// Если тут подчёркивается, то скачай через нугет эту фигню
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

// Тут надо будет классы фигнь всех создать
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
}

// Тут надо будет придумать как подключиться к бд
public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=1234");
    }
}

// ==================================
// Программа
// ==================================
class Program
{
    static void Main()
    {
        var context = new AppDbContext();

        context.Database.EnsureCreated();

        context.Products.AddRange(
            new Product { Id = 1, Name = "Phone", Price = 800 },
            new Product { Id = 2, Name = "Laptop", Price = 1200 },
            new Product { Id = 3, Name = "Tablet", Price = 600 }
        );

        context.SaveChanges();

        // ----------------------------------
        // Добавление фигни
        // ----------------------------------
        Console.WriteLine("Продукты добавлены.");

        Console.WriteLine("Все продукты:");
        var products = context.Products.ToList();
        foreach (var pr in products)
        {
            Console.WriteLine($"{pr.Id}: {pr.Name} - {pr.Price}");
        }

        // ----------------------------------
        // Нахождение фигни
        // ----------------------------------
        Console.WriteLine("\nПоиск продукта по имени:");

        var product = context.Products.FirstOrDefault(p => p.Name == "Phone");

        if (product != null)
        {
            Console.WriteLine($"Найден продукт: {product.Name} - {product.Price}");
        }
        else
        {
            Console.WriteLine("Продукт не найден.");
        }

        // ----------------------------------
        // Фильтрация фигни
        // ----------------------------------
        Console.WriteLine("\nПродукты дороже 700:");

        var filteredProducts = context.Products.Where(p => p.Price > 700).ToList();

        foreach (var pr in filteredProducts)
        {
            Console.WriteLine($"{pr.Name} - {pr.Price}");
        }

        // ----------------------------------
        // Обновление фигни
        // ----------------------------------
        Console.WriteLine("\nОбновление цены продукта:");

        product = context.Products.Find(1);

        if (product != null)
        {
            product.Price = 900;

            context.SaveChanges();

            Console.WriteLine($"Цена продукта {product.Name} обновлена до {900}.");
        }
        else
        {
            Console.WriteLine("Продукт для обновления не найден.");
        }

        products = context.Products.ToList();

        foreach (var pr in products)
        {
            Console.WriteLine($"{pr.Id}: {pr.Name} - {pr.Price}");
        }

        // ----------------------------------
        // Удаление фигни
        // ----------------------------------
        Console.WriteLine("\nУдаление продукта:");

        product = context.Products.Find(2);

        if (product != null)
        {
            context.Products.Remove(product);

            context.SaveChanges();

            Console.WriteLine($"Продукт {product.Name} удален.");
        }
        else
        {
            Console.WriteLine("Продукт для удаления не найден.");
        }

        products = context.Products.ToList();
        foreach (var pr in products)
        {
            Console.WriteLine($"{pr.Id}: {pr.Name} - {pr.Price}");
        }

        // ----------------------------------
        // Агрегатная фигня
        // ----------------------------------
        Console.WriteLine("\nСредняя цена всех продуктов:");

        var averagePrice = context.Products.Average(p => p.Price);
        Console.WriteLine($"Средняя цена: {averagePrice:F2}");

        // ----------------------------------
        // Отчистка фигни
        // ----------------------------------
        Console.WriteLine("\nОчистка таблицы:");

        context.Products.RemoveRange(context.Products);

        context.SaveChanges();

        Console.WriteLine("Таблица очищена.");

        products = context.Products.ToList();
        foreach (var pr in products)
        {
            Console.WriteLine($"{pr.Id}: {pr.Name} - {pr.Price}");
        }
    }
}
