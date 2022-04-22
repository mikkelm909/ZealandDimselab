using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZealandDimselab.Models;

namespace ZealandDimselab.MockData
{
    public static class MockDataBooking
    {
        public static List<Booking> GetBookings()
        {
            return new List<Booking>()
            {
                //new Booking(){BookingItems = GetItems(), User = GetUser(), Details = "Booking 1", BookingDate = DateTime.Now, ReturnDate = DateTime.Now},
                //new Booking(){BookingItems = GetItems(), User = GetUser(), Details = "Booking 2", BookingDate = DateTime.Now, ReturnDate = DateTime.Now},
                //new Booking(){BookingItems = GetItems(), User = GetUser(), Details = "Booking 3", BookingDate = DateTime.Now, ReturnDate = DateTime.Now}
            };
        }

        private static List<Item> GetItems()
        {
            return new List<Item>()
            {
                new Item("Drone V2", "Epic Drone version 2"),
                new Item("Raspberry Pie", "Raspberry Pie v500"),
                new Item("Smartphone", "Android Pie")
            };
        }
        private static User GetUser()
        {
            return new User(1, "Steven", "Steven@gmail.com", "Hej1234");
        }
    }
}
