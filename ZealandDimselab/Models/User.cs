using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZealandDimselab.Models
{
    public class User
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required] [StringLength(100)] public string Email { get; set; }
        [StringLength(100)] public string Password { get; set; }
        [StringLength(20)] public string Role { get; set; }
        [Required] public ICollection<Booking> Bookings { get; set; }

        public User()
        {

        }
        public User(string email)
        {
            Email = email;
            Bookings = new List<Booking>();
        }

        public User(string email, string role, string password = null)
        {
            Email = email;
            Role = role;
            Password = password;
            Bookings = new List<Booking>();
        }

        public User(int id, string name, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;
            Bookings = new List<Booking>();
        }

        public ICollection<Booking> GetUserBookings()
        {
            return Bookings;
        }
    }
}
