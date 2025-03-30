using AutoMapper;
using RapidPay.Application.ViewModels;
using RapidPay.Domain.Models;

namespace RapidPay.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            #region Card
            CreateMap<CardModel, CardViewModel>();
            #endregion
        }
    }
}
