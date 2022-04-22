using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZealandDimselab.Models
{
    public class BookedItem
    {
        public Item Item { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public User User { get; set; }
        public int BookingId { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }

        public BookedItem()
        {

        }

        public BookedItem(Item item, DateTime bookingDate, DateTime returnDate, int bookingId, User user, bool status, int quantity)
        {
            Item = item;
            BookingDate = bookingDate;
            ReturnDate = returnDate;
            BookingId = bookingId;
            User = user;
            if (status) Status = "Returned";
            else Status = "Not Returned";
            Quantity = quantity;
        }
    }
}
