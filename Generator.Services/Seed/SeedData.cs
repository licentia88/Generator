namespace Generator.Services.Seed;

public class SeedData
{
    
    public GeneratorContext Db { get; set; }

    public SeedData(IServiceProvider provider)
    {
        Db = provider.GetService<GeneratorContext>();
    }

    public Task FillComponentsAsync()
    {
        return Task.CompletedTask;

    }
}

