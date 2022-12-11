﻿using System;
using System.Collections.ObjectModel;
using Generator.Shared.Extensions;
using Generator.Shared.Services;
using Generator.Shared.TEST_WILL_DELETE_LATER;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualBasic;

namespace Generator.Example.Pages
{
    public partial class DictionaryGrid
    {
         

        public ICollection<object> DataSource { get; set; }

        [Inject]
        public ITestService Service { get; set; }

        public DictionaryGrid()
        {
            DataSource = new List<object>();
        }

        protected override async Task OnInitializedAsync()
        {
            //var data = await Service.QueryAsync();

            //var result2 = data.Data.Deserialize<object>();
            //var result = data.Data.Deserialize<List<IDictionary<string,object>>>();

            //((List<object>)DataSource).AddRange(result);

            StateHasChanged();
        }
    }
}

