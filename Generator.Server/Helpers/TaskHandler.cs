using Generator.Shared.Models.ServiceModels;
using MagicOnion;

namespace Generator.Server.Helpers;

public static class TaskHandler
{
    public static async UnaryResult<RESPONSE_RESULT<T>> ExecuteAsync<T>(Func<Task<T>> task) where T : new()
    {
        var result = await task().ConfigureAwait(false);
        return new RESPONSE_RESULT<T>(result);
    }

    public static UnaryResult<RESPONSE_RESULT<T>> Execute<T>(Func<T> task) where T : new()
    {
        var result = task();
        return new UnaryResult<RESPONSE_RESULT<T>>(new RESPONSE_RESULT<T>(result));
    }

    public static async UnaryResult<T> ExecuteAsyncWithoutResponse<T>(Func<Task<T>> task)
    {
        var result = await task().ConfigureAwait(false);
        return result;
    }

    public static UnaryResult<T> ExecuteWithoutResponse<T>(Func<T> task)
    {
        var result = task();
        return new UnaryResult<T>(result);
    }


    public static void Execute(Action task)
    {
        task();
    }

    public static async Task ExecuteAsync(Func<Task> task)
    {
        await task().ConfigureAwait(false);
    }


}


