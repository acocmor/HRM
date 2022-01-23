using AutoMapper;
using HRM.Models.Auth;
using HRM.Models.User;
using HRM.Entity.Entities;
using HRM.Models.Address;
using HRM.Models.Department;
using HRM.Models.Employee;
using HRM.Models.Gender;
using HRM.Models.Position;

namespace HRM.API.Core.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //User Map
            CreateMap<User, GetUserDTO>().ReverseMap();
            CreateMap<User, LoginDTO>().ReverseMap();
            CreateMap<GetUserDTO, LoginDTO>().ReverseMap();
            CreateMap<CreateUserDTO, User>();
            CreateMap<ChangePasswordDTO, User>();

            //Employee Map
            CreateMap<Employee, GetEmployeeDTO>().ReverseMap();
            CreateMap<CreateEmployeeDTO, Employee>();
            CreateMap<UpdateEmployeeDTO, Employee>();

            //Address Map
            CreateMap<Address, GetAddressDTO>().ReverseMap();
            CreateMap<CreateAddressDTO, Address>();
            CreateMap<UpdateAddressDTO, Address>();

            //Gender Map
            CreateMap<Gender, GetGenderDTO>().ReverseMap();
            CreateMap<Gender, GetGenderNoList>().ReverseMap();
            CreateMap<CreateGenderDTO, Gender>();
            CreateMap<UpdateGenderDTO, Gender>();

            //Position Map
            CreateMap<Position, GetPositionDTO>().ReverseMap();
            CreateMap<Position, GetPositionNoList>().ReverseMap();
            CreateMap<CreatePositionDTO, Position>();
            CreateMap<UpdatePositionDTO, Position>();

            //Department Map
            CreateMap<Department, GetDepartmentDTO>().ReverseMap();
            CreateMap<Department, GetDepartmentNoList>().ReverseMap();
            CreateMap<CreateDepartmentDTO, Department>();
            CreateMap<UpdateDepartmentDTO, Department>();
        }
    }
}
