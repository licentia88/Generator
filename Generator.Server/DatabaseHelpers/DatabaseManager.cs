namespace Generator.Server.DatabaseResolvers
{

    public interface IDatabaseManager
    {

        Task ExecuteQueryAsync(string query);


    }

    public class DatabaseManager: IDatabaseManager
    {
        private readonly IDatabaseManager _helper;

        public DatabaseManager(IDatabaseManager helper)
        {
            _helper = helper;
        }

        public async Task ExecuteQueryAsync(string query)
        {
            await  _helper.ExecuteQueryAsync(query);
        }
    }

    
}
