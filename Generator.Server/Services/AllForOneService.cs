namespace Generator.Server.Services;


/// <summary>
/// Tum queryler bu service uzerinden gececek
/// </summary>
public class AllForOneService : ServiceBase<GeneratorContext>
{
    public AllForOneService(IServiceProvider provider) : base(provider)
    {
    }
}
