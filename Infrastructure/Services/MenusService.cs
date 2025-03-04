using AutoMapper;
using ECoSCore;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGDPEDIDOS.Application.DTOs;
using SGDPEDIDOS.Application.DTOs.ViewModel;
using SGDPEDIDOS.Application.Exceptions;
using SGDPEDIDOS.Application.Interfaces.Repositories;
using SGDPEDIDOS.Application.Interfaces.Services;
using SGDPEDIDOS.Application.Wrappers;
using SGDPEDIDOS.Domain.Entities;
using SGDPEDIDOS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Runtime.InteropServices.ObjectiveC;
using System.Threading.Tasks;
using static DSCALIDAD.Infrastructure.Services.WeeklyMenusDetailService;
using static System.Net.Mime.MediaTypeNames;
using Menu = SGDPEDIDOS.Domain.Entities.Menu;


namespace SGDPEDIDOS.Infrastructure.Services
{
    public class MenusService : BaseService, IMenusService
    {
        private readonly IRepositoryAsync<Menu> _MenuRepo;
        private readonly IRepositoryAsync<Company> _CompanyRepo;
        private readonly IRepositoryAsync<Favorite> _favoriteRepo;
        private readonly IRepositoryAsync<SupplierCompany> _suppliereRepo;
        private readonly IRepositoryAsync<WeeklyMenu> _weeklyMenu;
        private readonly PrincipalContext _Context;
        private readonly IMapper _mapper;
        private readonly IValidator<MenusDto> _validator;
        private readonly IAzureStorage _azureStorage;
        protected readonly DbContext _dbContext;
        public MenusService(IRepositoryAsync<WeeklyMenu> weeklyMenu, IAzureStorage azureStorage, DbContext dbContext, IRepositoryAsync<Menu> MenuRepo, PrincipalContext Context, IRepositoryAsync<SupplierCompany> suppliereRepo, IRepositoryAsync<Favorite> favoriteRepo, IRepositoryAsync<Company> CompanyRepo, IMapper mapper, IValidator<MenusDto> validator, IHttpContextAccessor context) : base(context)
        {
            _MenuRepo = MenuRepo;
            _CompanyRepo = CompanyRepo;
            _mapper = mapper;
            _favoriteRepo = favoriteRepo;
            _suppliereRepo = suppliereRepo;
            _validator = validator;
            _weeklyMenu = weeklyMenu;
            _Context = Context;
            _azureStorage = azureStorage;
            _dbContext = dbContext;
        }

        private async Task<bool> ExitsAsync(int Id)
        {
            var result = await _MenuRepo.Exists(x => x.MenuId.Equals(Id));
            if (result) { return true; }
            throw new ApiException("Menus not found");
        }

        public async Task<Response<MenusVm>> InsertAsync(MenusDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);
            string imageUrl = "";
            var obj = _mapper.Map<Menu>(dto);
            obj.CreatedBy = base.GetLoggerUserId();
            obj.CreationDate = DateTime.Now;
            obj.IsActive = true;
            int onePicture = 1;

            obj = await _MenuRepo.AddAsync(obj);

            var images = new List<SGDPEDIDOS.Domain.Entities.Image>();

