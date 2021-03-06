﻿using Autofac;
using NorthwindTraders.Application.Customers.Queries.GetCustomersList;
using NorthwindTraders.Common.Dates;

namespace NorthwindTraders.Infrastructure
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(GetCustomersListQuery).Assembly)
                .Where(x => x.Name.EndsWith("Command") || x.Name.EndsWith("Query") || x.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
            builder.RegisterType<MachineDateTime>().As<IDateTime>();
        }
    }
}
