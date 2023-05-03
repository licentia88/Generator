using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Generator.UI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;

namespace Generator.UI.Pages
{
	public partial class NotificationsView
	{
        [Inject]
        public ISnackbar Snackbar { get; set; } 

        [Inject]
        public List<NotificationVM> Notifications { get; set; }

     
        protected override Task OnInitializedAsync()
        {
            Snackbar.Configuration.SnackbarVariant = Variant.Filled;
            Snackbar.Configuration.MaxDisplayedSnackbars = 10;
            Snackbar.Configuration.NewestOnTop = true;

            return base.OnInitializedAsync();
        }

        public void Fire()
        {
            if (!Notifications.Any()) return;

            foreach (var notification in Notifications)
            {
                //var errorSeverity =

                Snackbar.Add(notification.Message, notification.Severity);

            }

            Notifications.Clear();
        } 
       

        
    }
}

