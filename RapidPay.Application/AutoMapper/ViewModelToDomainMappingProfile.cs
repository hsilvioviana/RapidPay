using AutoMapper;
using RapidPay.Application.ViewModels;
using RapidPay.Domain.Models;

namespace RapidPay.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            #region Card
            CreateMap<CardViewModel, CardModel>();
            #endregion
        }
    }
}
