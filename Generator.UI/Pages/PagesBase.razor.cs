using System;
using Generator.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MudBlazor;

namespace Generator.UI.Pages
{
    public partial class PagesBase<TModel, TService> //where TService : IGenericServiceBase<TModel> where TModel : new()
    {
        [Inject]
        public TService Service { get; set; }

        public List<TModel> DataSource { get; set; } = new List<TModel>();


        [Inject]
        public NotificationsView NotificationsView { get; set; }

        [Inject] ISnackbar Snackbar { get; set; }

      
        protected override Task OnInitializedAsync()
        {
            NotificationsView.Snackbar = Snackbar;
            NotificationsView.Notifications = new();
            return base.OnInitializedAsync();
        }
    }

    public partial class PagesBase<TModel,TChild, TService> : PagesBase<TChild,TService> where TService : IGenericServiceBase<TChild> where TModel : new() where TChild:new()
    {
        [Parameter]
        public TModel ParentModel { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

    }
}

