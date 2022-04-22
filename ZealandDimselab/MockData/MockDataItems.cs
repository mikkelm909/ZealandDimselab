using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Models;

namespace ZealandDimselab.MockData
{
    public class MockDataItems
    {
        private List<Item> Items;

        public MockDataItems()
        {
            Items = new List<Item>() {
                new Item()
                {
                    Id = 1,
                    Name = "iPhone",
                    Description = "Is an iPhone"
                },
                new Item()
                {
                    Id = 2,
                    Name = "Android",
                    Description = "Is an android"
                },
                new Item()
                {
                    Id = 3,
                    Name = "Raspberry Pie",
                    Description = "Is an Raspberry Pie"
                },
            };
        }

        public List<Item> findAll()
        {
            return Items;
        }

        public Item find(int id)
        {
            return Items.Where(p => p.Id == id).FirstOrDefault();
        }
    }
}
