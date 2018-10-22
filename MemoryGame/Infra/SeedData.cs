using MemoryGame.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemoryGame.Infra
{
    public class SeedData
    {
        public async static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MemoryGameContext(
                serviceProvider.GetRequiredService<DbContextOptions<MemoryGameContext>>()))
            {
                // Look for any Records.
                if (context.Record.Any())
                {
                    return;   // DB has been seeded
                }
                var list = await context.List.FirstOrDefaultAsync();
                context.Record.AddRange(
                    new Record
                    {
                        List = list,
                        ListId = list.ID,
                        Word = "Table",
                        Translation = "Стол"
                    },
                     new Record
                     {
                         List = list,
                         ListId = list.ID,
                         Word = "Bird",
                         Translation = "Птица"
                     },
                      new Record
                      {
                          List = list,
                          ListId = list.ID,
                          Word = "Animal",
                          Translation = "Животное"
                      },
                       new Record
                       {
                           List = list,
                           ListId = list.ID,
                           Word = "Wind",
                           Translation = "Ветер"
                       },
                     new Record
                     {
                         List = list,
                         ListId = list.ID,
                         Word = "Water",
                         Translation = "Вода"
                     },
                      new Record
                      {
                          List = list,
                          ListId = list.ID,
                          Word = "Food",
                          Translation = "Еда"
                      },
                       new Record
                       {
                           List = list,
                           ListId = list.ID,
                           Word = "Earth",
                           Translation = "Земля(планета)"
                       },
                     new Record
                     {
                         List = list,
                         ListId = list.ID,
                         Word = "Mist",
                         Translation = "Туман"
                     },

                     new Record
                     {
                         List = list,
                         ListId = list.ID,
                         Word = "Rain",
                         Translation = "Дождь"
                     },

                     new Record
                     {
                         List = list,
                         ListId = list.ID,
                         Word = "Sweet",
                         Translation = "Сладкий"
                     },
                    new Record
                    {
                        List = list,
                        ListId = list.ID,
                        Word = "Weather",
                        Translation = "Погода"
                    },
                    new Record
                    {
                        List = list,
                        ListId = list.ID,
                        Word = "Ocean",
                        Translation = "Океан"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}