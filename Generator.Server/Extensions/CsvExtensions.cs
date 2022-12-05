using System.Globalization;
using CsvHelper;

namespace Generator.Server.Extensions;

public static class CsvExtensions
{
   
    public static string CreateCsv<T>(this List<T> objects)
    {
        //var adaptedList = objects.Adapt<List<IDictionary<string, object>>>();
        var path =  Path.Combine(Directory.GetCurrentDirectory(), "TempCsvFiles");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        var savePath = Path.Combine(path, $"{Guid.NewGuid()}.Csv");

        using (var writer = new StreamWriter(savePath))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(objects);

            var test = writer.ToString();
        }

        File.SetAttributes(path, File.GetAttributes(savePath) & ~FileAttributes.Normal);


       

        return savePath;
    }

 
    public static void CreateCsv(this List<IDictionary<string, object> > objects)
    {
        using (var writer = new StringWriter())
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(objects);

            var test = writer.ToString();

        }
    }
}

