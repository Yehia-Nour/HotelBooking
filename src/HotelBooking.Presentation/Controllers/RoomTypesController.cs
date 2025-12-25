using HotelBooking.Application.DTOs.RoomTypeDTOs;
using HotelBooking.Application.Results;
using HotelBooking.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelBooking.Presentation.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class RoomTypesController : ApiBaseController
    {
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypesController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomTypeDTO>>> GetAllRoomTypes(bool? IsActive)
        {
            var result = await _roomTypeService.GetAllRoomTypesAsync(IsActive);

            return HandleResult(result);
        }

        [HttpGet("{roomTypeId}")]
        public async Task<ActionResult<RoomTypeDTO>> GetRoomTypeById(int roomTypeId)
        {
            var result = await _roomTypeService.GetRoomTypeByIdAsync(roomTypeId);

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<ActionResult<RoomTypeDTO>> CreateRoomType(CreateRoomTypeDTO roomTypeDTO)
        {
            var userEmail = GetEmailFromToken();
            var result = await _roomTypeService.CreateRoomTypeAsync(userEmail, roomTypeDTO);

            return HandleResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRoomType(UpdateRoomTypeDTO roomTypeDTO)
        {
            var userEmail = GetEmailFromToken();
            var result = await _roomTypeService.UpdateRoomTypeAsync(userEmail, roomTypeDTO);

            return HandleResult(result);
        }

        [HttpDelete("{roomTypeId}")]
        public async Task<IActionResult> DeleteRoomType(int roomTypeId)
        {
            var result = await _roomTypeService.DeleteRoomTypeAsync(roomTypeId);

            return HandleResult(result);
        }

        [HttpPut("{roomTypeId}")]
        public async Task<IActionResult> ToggleRoomType(int roomTypeId)
        {
            var result = await _roomTypeService.ToggleRoomTypeActiveAsync(roomTypeId);

            return HandleResult(result);
        }

    }
}
