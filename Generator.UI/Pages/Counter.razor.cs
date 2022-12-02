using System.Dynamic;
using System.Reflection;
using Generator.Shared.Extensions;
using Generator.Shared.Models;
using Newtonsoft.Json;

namespace Generator.UI.Pages
{

    public partial class Counter
	{
        
        private  void IncrementCount()
        {
            //var result = await Service.TestRequest();

           

            //var data1 = result.Data.Deserialize<List<Dictionary<string, object>>>();

            //var test = new TypeDictionary<object>();

            //  test.Add<string>("too");
            //test.Add<string>("me");

            //var result2222 = test.Get<string>();

            //Console.WriteLine();

            //////var datastring = result.Data.Deserialize<object>().ToString();
            ////var dataObject = ((IEnumerable<object>)JsonConvert.DeserializeObject(datastring)).ToList();

            ////var data2= result.Data.Deserialize<List<TESTME>>();


            //var assembly = Assembly.GetEntryAssembly();

            ////var createDynamicList = PropertyExtensions.CreateDynamicList(typeof(TESTME).FullName, assembly);
            //var createDynamicList = PropertyExtensions.CreateDynamicList("Generator.UI.Pages.TESTME", assembly);

            //var data3 = result.Data.Deserialize(createDynamicList.GetType());

            currentCount++;
        }

        public void des<T>(T obj) where T: IDictionary<string, Object>
        {

        }
    }

}

