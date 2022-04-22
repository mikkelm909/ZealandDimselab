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
    public class MyBookingsModel : PageModel
    {
        private readonly BookingService bookingService;

        public List<Booking> Bookings { get; set; }

        public MyBookingsModel(BookingService bookingService)
        {
            this.bookingService = bookingService;
            Bookings = new List<Booking>();
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.User.IsInRole("admin") || HttpContext.User.IsInRole("teacher"))
            {
                Bookings = (await bookingService.GetAllBookingsAsync()).ToList();
            } else if (HttpContext.User.IsInRole("student"))
            {
                Bookings = (await bookingService.GetBookingsByEmailAsync(HttpContext.User.Identity.Name)).ToList();
            }
         

            return Page();

        }

        public async Task<IActionResult> OnGetConfirmReturnAsync(int id)
        {
            if (HttpContext.User.IsInRole("admin"))
            {
                await bookingService.ReturnedBooking(id);
            }
            return RedirectToPage("MyBookings");
        }
    }
}
