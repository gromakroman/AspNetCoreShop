using AspNetCoreShop.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Basket> Baskets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Комиксы", ParentCategoryId = null },
                    new Category { CategoryId = 2, Name = "Marvel", ParentCategoryId = 1 },
                        new Category { CategoryId = 3, Name = "Человек-Паук", ParentCategoryId = 2 },
                        new Category { CategoryId = 4, Name = "Люди Икс", ParentCategoryId = 2 },
                        new Category { CategoryId = 5, Name = "Железный человек", ParentCategoryId = 2 },
                        new Category { CategoryId = 6, Name = "Стражи Галактики", ParentCategoryId = 2 },
                    new Category { CategoryId = 7, Name = "DC", ParentCategoryId = 1 },
                        new Category { CategoryId = 8, Name = "Бэтмен", ParentCategoryId = 7 },
                        new Category { CategoryId = 9, Name = "Стражи Галактики", ParentCategoryId = 7 },
                        new Category { CategoryId = 10, Name = "Супермен", ParentCategoryId = 7 },
                        new Category { CategoryId = 11, Name = "Флэш", ParentCategoryId = 7 },
                    new Category { CategoryId = 12, Name = "Bubble", ParentCategoryId = 1 },
                        new Category { CategoryId = 13, Name = "Красная Фурия", ParentCategoryId = 12 },
                        new Category { CategoryId = 14, Name = "Экслибриум", ParentCategoryId = 12 },
                    new Category { CategoryId = 15, Name = "Другие", ParentCategoryId = 1 },
                        new Category { CategoryId = 16, Name = "Рик и Морти", ParentCategoryId = 15 },
                        new Category { CategoryId = 17, Name = "Ходячие мертвецы", ParentCategoryId = 15 },
                        new Category { CategoryId = 18, Name = "Сага", ParentCategoryId = 15 },
                new Category { CategoryId = 19, Name = "Манга", ParentCategoryId = null },
                new Category { CategoryId = 20, Name = "Артбуки", ParentCategoryId = null },
                new Category { CategoryId = 21, Name = "Фигурки / Игрушки", ParentCategoryId = null },
                    new Category { CategoryId = 22, Name = "Funko POP Vinyl", ParentCategoryId = 21 },
                    new Category { CategoryId = 23, Name = "DC", ParentCategoryId = 21 },
                    new Category { CategoryId = 24, Name = "Гарри Поттер", ParentCategoryId = 21 }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Современный Человек-Паук. Том 1", CategoryId = 3, Image = "s01.jpg", Price = 500, Description = "Ещё до смерти Питера Паркера юный Майлз готовится начать новую главу в своей жизни в новой школе. Вот только укус паука дарует подростку невероятные паучьи способности. Теперь Майлз оказывается в непонятном ему мире, вооружённый лишь инстинктами и ответственностью. Сможет ли он сохранить наследие Человека-паука? Пока Майлз пытается привыкнуть к новой роли, его Дядя Аарон, он же Мародёр, узнает его тайну! У него есть свои планы на племянника, вот только он не догадывается, что у него на хвосте сам Скорпион!" },
                new Product { ProductId = 2, Name = "Современный Человек-Паук. Том 2", CategoryId = 3, Image = "s02.jpg", Price = 300, Description = @"Майлз Моралес всё ещё привыкает к жизни Человека-паука, когда Капитан Америка делает ему особое предложение. Майлз действительно вступит в Алтимэйтс? Пока израненная страна от Гражданской Войны взывает к героям за помощью, Майлз полон решимости доказать, что как никто другой способен помочь.
А когда зловещий новый симбиот Веном появляется, вооружённый правдой об инциденте, давшем новому Человеку - пауку силы,  Паучок обзаводится своим первым настоящим заклятым врагом." },
                new Product { ProductId = 3, Name = "Современный Человек-Паук. Том 3", CategoryId = 3, Image = "s03.jpg", Price = 400, Description = "" },

                  new Product { ProductId = 4, Name = "Новые Люди Икс. Том 1. Первые Люди Икс", CategoryId = 4, Image = "x01.jpg", Price = 1500, Description = "" },
                  new Product { ProductId = 5, Name = "Новые Люди Икс. Том 2. Мы остаёмся", CategoryId = 4, Image = "x02.jpg", Price = 500, Description = "" },
                  new Product { ProductId = 6, Name = "Новые Люди Икс. Том 3. Не по силам", CategoryId = 4, Image = "x03.jpg", Price = 1100, Description = "" },
                  new Product { ProductId = 7, Name = "Смерть Росомахи", CategoryId = 4, Image = "r01.png", Price = 500, Description = "" },
                  new Product { ProductId = 8, Name = "Люди Икс. Нуар. Метка Каина", Image = "r02.jpg", CategoryId = 4, Price = 700, Description = "" },

                  new Product { ProductId = 9, Name = "Железный человек. Нуар", Image = "i01.png", CategoryId = 5, Price = 700, Description = "" },
                  new Product { ProductId = 10, Name = "Железный Человек: Латная перчатка", CategoryId = 5, Price = 500, Description = "" }
                );
        }
    }
}
