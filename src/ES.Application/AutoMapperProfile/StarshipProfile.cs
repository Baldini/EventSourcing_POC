using AutoMapper;
using ES.Domain.DTO;
using ES.Domain.Entities;
using ES.Domain.Results;

namespace ES.Application.AutoMapperProfile
{
    public class StarshipProfile : Profile
    {
        public StarshipProfile()
        {
            CreateMap<Starship, StarshipDto>()
                .ForMember(d => d.Id, f => f.MapFrom(s => s.Id))
                .ForMember(d => d.CurrentLocation, f => f.MapFrom(s => s.CurrentLocation))
                .ForMember(d => d.PilotName, f => f.MapFrom(s => s.PilotName))
                .ForMember(d => d.ShipName, f => f.MapFrom(s => s.ShipName))
                .ForMember(d => d.ShipStatus, f => f.MapFrom(s => s.ShipStatus.ToString()));

            CreateMap<PilotResult, PilotDto>()
                .ForMember(d => d.PilotName, f => f.MapFrom(s => s.PilotName))
                .ForMember(d => d.Date, f => f.MapFrom(s => s.Date));
        }
    }
}
