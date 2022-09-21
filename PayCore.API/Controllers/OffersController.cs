using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayCore.API.Filters;
using PayCore.Core.DTOs;
using PayCore.Core.Models;
using PayCore.Core.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PayCore.API.Controllers
{
    [Authorize]
    public class OffersController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IOfferService _offerService;
        private readonly IUserService _userService;

        public OffersController(IUserService userService, IMapper mapper, IOfferService offerService)
        {
            _mapper = mapper;
            _offerService = offerService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffer(OfferCreateDto offerCreateDto)
        {
            var userDto = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);

            var offerDto = _mapper.Map<OfferDto>(offerCreateDto);

            offerDto.UserAppId = userDto.Id;

            return CreateActionResult(await _offerService.CreateOffer(offerDto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(OfferUpdateDto offerDto)
        {
            var userDto = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);

            await _offerService.UpdateOffer(offerDto,userDto.Id);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int offerId)
        {
            var userDto = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
            return CreateActionResult(await _offerService.DeleteOffer(offerId, userDto.Id));
        }

        [HttpGet("Inbox")]
        public async Task<IActionResult> GetInboxOffers()
        {
            var userDto = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);

            var offersDto = await _offerService.GetAllOffersForUserAppInBoxAsync(userDto.Id);

            return CreateActionResult(CustomResponseDto<List<OfferGetDto>>.Success(200, offersDto.ToList()));
        }

        [HttpGet("Outbox")]
        public async Task<IActionResult> GetOutboxOffers()
        {
            var userDto = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);

            var offersDto = await _offerService.GetAllOffersForUserAppOutBoxAsync(userDto.Id);

            return CreateActionResult(CustomResponseDto<List<OfferGetDto>>.Success(200, offersDto.ToList()));
        }

        [HttpPut("Confirm")]
        public async Task<IActionResult> ConfimOffer(int offerId)
        {
            var userDto = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);

            return CreateActionResult(await _offerService.ConfirmOrRefuseTheOffer(offerId, userDto.Id, true));
        }

        [HttpPut("Refuse")]
        public async Task<IActionResult> RefuseOffer(int offerId)
        {
            var userDto = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);

            return CreateActionResult(await _offerService.ConfirmOrRefuseTheOffer(offerId, userDto.Id, false));
        }




    }
}
