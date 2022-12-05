namespace Generator.Server.Extensions;

public static class AsyncHelper
{
    public static void Sync(Func<Task> func) => Task.Run(func).ConfigureAwait(false);

    public static T Sync<T>(Func<Task<T>> func) => Task.Run(func).Result;

}

