using AutoMapper;
using PayCore.Core.DTOs;
using PayCore.Core.Models;
using PayCore.Core.Repositories;
using PayCore.Core.Services;
using PayCore.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Service.Services
{
    public class OfferService : GenericService<Offer>, IOfferService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper; 
        public OfferService(IProductService productService, IGenericRepository<Offer> repository, IUnitOfWork unitOfWork, IOfferRepository offerRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _offerRepository = offerRepository;
            _mapper = mapper;
            _productService = productService;
        }

        public async Task<IEnumerable<OfferGetDto>> GetAllOffersForUserAppInBoxAsync(string userAppId)
        {
            var offers = await _offerRepository.GetAllOffersForUserAppInBoxAsync(userAppId);
            var offersDto = _mapper.Map<List<OfferGetDto>>(offers);
            return offersDto;
        }
        public async Task<CustomResponseDto<OfferDto>> CreateOffer(OfferDto offerDto)
        {
            var product = await _productService.GetByIdAsync(offerDto.ProductId);
            if (!product.IsOfferable)
            {
                return CustomResponseDto<OfferDto>.Fail(400, "Product is not offerable.");
            }
            if (product.UserAppId == offerDto.UserAppId)
            {
                return CustomResponseDto<OfferDto>.Fail(400, "This product is already your.");
            }

            var offer = _mapper.Map<Offer>(offerDto);

            await _repository.AddAsync(offer);

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<OfferDto>.Success(200);
        }
        public async Task<IEnumerable<OfferGetDto>> GetAllOffersForUserAppOutBoxAsync(string userAppId)
        {
            var offers =  await _offerRepository.GetAllOffersForUserAppOutBoxAsync(userAppId);
            var offersDto = _mapper.Map<List<OfferGetDto>>(offers);
            return offersDto;
        }
        public async Task<CustomResponseDto<OfferDto>> ConfirmOrRefuseTheOffer(int offerId, string userAppId, bool resultOffer)
        {
            var offer = await GetByIdAsync(offerId);

            var product = await _productService.GetByIdAsync(offer.ProductId);

            if (product.UserAppId != userAppId)
            {
                return CustomResponseDto<OfferDto>.Fail(400, "Product is not your.");
            }
            offer.IsConfirm = resultOffer;

            await UpdateAsync(offer);

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<OfferDto>.Success(200);
        }
        public async Task<CustomResponseDto<NoContentDto>> DeleteOffer(int offerId, string userAppId)
        {
            var offer = await GetByIdAsync(offerId);

            if (offer.UserAppId != userAppId)
            {
                return CustomResponseDto<NoContentDto>.Fail(400, "Offer is not your.");
            }
            await RemoveAsync(offer);

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<NoContentDto>.Success(200);
        }
        public async Task<CustomResponseDto<OfferDto>> UpdateOffer(OfferUpdateDto offerDto, string userAppId)
        {
            var offer = await GetByIdAsync(offerDto.Id);

            if (offer.UserAppId != userAppId)
            {
                return CustomResponseDto<OfferDto>.Fail(400, "Offer is not your.");
            }
            await UpdateAsync(offer);

            await _unitOfWork.CommitAsync();

            return CustomResponseDto<OfferDto>.Success(200);
        }
    }
}
