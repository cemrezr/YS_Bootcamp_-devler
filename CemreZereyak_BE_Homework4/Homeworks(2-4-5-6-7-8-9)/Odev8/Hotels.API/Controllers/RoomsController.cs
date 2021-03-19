using System.Data.Common;
using System;
using Hotels.API.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Hotels.API.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Hotels.API.Controllers
{
    [Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {

        private readonly IRoomService _roomService;
        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet(Name = nameof(GetRooms))]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _roomService.GetRoomsAsync();

            if(rooms == null)
                return NoContent();
            
            return Ok(rooms);

        }

    }
}
