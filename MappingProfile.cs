namespace WebApplication3.Models;
using AutoMapper;
using WebApplication3.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<VaiTro, VaiTro>();
        CreateMap<TaiKhoan, TaiKhoanDTO>();
    }
}
