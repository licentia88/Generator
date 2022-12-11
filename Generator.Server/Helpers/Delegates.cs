using System.Diagnostics;
using Generator.Shared.Extensions;
using Generator.Shared.Models;
using ProtoBuf.Meta;

namespace Generator.Server.Helpers;

public class Delegates
{
    public static async ValueTask<RESPONSE_RESULT> ExecuteAsync<T>(Func<ValueTask<T>> task) where T: GenObject
    {
        try
        {
            var result = await task();

            //var bytes = result.Serialize();

            return new RESPONSE_RESULT(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            //TODO
            throw new Exception(ex.Message);
        }
    }

    public static async IAsyncEnumerable<RESPONSE_RESULT> ExecuteStreamAsync<T>(Func<IAsyncEnumerable<T>> task) where T : GenObject
    {
        await foreach (var data in task())
            yield return new RESPONSE_RESULT(data);
    }

    public static RESPONSE_RESULT Execute<T>(Func<T> task) where T : GenObject
    {
        try
        {
            var result = task();

            //var bytes = result.Serialize();

            return new RESPONSE_RESULT(result);
        }
        catch (Exception ex)
        {
            //TODO
            throw new Exception("Do something, Logs probably");
        }
    }

    public static async ValueTask<RESPONSE_RESULT<T>> ExecuteModelAsync<T>(Func<ValueTask<T>> task)
    {
        try
        {
            var result = await task();

             return new RESPONSE_RESULT<T>(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            //TODO
            throw new Exception(ex.Message);
        }
    }

    public static async IAsyncEnumerable<RESPONSE_RESULT<T>> ExecuteModelStreamAsync<T>(Func<IAsyncEnumerable<T>> task)
    {
        await foreach (var data in task())
        {
            yield return new RESPONSE_RESULT<T>(data);
        }
    }

    public static RESPONSE_RESULT<T> ExecuteModel<T>(Func<T> task)
    {
        try
        {
            var result = task();

            return new RESPONSE_RESULT<T>(result);
        }
        catch (Exception ex)
        {
            //TODO
            throw new Exception("Do something, Logs probably");
        }
    }

}

 
