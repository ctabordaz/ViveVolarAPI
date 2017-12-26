using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViveVolar.Entities;
using ViveVolar.Models;

namespace ViveVolar.WebApi.Mapper
{
    public class DefaultProfile: Profile
    {
        public override string ProfileName
        {
            get
            {
                return "DefaultProfile";
            }
        }

        public DefaultProfile()
        {
            //Entity to Model
            CreateMap<UserEntity, User>()
                .ForMember(g => g.Email, map => map.MapFrom(vm => vm.RowKey));

            CreateMap<FlightEntity, Flight>()
                .ForMember(g => g.FlightNumber, map => map.MapFrom(vm => vm.RowKey));

            CreateMap<BookingEntity, Booking>();
                
            //Model to Entity
            CreateMap<User, UserEntity>()
                .ForMember(g=> g.RowKey, map => map.MapFrom(vm => vm.Email));

            CreateMap<Flight, FlightEntity>()
                .ForMember(g => g.RowKey, map => map.MapFrom(vm => vm.FlightNumber));

            CreateMap<Booking, BookingEntity>();
        }
    }
}