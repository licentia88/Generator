﻿using Generator.Client.ExampeServices;
using Generator.Components.Args;
using Generator.Components.Components;
using Generator.Components.Interfaces;
using Generator.Examples.Shared;
using Generator.Examples.Shared.Models;
using Generator.Examples.Shared.Services;
using Generator.Shared.Extensions;
using Generator.Shared.Models.ServiceModels;
using Microsoft.AspNetCore.Components;

namespace Generator.Example.Pages;

public partial class Users
{
    [Inject]
    public UserService UserService { get; set; }

    [Inject]
    public Lazy<List<USER>> userList { get; set; }

    public List<USER> DataSource { get; set; } = new();

    private IGenView<USER> View { get; set; }

    private GenCheckBox? isMarriedCheckBox;
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
            CheckBoxText.SetSearchValue(true);

        base.OnAfterRender(firstRender);
    }
    public void OnBeforeLoad(IGenGrid<USER> grid)
    {
        //if (grid.SelectedItem.U_AGE == 0)
        //{
        //    grid.ShouldShowDialog = false;
        //}

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
            view.ShoulShowDialog = false;

        U_LASTNAME = view.GetComponent<GenTextField>(nameof(USER.U_LASTNAME));

        U_REGISTER_DATE = view.GetComponent<GenDatePicker>(nameof(USER.U_REGISTER_DATE));
    }

    public async ValueTask Search(SearchArgs components)
    {
        var wherestatements = components.WhereStatements;

        Console.WriteLine();
    }

    public async ValueTask CreateAsync(USER data)
    {
          
        //throw new Exception();
        var result = await UserService.Create(data);



        ////REQUIRED 
        data.U_ROWID = result.U_ROWID;
        data = result;

 
        DataSource.Add(result);
    }
    private bool IsDisabled = false;

    public void TEST()
    {
        IsDisabled = !IsDisabled;
    }

    public async ValueTask UpdateAsync(USER data)
    {
        var result = await UserService.Update(data);

        var existing = DataSource.FirstOrDefault(x => x.U_ROWID == data.U_ROWID);

        DataSource.Replace(existing, data);
    }

    public async ValueTask DeleteAsync(USER data)
    {
        var result = await UserService.Delete(data);

        DataSource.Remove(data);
    }
}