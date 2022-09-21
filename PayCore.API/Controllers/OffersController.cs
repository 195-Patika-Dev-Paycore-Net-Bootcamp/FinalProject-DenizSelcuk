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
    //[Authorize]
    public class OffersController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IGenericService<Offer> _offerService;

        public OffersController(IMapper mapper, IGenericService<Offer> offerService)
        {
            _mapper = mapper;
            _offerService = offerService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var offers = await _offerService.GetAllAsync();
            var offersDtos = _mapper.Map<List<OfferDto>>(offers.ToList());
            return CreateActionResult(CustomResponseDto<List<OfferDto>>.Success(200, offersDtos));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var offer = await _offerService.GetByIdAsync(id);
            var offersDto = _mapper.Map<OfferDto>(offer);
            return CreateActionResult(CustomResponseDto<OfferDto>.Success(200, offersDto));
        }
        [HttpPost]
        public async Task<IActionResult> Save(OfferDto offerDto)
        {
            var offer = await _offerService.AddAsync(_mapper.Map<Offer>(offerDto));
            var offersDto = _mapper.Map<OfferDto>(offer);
            return CreateActionResult(CustomResponseDto<OfferDto>.Success(201, offersDto));
        }
        [HttpPut]
        public async Task<IActionResult> Update(OfferUpdateDto offerDto)
        {
            await _offerService.UpdateAsync(_mapper.Map<Offer>(offerDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var offer = await _offerService.GetByIdAsync(id);
            await _offerService.RemoveAsync(offer);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}
