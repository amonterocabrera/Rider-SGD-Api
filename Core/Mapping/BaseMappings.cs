using AutoMapper;
using SGDPEDIDOS.Api.Models;
using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.DTOs.ViewModel.Recoverykey;
using SGDPEDIDOS.Domain.Entities;
using SGDPEDIDOS.Domain.Entities.View;


namespace SGDPEDIDOS.Application.Mapping
{
    public class BaseMappings : Profile
    {
        public BaseMappings()
        {
            //trancking json//
            CreateMap<User, UsersDtoList>().ReverseMap();           

            CreateMap<User, UsersDto>().ReverseMap();
            CreateMap<User, UsersVm>().ReverseMap();
            CreateMap<User, UsersImagenDto>().ReverseMap();
            CreateMap<User, UpdatePasswordUsersDto>().ReverseMap();
            CreateMap<User, UpdateUsersDto>().ReverseMap();
            CreateMap<RecoverKey, RecoverKeyVm>().ReverseMap();
            CreateMap<Menu, MenusDto>().ReverseMap();
            CreateMap<Menu, MenusVm>().ReverseMap();
            CreateMap<Suggestion, SuggestionsDto>().ReverseMap();
            CreateMap<Suggestion, SuggestionsVm>().ReverseMap();
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
            CreateMap<Invoice, InvoiceVm>().ReverseMap();
            CreateMap<InvoicesDetail, InvoicesDetailDto>().ReverseMap();
            CreateMap<InvoicesDetail, InvoicesDetailVm>().ReverseMap();
            CreateMap<RecoverKey, RecoverKeyDto>().ReverseMap();
            CreateMap<WeeklyMenusDetail, WeeklyMenusDetailDto>().ReverseMap();
            CreateMap<WeeklyMenusDetail, WeeklyMenusDetailVm>().ReverseMap();
            CreateMap<WeeklyMenu, WeeklyMenusDto>().ReverseMap();
            CreateMap<WeeklyMenu, WeeklyMenusVm>().ReverseMap();
            CreateMap<WeeklyDay, WeeklyDaysDto>().ReverseMap();
            CreateMap<WeeklyDay, WeeklyDaysVm>().ReverseMap();
            CreateMap<Favorite, FavoritesDto>().ReverseMap();
            CreateMap<Favorite, FavoritesVm>().ReverseMap();
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Company, CompanyVm>().ReverseMap();
            CreateMap<Company, SupplierVm>().ReverseMap();
            CreateMap<SupplierCompany, SupplierCompanyDto>().ReverseMap();
            CreateMap<SupplierCompany, SupplierCompanyVm>().ReverseMap();
            CreateMap<TypeSuggestion, TypeSuggestionsDto>().ReverseMap();
            CreateMap<TypeSuggestion, TypeSuggestionsVm>().ReverseMap();
            CreateMap<VReport, VReportVm>().ReverseMap();
            CreateMap<Gender, GenderVm>().ReverseMap();
            CreateMap<Gender, GenderDto>().ReverseMap();
            CreateMap<UsersType, UsersTypesVm>().ReverseMap();
            CreateMap<UsersType, UsersTypesDto>().ReverseMap();
            CreateMap<TypesIdentification, TypesIdentificationsVm>().ReverseMap();
            CreateMap<TypesIdentification, TypesIdentificationsDto>().ReverseMap();
            CreateMap<VReportSupplier, VReportSupplierVm>().ReverseMap();
            CreateMap<VReportSuggestion, VReportSuggestionVm>().ReverseMap();

            CreateMap<VReportSupplierGroup, VReportSupplierGroupVM>().ReverseMap();
            CreateMap<VReportSupplier, VReportSupplierVM>().ReverseMap();

            CreateMap<Department, DeparmentVm>().ReverseMap();
            CreateMap<Department, DeparmentDto>().ReverseMap();
        }  
    }
}
