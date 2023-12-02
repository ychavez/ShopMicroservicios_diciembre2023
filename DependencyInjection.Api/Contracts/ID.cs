namespace DependencyInjection.Api.Contracts
{

    public interface IID 
    {
        public Guid Value { get; set; }
    }

    public interface IIDSingleton : IID { }
    public interface IIDScoped : IID { }
    public interface IIDTransient : IID { }


    public class ID : IIDScoped, IIDSingleton, IIDTransient
    {
        public Guid Value { get; set ; } = Guid.NewGuid();
    }
}
