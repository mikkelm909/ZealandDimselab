using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ZealandDimselab.Interfaces;
using ZealandDimselab.Models;

namespace ZealandDimselab.Services
{
    public class BookingService
    {
        private readonly IBookingDb dbService;
        private IItemDb _itemService;

        public BookingService(IBookingDb dbService, IItemDb itemService)
        {
            this.dbService = dbService;
            _itemService = itemService;
            
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            await dbService.GetObjectsAsync();
            return await dbService.GetObjectsAsync();
        }

        public async Task<Booking> GetBookingByKeyAsync(int id)
        {
            return await dbService.GetObjectByKeyAsync(id);
        }

        public async Task AddBookingAsync(Booking booking)
        {
            await dbService.AddObjectAsync(booking);
        }

        public async Task DeleteBookingAsync(int id)
        {
            Booking booking = await GetBookingByKeyAsync(id);
            await dbService.DeleteObjectAsync(booking);
        }

        public async Task UpdateBookingAsync(Booking updatedBooking)
        {
            await dbService.UpdateObjectAsync(updatedBooking);
        }

        public async Task<List<Booking>> GetBookingsByEmailAsync(string email) // TODO Pretty sure this doesn't work
        {
            List<Booking> userBookings = new List<Booking>();
            foreach (Booking booking in await GetAllBookingsAsync())
            {
                if (booking.User.Email.ToLower() == email.ToLower())
                {
                    userBookings.Add(booking);
                }
            }
            return userBookings;
        }


        public async Task<List<BookedItem>> GetAllBookedItemsAsync()
        {
            var bookedItems = new List<BookedItem>();

            foreach (var booking in await GetAllBookingsAsync())
            {
                foreach (var item in booking.BookingItems)
                {
                    bookedItems.Add(new BookedItem(item.Item, booking.BookingDate, booking.ReturnDate, booking.Id, booking.User, booking.Returned, item.Quantity));
                }
            }

            return bookedItems;
        }

        public async Task ReturnedBooking(int id)
        {
            Booking booking = await GetBookingByKeyAsync(id);
            if (booking.Returned == false)
            {
                booking.Returned = true;
                foreach (var bookingItem in booking.BookingItems)
                {
                    //Item item = await _itemService.GetItemByIdAsync(bookingItem.ItemId);
                    Item item = await _itemService.GetObjectByKeyAsync(bookingItem.ItemId);
                    item.Stock = item.Stock + bookingItem.Quantity;
                    //await _itemService.UpdateItemAsync(item.Id, item);
                    await _itemService.UpdateObjectAsync(item);
                }
                await dbService.UpdateObjectAsync(booking);
            }
            else
            {
                booking.Returned = false;
                foreach (var bookingItem in booking.BookingItems)
                {
                    //Item item = await _itemService.GetItemByIdAsync(bookingItem.ItemId);
                    Item item = await _itemService.GetObjectByKeyAsync(bookingItem.ItemId);
                    item.Stock = item.Stock - bookingItem.Quantity;
                    //await _itemService.UpdateItemAsync(item.Id, item);
                    await _itemService.UpdateObjectAsync(item);
                }
                await dbService.UpdateObjectAsync(booking);
            }

        }
    }
}
