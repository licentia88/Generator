using ClosedXML.Excel;
using Generator.Components.Components;
using Generator.Components.Extensions;
using Generator.Components.Interfaces;
//using Generator.Shared.Extensions;

namespace Generator.Components.Helpers;

public class GenExcel
{
    //[Inject]
    GeneratorJs ExampleJsInterop { get; set; }

    public GenExcel(GeneratorJs exampleJsInterop)
    {
        ExampleJsInterop = exampleJsInterop;
    }
    public async Task Create<T>(GenGrid<T> grid, string Title) where T:new()
    {
        var excelWorkBook = new XLWorkbook();

        var ws =  excelWorkBook.Worksheets.Add("Sheet1");

        var displayableFields = grid.Components.Where(x => x.GridVisible && x is not IGenSpacer).ToList();


        for (int i = 0; i < displayableFields.Count(); i++)
            ws.Cell(1, i + 1).Value = displayableFields[i].Label;

 
        int row = 1;
        int col = 1;
        foreach (var item in grid.DataSource)
        {
            foreach (var field in displayableFields)
            {
                ws.Cell(row + 1, col).Value = item.GetPropertyValue(field.BindingField).ToString();
                col++;
            }
          
            row++;
            col = 1;
        }

        using var excelStream = new MemoryStream();
        excelWorkBook.SaveAs(excelStream);


       await ExampleJsInterop.DownloadExcelFile(Title, excelStream);


    }
}

