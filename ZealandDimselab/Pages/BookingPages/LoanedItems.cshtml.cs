using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZealandDimselab.Models;
using ZealandDimselab.Services;

namespace ZealandDimselab.Pages.BookingPages
{
    public class LoanedItemsModel : PageModel
    {
        private BookingService _bookingService;
        public List<BookedItem> BookedItems;

        public LoanedItemsModel(BookingService bookingService)
        {
            _bookingService = bookingService;
        }


        public async Task OnGetAsync()
        {
            BookedItems = await _bookingService.GetAllBookedItemsAsync();
        }

        public async Task OnGetSortByIdAscendingAsync()
        {
            List<BookedItem> unsortedList = (await _bookingService.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderBy(bookedItem => bookedItem.Item.Id).ToList();
        }

        public async Task OnGetSortByIdDescendingAsync()
        {
            List<BookedItem> unsortedList = (await _bookingService.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderByDescending(bookedItem => bookedItem.Item.Id).ToList();
        }

        public async Task OnGetSortByItemNameAscendingAsync()
        {
            List<BookedItem> unsortedList = (await _bookingService.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderBy(bookedItem => bookedItem.Item.Name).ToList();
        }

        public async Task OnGetSortByItemNameDescendingAsync()
        {
            List<BookedItem> unsortedList = (await _bookingService.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderByDescending(bookedItem => bookedItem.Item.Name).ToList();
        }

        public async Task OnGetSortByQuantityAscendingAsync()
        {
            List<BookedItem> unsortedList = (await _bookingService.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderBy(bookedItem => bookedItem.Quantity).ToList();
        }

        public async Task OnGetSortByQuantityDescendingAsync()
        {
            List<BookedItem> unsortedList = (await _bookingService.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderByDescending(bookedItem => bookedItem.Quantity).ToList();
        }

        public async Task OnGetSortByBookingIdAscendingAsync()
        {
            List<BookedItem> unsortedList = (await _bookingService.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderBy(bookedItem => bookedItem.BookingId).ToList();
        }

        public async Task OnGetSortByBookingIdDescendingAsync()
        {
            List<BookedItem> unsortedList = (await _bookingService.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderByDescending(bookedItem => bookedItem.BookingId).ToList();
        }

        public async Task OnGetSortByUserNameAscendingAsync()
        {
            List<BookedItem> unsortedList = (await _bookingService.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderBy(bookedItem => bookedItem.User.Email).ToList();
        }

        public async Task OnGetSortByUserNameDescendingAsync()
        {
            List<BookedItem> unsortedList = (await _bookingService.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderByDescending(bookedItem => bookedItem.User.Email).ToList();
        }

        public async Task OnGetSortByBookingDateAscendingAsync()
        {
            List<BookedItem> unsortedList = (await _bookingService.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderBy(bookedItem => bookedItem.BookingDate).ToList();
        }

        public async Task OnGetSortByBookingDateDescendingAsync()
        {
            List<BookedItem> unsortedList = (await _bookingService.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderByDescending(bookedItem => bookedItem.BookingDate).ToList();
        }

        public async Task OnGetSortByReturnDateAscendingAsync()
        {
            List<BookedItem> unsortedList = (await _bookingService.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderBy(bookedItem => bookedItem.ReturnDate).ToList();
        }

        public async Task OnGetSortByReturnDateDescendingAsync()
        {
            List<BookedItem> unsortedList = (await _bookingService.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderByDescending(bookedItem => bookedItem.ReturnDate).ToList();
        }
        public async Task OnGetSortByStatusAscendingAsync()
        {
            List<BookedItem> unsortedList = (await _bookingService.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderBy(bookedItem => bookedItem.Status).ToList();
        }

        public async Task OnGetSortByStatusDescendingAsync()
        {
            List<BookedItem> unsortedList = (await _bookingService.GetAllBookedItemsAsync());
            BookedItems = unsortedList.OrderByDescending(bookedItem => bookedItem.Status).ToList();
        }

        public async Task OnGetFilterByItemId(int id)
        {

        }

        public async Task OnGetFilterByQuantity(int min, int max)
        {

        }

        public async Task OnGetFilterByItemNameAsync(string name)
        {

        }

        public async Task OnGetFilterByBookedByAsync(string name)
        {

        }

        public async Task OnGetFilterByStatus(string status)
        {

        }
    }
}
