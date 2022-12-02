using System;
using System.Diagnostics;
using Generator.Shared.Extensions;
using Generator.Shared.Models;

namespace Generator.Service.Helpers;

public class Delegates
{
    public static async ValueTask<RESPONSE_RESULT> ExecuteAsync<T>(Func<ValueTask<T>> task)
    {
        try
        {
            var result = await task();

            var bytes = result.Serialize();

            return new RESPONSE_RESULT(bytes);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            //TODO
            throw new Exception(ex.Message);
        }
    }

    public static async IAsyncEnumerable<RESPONSE_RESULT> ExecuteStreamAsync<T>(Func<IAsyncEnumerable<T>> task)
    {
        await foreach (var data in task())
        {
            byte[] bytes;
            try
            {
                bytes = data.Serialize();
            }
            catch (Exception ex)
            {
                //TODO
                throw new Exception("Do something, Logs probably");
            }

            yield return new RESPONSE_RESULT(bytes);
        }        
    }

    public static RESPONSE_RESULT Execute<T>(Func<T> task)
    {
        try
        {
            var result = task();

            var bytes = result.Serialize();

            return new RESPONSE_RESULT(bytes);
        }
        catch (Exception ex)
        {
            //TODO
            throw new Exception("Do something, Logs probably");
        }
    }


    
}