            if (dto.Images != null && dto.Images.Count > 0)
            {
                Guid archivoBLock = Guid.NewGuid();
                var nombreArchivoBlock = archivoBLock.ToString().Substring(0, 8);

                foreach (var attachment in dto.Images)
                {
                    var stream = new MemoryStream(Convert.FromBase64String(attachment.ImageName));
                    var archivo = await _azureStorage.UploadAsync(stream, "." + attachment.ContentType);
                    nombreArchivoBlock = archivo.Blob.Name;
                    if (onePicture == 1)
                    {
                        imageUrl = await _azureStorage.GetBlobUriByNameAsync(nombreArchivoBlock);
                        onePicture++;
                    }


                    images.Add(new SGDPEDIDOS.Domain.Entities.Image()
                    {
                        MenuId = obj.MenuId,
                        CompanyId = obj.CompanyId,
                        CompanyCode = "sgd",
                        ImageName = nombreArchivoBlock,
                        ContentType = attachment.ContentType,
                        IsActive = true,
                        CreationDate = DateTime.Now,
                        CreatedBy1 = base.GetLoggerUserId(),

                    });
                }

                foreach (var imagen in images)
                {
                    await _dbContext.AddAsync(imagen);
                }

                await _dbContext.SaveChangesAsync();

                if (images.Count > 0)
                {
                    obj.PictureOne = imageUrl;
                }
            }
            else
            {
                obj.PictureOne = "src/assets/img/dish-icon.svg";
            }

            return new Response<MenusVm>(_mapper.Map<MenusVm>(obj));
        }


