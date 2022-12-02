using System;
using System.Reflection.Metadata;

namespace Generator.Services;

public class MsSQLService : IDatabaseService
{
    public List<(string Key, object Value)> GetFunctionParameters()
    {
        throw new NotImplementedException();
    }

    public List<(string Key, object Value)> GetStoredProcedureParameters()
    {
        throw new NotImplementedException();
    }
}

public class OracleService : IDatabaseService
{
    public List<(string Key, object Value)> GetFunctionParameters()
    {
        throw new NotImplementedException();
    }

    public List<(string Key, object Value)> GetStoredProcedureParameters()
    {
        throw new NotImplementedException();
    }
}

public interface IDatabaseService
{
	
	//public List<(string Key,object Value)> Parameters { get; set; }
   

	public List<(string Key, object Value)> GetFunctionParameters();

    public List<(string Key, object Value)> GetStoredProcedureParameters();

}
