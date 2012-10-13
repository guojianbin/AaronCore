namespace Aaron.Core.Infrastructure
{
    public interface IDependencyResolverFactory
    {
        IDependencyResolver CreateInstance();
    }
}
