using AutoMapper;
using Core.DTO;
using Core.Models;

namespace WebAPI.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EventAdministrator, EventAdministratorDTO>();//Конфигурация маршрута
            CreateMap<EventAdministratorDTO, EventAdministrator>();

        }
    }
}
