using Microsoft.AspNetCore.Mvc;
using HotelBookingAPI.Models;
using HotelBookingAPI.Data;

namespace HotelBookingAPI.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class HotelBookingController : ControllerBase
  {
    private readonly ApiContext _context;

    public HotelBookingController(ApiContext context)
    {
      _context = context;
    }

    [HttpPost]
    public JsonResult CreateEdit(HotelBooking booking)
    {
      if (booking.Id == 0)
      {
        _context.Bookings.Add(booking);
      } else {
        var bookingInDb = _context.Bookings.Find(booking.Id);

        if (bookingInDb == null)
          return new JsonResult(NotFound());
      }

      _context.SaveChanges();
      return new JsonResult(booking);
    }

    [HttpGet]
    public JsonResult Get(int id)
    {
      var booking = _context.Bookings.Find(id);

      if (booking == null)
        return new JsonResult(NotFound());

      return new JsonResult(Ok(booking));
    }

    [HttpGet]
    public JsonResult GetAll()
    {
      var result = _context.Bookings.ToList();

      return new JsonResult(Ok(result));
    }

    [HttpDelete]
    public JsonResult Delete(int id)
    {
      var booking = _context.Bookings.Find(id);

      if (booking == null)
        return new JsonResult(NotFound());

      _context.Bookings.Remove(booking);
      _context.SaveChanges();

      return new JsonResult(Ok());
    }
  }
}
