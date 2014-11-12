using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reader
{
    public static class Mappers
    {
        public static void Initialize()
        {
            Mapper.CreateMap<Reader.Model.Subscription, Reader.Models.SubscriptionViewModel>();
            Mapper.CreateMap<Reader.Models.SubscriptionViewModel, Reader.Model.Subscription>();

            /*    .ForMember(d => d.Id,
                         x => x.ResolveUsing(j => j.Id.ToString()));*/
        }
    }
}