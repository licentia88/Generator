using Generator.Client.ExampeServices;
using Generator.Client.Hubs;
using Generator.Components.Args;
using Generator.Components.Components;
using Generator.Components.Interfaces;
using Generator.Examples.Shared.Models;
using Generator.Shared.Extensions;
using Generator.Shared.Models.ComponentModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Generator.Example.Pages;

public partial class Users
{
    
    [Inject]
    public PermissionHub PermissionHub { get; set; }


    [Inject]
    public UserService UserService { get; set; }

    [Inject]
    public OrdersMService OrdersM { get; set; }

    private List<PERMISSIONS> pERMISSIONs { get; set; } = new List<PERMISSIONS>
    {
        new PERMISSIONS{ AUTH_NAME ="Test", AUTH_ROWID =1}
    };
    //[Inject]
    //public Lazy<List<USER>> userList { get; set; }

    public List<USER> DataSource { get; set; } = new();

    private IGenView<USER> View { get; set; }

    private GenCheckBox isMarriedCheckBox;
    // [Inject]
    // public ITestService tser { get; set; }

    public GenCheckBox CheckBoxText { get; set; }

    //public async IAsyncEnumerable<string> TestData()
    //{
    //    foreach (var item in DataSource)
    //    {
    //        await Task.Delay(0);
    //        Console.WriteLine($"************SENDING {item.U_LASTNAME}************");
    //        yield return item.U_LASTNAME;
    //    }
    //}
    protected override async Task OnInitializedAsync()
    {
         //var res = isMarriedCheckBox?.GetValue().CastTo<bool>() ?? true; ;
        DataSource =  await UserService.ReadAll();

        //DataSource = res;

        //DataSource = userList.Value.Take(1).ToList();

        //var test = tser.Subscribe(TestData());

        //await foreach (string item in test)
        //{
        //    Console.WriteLine($"************Serving {item}************ {DateTime.Now}");
        //}
           
    }

    private void OnCheckChanged(bool value)
    {
        isMarriedCheckBox.SetValue(value);
    }
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
            CheckBoxText?.SetValue(true);

        base.OnAfterRender(firstRender);
    }
    public void OnBeforeLoad(IGenGrid<USER> grid)
    {
        if (grid.SelectedItem.U_AGE == 0)
        {
            grid.ShouldShowDialog = false;
        }
        

        //grid.ShowDialog = false;
    }

    private GenTextField U_LASTNAME;
    private GenDatePicker U_REGISTER_DATE;

    public void OnDateChanged(DateTime? date)
    {
        U_REGISTER_DATE.SetValue(date);
        //U_LASTNAME.SetValue("VALUE SET!!");
    }

    public void OntextChanged(object date)
    {
        U_LASTNAME.SetValue("VALUE SET!!");
    }
        
    public  void Load(IGenView<USER> view)
    {
        //Console.WriteLine(view.ViewState.ToString());
        View = view;

        if (view.SelectedItem.U_AGE == 0)
            view.ShouldShowDialog = false;

        U_LASTNAME = view.GetComponent<GenTextField>(nameof(USER.U_LASTNAME));

        //U_REGISTER_DATE = view.GetComponent<GenDatePicker>(nameof(USER.U_REGISTER_DATE));
    }

    public async ValueTask Search(SearchArgs components)
    {
        var wherestatements = components.WhereStatements;

        Console.WriteLine();
    }

    public async ValueTask CreateAsync(Generator.Components.Args.GenArgs<USER> data)
    {
     
        //throw new Exception();
        var result = await UserService.Create(data.Model);
 
        ////REQUIRED 
        data.Model.U_ROWID = result.U_ROWID;
        data.Model = result;

 
        DataSource.Add(result);
    }
    private bool IsDisabled = false;

    public void TEST()
    {
        IsDisabled = !IsDisabled;
    }

    public async Task UpdateAsync(GenArgs<USER> data)
    {
         var result = await UserService.Update(data.Model);

        var existing = DataSource.FirstOrDefault(x => x.U_ROWID == data.Model.U_ROWID);


        DataSource.Replace(existing, data.Model);
    }

    public async Task DeleteAsync(GenArgs<USER> data)
    {
        var result = await UserService.Delete(data.Model);

        DataSource.Remove(data.Model);
    }

    public  Task Cancel(GenArgs<USER> data)
    {
        DataSource[data.Index] = data.OldModel;
        return Task.CompletedTask;
    }
}