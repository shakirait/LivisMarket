

using Autofac;

namespace Livis.Market.Utilities.IoC
{
    public static class Container
    {
        public static IContainer Instance { get; private set; }

        public static void SetContainer(IContainer container)
        {
            Instance = container;
        }
    }
}
