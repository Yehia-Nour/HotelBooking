using HotelBooking.Application.DTOs.HotelSearchDTOs;
using HotelBooking.Application.Features.HotelSearch.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Presentation.Controllers
{
    [Authorize(Roles = "Admin,Manager,Guest")]
    public class HotelSearchController : ApiBaseController
    {
        private readonly IMediator _mediator;

        public HotelSearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Available")]
        public async Task<ActionResult<IEnumerable<RoomSearchDTO>>> SearchAvailableRooms([FromQuery] RoomsAvailabilityFilter filter)
        {
            var result = await _mediator.Send(new SearchAvailableRoomsQuery(filter));

            return HandleResult(result);
        }

        [HttpGet("Price-Range")]
        public async Task<ActionResult<IEnumerable<RoomSearchDTO>>> SearchByPriceRange([FromQuery] RoomsPriceRangeFilter filter)
        {
            var result = await _mediator.Send(new SearchPriceRangeQuery(filter));

            return HandleResult(result);
        }

        [HttpGet("Type")]
        public async Task<ActionResult<IEnumerable<RoomSearchDTO>>> SearchByRoomType(string roomTypeName)
        {
            var result = await _mediator.Send(new SearchRoomsByTypeQuery(roomTypeName));

            return HandleResult(result);
        }

        [HttpGet("View")]
        public async Task<ActionResult<IEnumerable<RoomSearchDTO>>> SearchByViewType(string viewType)
        {
            var result = await _mediator.Send(new SearchRoomsByViewTypeQuery(viewType));

            return HandleResult(result);
        }

        [HttpGet("Amenity")]
        public async Task<ActionResult<IEnumerable<RoomSearchDTO>>> SearchByAmenity(string amenityName)
        {
            var result = await _mediator.Send(new SearchRoomsByAmenityQuery(amenityName));

            return HandleResult(result);
        }

        [HttpGet("Rating")]
        public async Task<ActionResult<IEnumerable<RoomSearchDTO>>> SearchByMinRating(int minRating)
        {
            var result = await _mediator.Send(new SearchRoomsByMinRatingQuery(minRating));

            return HandleResult(result);
        }

        [HttpGet("Rooms/{roomId}")]
        public async Task<ActionResult<RoomSearchWithAmenitiesDTO>> GetRoomDetails(int roomId)
        {
            var result = await _mediator.Send(new SearchRoomDetailsWithAmenitiesQuery(roomId));

            return HandleResult(result);
        }

        [HttpGet("Rooms/{roomId}/Amenities")]
        public async Task<ActionResult<IEnumerable<AmenitySearchDTO>>> GetAmenitiesForRoom(int roomId)
        {
            var result = await _mediator.Send(new GetAmenitiesForRoomQuery(roomId));

            return HandleResult(result);
        }

        [HttpGet("Custom")]
        public async Task<ActionResult<IEnumerable<RoomSearchDTO>>> SearchCustom([FromQuery] RoomSearchFilter filter)
        {
            var result = await _mediator.Send(new SearchRoomsCustomQuery(filter));

            return HandleResult(result);
        }
    }
}
