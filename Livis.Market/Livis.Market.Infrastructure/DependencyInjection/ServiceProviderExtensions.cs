﻿using System;

namespace Livis.Market.Infrastructure
{
    public static class ServiceProviderExtensions
    {
        public static T GetService<T>(this IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            return (T)serviceProvider.GetService(typeof(T));
        }
    }
}