        public async Task<Response<MenusVm>> CreateDishes(MenusDto dto)
        {
            try
            {
                int onePicture = 1;
                string imageUrl = "";
                int organizationId = base.GetLoggerUserOrganizationId();
                var valResult = _validator.Validate(dto);
                if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

                var valResultMenu = await _MenuRepo.Exists(d => d.CompanyId == organizationId && d.Name == dto.Name);

                if (valResultMenu)
                {
                    return new Response<MenusVm>($"Existe un producto con este nombre: {dto.Name}");
                }

                Menu obj = new Menu
                {
                    CreatedBy = base.GetLoggerUserId(),
                    CreationDate = DateTime.Now,
                    IsActive = true,
                    CompanyId = organizationId,
                    TypeMenuId = dto.TypeMenuId,
                    Name = dto.Name,
                    MenuDescription = dto.MenuDescription,
                    QuantityAvailable = dto.QuantityAvailable,
                    DeliveryTime = TimeSpan.FromHours(22),
                    MenuAmount = dto.MenuAmount,
                    PictureOne = "src/assets/img/dish-icon.svg" // Valor por defecto
                };

                obj = await _MenuRepo.AddAsync(obj);

                var images = new List<Domain.Entities.Image>();

                if (dto.Images != null && dto.Images.Count > 0)
                {
                    foreach (var attachment in dto.Images)
                    {
                        var stream = new MemoryStream(Convert.FromBase64String(attachment.ImageName));
                        var archivo = await _azureStorage.UploadAsync(stream, "." + attachment.ContentType.Replace("image/", ""));
                        var nombreArchivoBlock = archivo.Blob.Name;

                        if (onePicture == 1)
                        {
                            imageUrl = await _azureStorage.GetBlobUriByNameAsync(nombreArchivoBlock);
                            onePicture++;
                        }

                        images.Add(new Domain.Entities.Image()
                        {
                            MenuId = obj.MenuId,
                            CompanyId = obj.CompanyId,
                            CompanyCode = "syswin",
                            ImageName = nombreArchivoBlock,
                            ContentType = attachment.ContentType,
                            IsActive = true,
                            CreationDate = DateTime.Now,
                            CreatedBy1 = base.GetLoggerUserId(),
                        });
                    }



                    if (images.Count > 0)
                    {
                        obj.PictureOne = imageUrl;
                    }

                    foreach (var imagen in images)
                    {
                        await _dbContext.AddAsync(imagen);
                    }

                    await _dbContext.SaveChangesAsync();

                    var objDb = await _MenuRepo.GetByIdAsync(obj.MenuId);
                    objDb.PictureOne = imageUrl;
                    await _MenuRepo.UpdateAsync(objDb);
                    await _dbContext.SaveChangesAsync();

                }

                return new Response<MenusVm>(_mapper.Map<MenusVm>(obj));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Response<MenusVm>> UpdateDishes(MenusDto dto)
        {
            try
            {
                //var user = base.GetLoggerUser();
                int onePicture = 1;
                string imageUrl = "";
                int organizationId = base.GetLoggerUserOrganizationId();
                int userId = base.GetLoggerUserId();
                var valResult = _validator.Validate(dto);
                if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

                Menu obj = new Menu();
                obj = await _MenuRepo.GetByIdAsync(dto.MenuId);
                obj.ModifiedBy = userId;
                obj.ModifiedDate = DateTime.Now;
                obj.IsActive = dto.IsActive;
                obj.TypeMenuId = dto.TypeMenuId;
                obj.Name = dto.Name;
                obj.MenuDescription = dto.MenuDescription;
                obj.QuantityAvailable = dto.QuantityAvailable;
                obj.MenuAmount = dto.MenuAmount;

                obj = await _MenuRepo.UpdateAsync(obj);


                if (dto.Images.Count > 0)
                {
                    Guid ArchivoBLock = Guid.NewGuid();
                    var NombreArchivoBlock = ArchivoBLock.ToString().Substring(0, 8);
                    foreach (var attachment in dto.Images)
                    {

                        var stream = new MemoryStream(Convert.FromBase64String(attachment.ImageName));
                        var archivo = await _azureStorage.UploadAsync(stream, "." + attachment.ContentType.Replace("image/", ""));
                        NombreArchivoBlock = archivo.Blob.Name;
                        if (onePicture == 1)
                        {
                            imageUrl = await _azureStorage.GetBlobUriByNameAsync(NombreArchivoBlock);
                            onePicture++;
                        }

                        Domain.Entities.Image data = new Domain.Entities.Image()
                        {
                            MenuId = obj.MenuId,
                            CompanyId = obj.CompanyId,
                            CompanyCode = "sgd",
                            ImageName = NombreArchivoBlock,
                            ContentType = attachment.ContentType,
                            IsActive = true,
                            CreationDate = DateTime.Now,
                            CreatedBy1 = base.GetLoggerUserId(),
                        };
                        await _dbContext.AddAsync(data);

                    }
                    await _dbContext.SaveChangesAsync();
                    if (dto.Images.Count > 0)
                    {
                        obj.PictureOne = imageUrl;
                    }
                    var objDb = await _MenuRepo.GetByIdAsync(obj.MenuId);
                    objDb.PictureOne = imageUrl;
                    await _MenuRepo.UpdateAsync(objDb);
                }


                await _dbContext.SaveChangesAsync();
                return new Response<MenusVm>(_mapper.Map<MenusVm>(obj));
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<Response<DtoAddRemoveImage>> AddandRemovePitucture(DtoAddRemoveImage dto)
        {
            int CompanyUser = base.GetLoggerUserOrganizationId();

            var inDB = await _Context.Images.FirstOrDefaultAsync(d => d.ImageName == dto.ImageName && d.CompanyId == CompanyUser);

            if (inDB == null)
            {
                Guid ArchivoBLock = Guid.NewGuid();
                var NombreArchivoBlock = ArchivoBLock.ToString().Substring(0, 8);
                var stream = new MemoryStream(Convert.FromBase64String(dto.ImageName));
                var archivo = await _azureStorage.UploadAsync(stream, "." + dto.ContentType.Replace("image/", ""));
                NombreArchivoBlock = archivo.Blob.Name;
                Domain.Entities.Image data = new Domain.Entities.Image()
                {
                    MenuId = dto.MenuId,
                    CompanyId = CompanyUser,
                    CompanyCode = "sgd",
                    ImageName = NombreArchivoBlock,
                    ContentType = dto.ContentType,
                    IsActive = true,
                    CreationDate = DateTime.Now,
                    CreatedBy1 = base.GetLoggerUserId(),
                };
                await _dbContext.AddAsync(data);
                await _dbContext.SaveChangesAsync();
                dto.ImageName = NombreArchivoBlock;

            }
            else
            {
                var archivo = await _azureStorage.DeleteAsync(dto.ImageName);
                inDB.DeletedDate = DateTime.Now;
                inDB.ModifiedDate = DateTime.Now;
                inDB.ModifiedBy = base.GetLoggerUserId();
                var update = _Context.Images.Update(inDB);
                await _dbContext.SaveChangesAsync();
            }


            return new Response<DtoAddRemoveImage>(dto);
        }

        public async Task<Response<bool>> CopyMenuAsync(CopyMenusDto dto)
        {
            try
            {
                int organizationId = base.GetLoggerUserOrganizationId();

                if (dto == null || dto.WeeklyMenusId == 0 || dto.TypeInsert == 0)
                {
                    return new Response<bool>(false, "Invalid input data.");
                }

                var commandText = "EXEC dbo.sp_insert_menus @company_id, @weekly_menus_id, @type_insert";

                var query = $"EXEC dbo.sp_insert_menus {organizationId}, {dto.WeeklyMenusId}, {dto.TypeInsert}";

                int rowsAffected = await _Context.Database.ExecuteSqlRawAsync(query);

                // Verifying if any rows were affected
                bool result = rowsAffected > 0;
                return new Response<bool>(result);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, $"Error executing stored procedure: {ex.Message}");
            }
        }

        public async Task<Response<MenusVm>> UpdateAsync(int id, MenusDto dto)
        {
            var valResult = _validator.Validate(dto);
            if (!valResult.IsValid) throw new ApiValidationException(valResult.Errors);

            await ExitsAsync(id);

            var objDb = await _MenuRepo.WhereAsync(x => x.MenuId.Equals(id));
            var obj = _mapper.Map<Menu>(dto);

            obj.MenuId = objDb.MenuId;
            obj.CreatedBy = objDb.CreatedBy;
            obj.CreationDate = objDb.CreationDate;
            obj.ModifiedBy = base.GetLoggerUserId();
            obj.ModifiedDate = DateTime.Now;

            return new Response<MenusVm>(_mapper.Map<MenusVm>(await _MenuRepo.UpdateAsync(obj)));
        }
        public async Task<Response<MenusVm>> DeleteAsync(int MenuId)
        {
            int CompanyUser = base.GetLoggerUserOrganizationId();


            await ExitsAsync(MenuId);

            var objDb = await _MenuRepo.WhereAsync(x => x.MenuId.Equals(MenuId));
            if (objDb.CompanyId != CompanyUser)
            {
                throw new KeyNotFoundException($"Menus not found by id={MenuId}");
            }

            objDb.IsActive = false;
            objDb.DeletedDate = DateTime.UtcNow;
            objDb.ModifiedBy = base.GetLoggerUserId();
            objDb.ModifiedDate = DateTime.Now;

            return new Response<MenusVm>(_mapper.Map<MenusVm>(await _MenuRepo.UpdateAsync(objDb)));
        }

        public async Task<Response<MenusVm>> ActiveInactive(int MenuId, bool IsActive)
        {
            int CompanyUser = base.GetLoggerUserOrganizationId();
            await ExitsAsync(MenuId);
            var objDb = await _MenuRepo.WhereAsync(x => x.MenuId.Equals(MenuId));
            if (objDb.CompanyId != CompanyUser)
            {
                throw new KeyNotFoundException($"Menus not found by id={MenuId}");
            }
            objDb.IsActive = IsActive;
            objDb.ModifiedBy = base.GetLoggerUserId();
            objDb.ModifiedDate = DateTime.Now;

            return new Response<MenusVm>(_mapper.Map<MenusVm>(await _MenuRepo.UpdateAsync(objDb)));
        }

        public async Task<Response<MenuCompany>> GetByIdAsync(int id)
        {
            var data = await _MenuRepo.GetByIdAsync(id);
            if (data == null)
            {
                throw new KeyNotFoundException($"Menus not found by id={id}");
            }
            int Userid = base.GetLoggerUserId();
            int CompanyUser = base.GetLoggerUserOrganizationId();



            var supplierCompanies = await _suppliereRepo.WhereAllAsync(d => d.CompanyId == CompanyUser);
            var supplierCompanyIds = supplierCompanies.Select(sc => sc.SupplierId).ToList();

            var menusList = await _MenuRepo.WhereAllAsync(d => d.DeletedDate == null && d.MenuId == id);
            var relatedMenus = menusList.Where(menu => supplierCompanyIds.Contains(menu.CompanyId)).ToList();
            var company = _CompanyRepo.GetByIdAsync(data.CompanyId).Result;

            var favoriteMenuIds = await _favoriteRepo
                .WhereAsync(d => d.UserId == Userid && d.IsActive == true && d.MenuId == id);

            MenuCompany menuCompany = new MenuCompany
            {
                SupplierName = company.CompanyName,
                CompanyId = CompanyUser,
                SupplierId = company.CompanyId,
                Menus = relatedMenus
                        .Where(menu => menu.CompanyId == company.CompanyId)
                        .Select(menu =>
                        {
                            var menuVm = _mapper.Map<MenusVm>(menu);
                            menuVm.IsFavorite = favoriteMenuIds != null ? true : false;
                            menuVm.SupplierId = company.CompanyId;
                            return menuVm;
                        })
                        .ToList()
            };



            return new Response<MenuCompany>(menuCompany);
        }

        public async Task<Response<IList<MenusVm>>> GetAllAsync()
        {
            int CompanyUser = base.GetLoggerUserOrganizationId();
            var list = await _MenuRepo.WhereAsync(d => d.DeletedDate == null && d.CompanyId == CompanyUser);
            if (list == null)
            {
                throw new KeyNotFoundException($"Menus not found");
            }

            return new Response<IList<MenusVm>>(_mapper.Map<IList<MenusVm>>(list));
        }
        public async Task<PagedResponse<IList<MenuCompany>>> GetPagedListAsync(int pageNumber, int pageSize, int? weeklyMenusId, int SupplierId)
        {
            int Userid = base.GetLoggerUserId();
            int CompanyUser = base.GetLoggerUserOrganizationId();

            List<MenuCompany> menuCompanies = new List<MenuCompany>();
            List<Expression<Func<Company, bool>>> queryFilter = new List<Expression<Func<Company, bool>>>();
            List<Expression<Func<Company, object>>> includes = new List<Expression<Func<Company, object>>>();

            try
            {
                queryFilter.Add(x => x.DeletedDate == null && x.TypeCompanyId == 1); // Proveedores


                var list = await _CompanyRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);

                if (list == null || !list.Data.Any())
                {
                    throw new KeyNotFoundException($"Menus not found");
                }

                var currentWeeklyMenusId = GetCurrentWeeklyMenuId();
                int WeeklyDay = GetWeeklyDayId();
                weeklyMenusId = weeklyMenusId == 0 ? currentWeeklyMenusId : weeklyMenusId;
                var supplierCompanies = await _suppliereRepo.WhereAllAsync(d => d.CompanyId == CompanyUser && d.IsActive == true);
                var supplierCompanyIds = supplierCompanies.Select(sc => sc.SupplierId).ToList();


                var menusList = await _MenuRepo.WhereAllAsync(d => d.IsActive == true && d.DeletedDate == null && d.WeeklyMenusDetails.Any(x => x.WeeklyMenusId == weeklyMenusId && x.WeeklyDay == WeeklyDay));
                var relatedMenus = menusList.Where(menu => supplierCompanyIds.Contains(menu.CompanyId)).ToList();

                var favoriteMenuIds = _favoriteRepo
                    .WhereAllAsync(d => d.UserId == Userid && d.IsActive == true).Result
                    .Select(f => f.MenuId)
                    .ToList();

                List<Company> filteredListData = new List<Company>();

                if (SupplierId > 0)
                {
                    filteredListData = list.Data.Where(menu => menu.CompanyId == SupplierId).ToList();
                }
                else
                {
                    filteredListData = list.Data.Where(menu => supplierCompanyIds.Contains(menu.CompanyId)).ToList();
                }


                foreach (var item in filteredListData)
                {
                    MenuCompany menuCompany = new MenuCompany
                    {
                        SupplierName = item.CompanyName,
                        CompanyId = CompanyUser,
                        SupplierId = item.CompanyId,
                        StartServices = item.StartServices,
                        EndServices = item.EndServices,
                        Menus = relatedMenus
                            .Where(menu => menu.CompanyId == item.CompanyId)
                            .Select(menu =>
                            {
                                var menuVm = _mapper.Map<MenusVm>(menu);
                                menuVm.IsFavorite = favoriteMenuIds.Contains(menuVm.MenuId);
                                menuVm.SupplierId = item.CompanyId;
                                menuVm.WeeklyMenuId = weeklyMenusId;
                                menuVm.WeeklyDayId = WeeklyDay;
                                menuVm.TypeMenuName = menu.TypeMenuId == 2 ? "Platos" : "Bebidas";
                                return menuVm;
                            })
                            .ToList()
                    };

                    menuCompanies.Add(menuCompany);
                }

                return new PagedResponse<IList<MenuCompany>>(menuCompanies, list.PageNumber, list.PageSize, list.TotalCount);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<List<MenuCompany>> GeActiveInactiveListAll()
        {

            var archivo = await _azureStorage.ListAsync();
            int Userid = base.GetLoggerUserId();
            int CompanyUser = base.GetLoggerUserOrganizationId();

            List<MenuCompany> menuCompanies = new List<MenuCompany>();
            List<Expression<Func<Company, bool>>> queryFilter = new List<Expression<Func<Company, bool>>>();
            List<Expression<Func<Company, object>>> includes = new List<Expression<Func<Company, object>>>();
            List<Company> filteredListData = new List<Company>();

            queryFilter.Add(x => x.DeletedDate == null && x.TypeCompanyId == 1); // Proveedores


            var list = await _CompanyRepo.GetPagedList(0, 5000, queryFilter, includes: includes);

            if (list == null || !list.Data.Any())
            {
                throw new KeyNotFoundException($"Menus not found");
            }

            var supplierCompanies = await _suppliereRepo.WhereAllAsync(d => d.CompanyId == CompanyUser && d.IsActive == true);
            var supplierCompanyIds = supplierCompanies.Select(sc => sc.SupplierId).ToList();


            var menusList = await _MenuRepo.WhereAllAsync(d => d.IsActive == true && d.DeletedDate == null);
            List<Menu> relatedMenus = menusList.Where(menu => supplierCompanyIds.Contains(menu.CompanyId)).ToList();

            filteredListData = list.Data.Where(menu => supplierCompanyIds.Contains(menu.CompanyId)).ToList();




            foreach (var item in filteredListData)
            {
                MenuCompany menuCompany = new MenuCompany
                {
                    SupplierName = item.CompanyName,
                    CompanyId = CompanyUser,
                    SupplierId = item.CompanyId,
                    StartServices = item.StartServices,
                    EndServices = item.EndServices,
                    Menus = relatedMenus
                        .Where(menu => menu.CompanyId == item.CompanyId)
                        .Select(menu =>
                        {
                            var menuVm = _mapper.Map<MenusVm>(menu);
                            menuVm.SupplierId = item.CompanyId;
                            menuVm.TypeMenuName = menu.TypeMenuId == 2 ? "Platos" : "Bebidas";
                            return menuVm;
                        })
                        .ToList()
                };

                menuCompanies.Add(menuCompany);
            }
            return new List<MenuCompany>(menuCompanies);
        }

        public async Task<PagedResponse<MenuCompany>> GeActiveInactiveListAllPaginate(int pageNumber, int pageSize, string searchText, bool isActive, int type)
        {

            int Userid = base.GetLoggerUserId();
            int CompanyUser = base.GetLoggerUserOrganizationId();

            MenuCompany menuCompanies = new MenuCompany();
            List<Expression<Func<Company, bool>>> queryFilter = new List<Expression<Func<Company, bool>>>();
            List<Expression<Func<Menu, bool>>> Menusilter = new List<Expression<Func<Menu, bool>>>();
            List<Expression<Func<Company, object>>> includes = new List<Expression<Func<Company, object>>>();
            List<Company> filteredListData = new List<Company>();

            queryFilter.Add(x => x.DeletedDate == null && x.TypeCompanyId == 1); // Proveedores



            var list = await _CompanyRepo.GetPagedList(0, 1000, queryFilter, includes: includes);

            if (list == null || !list.Data.Any())
            {
                throw new KeyNotFoundException($"Menus not found");
            }




            var supplierCompanies = await _suppliereRepo.WhereAllAsync(d => d.CompanyId == CompanyUser && d.IsActive == isActive);
            var supplierCompanyIds = supplierCompanies.Select(sc => sc.SupplierId).ToList();

            Menusilter.Add(d => d.IsActive == isActive && d.DeletedDate == null && d.CompanyId == CompanyUser);

            if (!String.IsNullOrEmpty(searchText))
            {
                Menusilter.Add(d => d.Name.Contains(searchText));
            }

            if (type > 0)
            {
                Menusilter.Add(d => d.TypeMenuId == type);
            }


            var menusList = await _MenuRepo.GetPagedList(pageNumber, pageSize,
                Menusilter,
                orderBy: query => query.OrderBy(u => u.Name)
                );
            List<Menu> relatedMenus = menusList.Data.ToList();

            if (supplierCompanyIds.Count > 0)
            {
                filteredListData = list.Data.Where(menu => supplierCompanyIds.Contains(menu.CompanyId)).ToList();
            }
            if (filteredListData.Count == 0)
            {
                Company companyRepo = await _CompanyRepo.GetByIdAsync(CompanyUser);
                if (companyRepo != null)
                {
                    filteredListData.Add(companyRepo);
                }
            }


                menuCompanies.SupplierId = filteredListData[0].CompanyId;
                menuCompanies.SupplierName = filteredListData[0].CompanyName;
                menuCompanies.CompanyId = CompanyUser;
                menuCompanies.EndServices = filteredListData[0].EndServices;
                foreach (var item in filteredListData)
                {


                    foreach (var menu in relatedMenus)
                    {
                        var menuVm = _mapper.Map<MenusVm>(menu);
                        menuVm.SupplierId = item.CompanyId;
                        menuVm.TypeMenuName = menu.TypeMenuId == 2 ? "Platos" : "Bebidas";

                        var images = _Context.Images
                            .Where(d => d.MenuId == menu.MenuId)
                            .Select(d => new ImagenDto
                            {
                                ContentType = d.ContentType,
                                ImageName = d.ImageName
                            })
                            .ToList();


                        menuCompanies.Menus.Add(menuVm);
                    }


                }
            
            return new PagedResponse<MenuCompany>(menuCompanies, menusList.PageNumber, menusList.PageSize, menusList.TotalCount);

        }


        public async Task<PagedResponse<MenuCompany>> GeActiveInactiveListAsync(int pageNumber, int pageSize, int weeklyMenuId, int weeklyDayId, bool? IsActive = null)
        {
            int Userid = base.GetLoggerUserId();
            int CompanyUser = base.GetLoggerUserOrganizationId();

            MenuCompany menuCompanies = new MenuCompany();
            List<Expression<Func<Menu, bool>>> queryFilter = new List<Expression<Func<Menu, bool>>>();
            List<Expression<Func<Menu, object>>> includes = new List<Expression<Func<Menu, object>>>() { d => d.Company, d => d.WeeklyMenusDetails };
            var currentWeeklyMenusId = GetCurrentWeeklyMenuId();
            weeklyMenuId = weeklyMenuId > 0 ? weeklyMenuId : currentWeeklyMenusId;
            pageNumber = pageNumber > 0 ? pageNumber : 0;
            pageSize = pageSize > 0 ? pageSize : 1000;

            queryFilter.Add(d => d.CompanyId == CompanyUser && !d.DeletedDate.HasValue);

            if (weeklyDayId > 0)
            {
                queryFilter.Add(d => d.WeeklyMenusDetails.Any(x => x.WeeklyMenusId == weeklyMenuId && x.WeeklyDay == weeklyDayId));
                var relatedMenus3 = await _MenuRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);
            }
            if (weeklyMenuId > 0)
            {
                queryFilter.Add(d => d.WeeklyMenusDetails.Any(x => x.WeeklyMenusId == weeklyMenuId));
            }

            if (IsActive != null)
            {
                queryFilter.Add(d => d.IsActive == IsActive);

            }

            var relatedMenus = await _MenuRepo.GetPagedList(pageNumber, pageSize, queryFilter, includes: includes);

            menuCompanies.CompanyId = CompanyUser;
            menuCompanies.SupplierId = relatedMenus.Data[0].CompanyId;
            menuCompanies.SupplierName = relatedMenus.Data[0].Company.CompanyName;
            menuCompanies.Menus = new List<MenusVm>();
            menuCompanies.Menus = relatedMenus.Data
                      .Where(menu => menu.CompanyId == menuCompanies.CompanyId)
                      .Select(menu =>
                      {
                          var menuVm = _mapper.Map<MenusVm>(menu);
                          var weeklyDay = weeklyDayId > 0 ? weeklyDayId : menu.WeeklyMenusDetails
                        .Where(d => d.MenuId == menu.MenuId)
                        .Select(d => d.WeeklyDay)
                        .FirstOrDefault();
                          menuVm.IsFavorite = false;
                          menuVm.WeeklyMenuId = weeklyMenuId;
                          menuVm.WeeklyDayId = weeklyDay;
                          menuVm.SupplierId = menuCompanies.CompanyId;
                          menuVm.TypeMenuName = menu.TypeMenuId == 2 ? "Platos" : "Bebidas";
                          return menuVm;
                      })
                      .ToList();



            return new PagedResponse<MenuCompany>(menuCompanies, relatedMenus.PageNumber, relatedMenus.PageSize, relatedMenus.TotalCount);
        }

        public static int GetDayOfWeekNumber(DateTime date)
        {
            return (int)date.DayOfWeek == 0 ? 7 : (int)date.DayOfWeek;
        }

        public int GetCurrentWeeklyMenuId()
        {

            DateTime today = DateTime.UtcNow.Date;
            int daysSinceMonday = ((int)today.DayOfWeek + 6) % 7;
            DateTime currentWeekMonday = today.AddDays(-daysSinceMonday);

            var currentweeklyMenu = _weeklyMenu
                .WhereAsync(d => d.WeeklyFrom.Date == currentWeekMonday).Result.WeeklyMenusId;


            return currentweeklyMenu;
        }

        private static int GetWeeklyDayId()
        {

            DayOfWeek currentDay = DateTime.Now.DayOfWeek;
            int weeklyDayId = currentDay switch
            {
                DayOfWeek.Monday => 1,
                DayOfWeek.Tuesday => 2,
                DayOfWeek.Wednesday => 3,
                DayOfWeek.Thursday => 4,
                DayOfWeek.Friday => 5,
                DayOfWeek.Saturday => 6,
                DayOfWeek.Sunday => 7,
                _ => throw new ArgumentOutOfRangeException()
            };

            return weeklyDayId;
        }


    }
}
