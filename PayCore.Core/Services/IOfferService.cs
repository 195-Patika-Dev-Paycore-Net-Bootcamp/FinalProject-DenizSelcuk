using PayCore.Core.DTOs;
using PayCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore.Core.Services
{
    public interface IOfferService :IGenericService<Offer>
    {
        /// <summary>
        /// Kullanıcı teklif oluşturur
        /// </summary>
        /// <param name="offerDto"></param>
        /// <returns></returns>
        Task<CustomResponseDto<OfferDto>> CreateOffer(OfferDto offerDto);
        /// <summary>
        /// Kullanıcı kendisine gelen teklifi sonuçlandırır.
        /// </summary>
        /// <param name="offerDto"></param>
        /// <returns></returns>
        Task<CustomResponseDto<OfferDto>> ConfirmOrRefuseTheOffer(int offerId, string userAppId, bool resultOffer);
        /// <summary>
        /// Kullanıcı yaptığı teklifi iptal eder.
        /// </summary>
        /// <param name="offerDto"></param>
        /// <returns></returns>
        Task<CustomResponseDto<NoContentDto>> DeleteOffer(int offerId, string userAppId);
        Task<CustomResponseDto<OfferDto>> UpdateOffer(OfferUpdateDto offerDto, string userAppId);
        /// <summary>
        /// Kullanıcının yaptığı teklifleri listeler
        /// </summary>
        /// <param name="userAppId"></param>
        /// <returns></returns>
        Task<IEnumerable<OfferGetDto>> GetAllOffersForUserAppOutBoxAsync(string userAppId);
        /// <summary>
        /// Kullanıcıya gelen teklifleri listeler
        /// </summary>
        /// <param name="userAppId"></param>
        /// <returns></returns>
        Task<IEnumerable<OfferGetDto>> GetAllOffersForUserAppInBoxAsync(string userAppId);
    }
}
