using AutoMapper;
using IntercompanyCore.DTOs;
using IntercompanyCore.Entities;

namespace IntercompanyCore.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TransaccionCreacionDTO, Transaccion>();
            CreateMap<Transaccion, TransaccionCreacionDTO>();
            CreateMap<CentrosCostoCreacionDTO, CentrosCosto>();
            CreateMap<CentrosCosto, CentrosCostoCreacionDTO>();
            CreateMap<CuentasCreacionDTO, Cuentas>();
            CreateMap<Cuentas, CuentasCreacionDTO>();
            CreateMap<ItemsCreacionDTO, Items>();
            CreateMap<Items, ItemsCreacionDTO>();
            CreateMap<SocioNegociosCreacionDTO, SocioNegocios>();
            CreateMap<SocioNegocios, SocioNegociosCreacionDTO>();
        }
    }
}
