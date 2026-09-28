using System.Reflection;
using Bunit;
using Generator.Components.Components;
using Generator.Components.Enums;
using Generator.Components.Extensions;
using MudBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Xunit;

namespace Generator.Components.Tests;

public class GenPageDisposalTests
{
    [Fact]
    public void GenPage_HasNoFinalizer()
    {
        var finalizer = typeof(GenPage<Row>).GetMethod(
            "Finalize",
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

        Assert.Null(finalizer);
    }

    [Theory]
    [InlineData(ViewState.Create)]
    [InlineData(ViewState.Update)]
    public void GridDisposedFirst_WithADialogOpen_DisposesTheDialogPageWithoutAnError(ViewState dialog)
    {
        using var context = CreateContext();
        var host = context.RenderComponent<GridHost>();
        OpenDialog(host, dialog);

        // When a circuit ends, Blazor disposes its components in the order its renderer holds them, where the grid can
        // come before the page its dialog shows. Disposing the host's components removes them in render-tree order, the
        // grid first as GridHost renders it first, and reports any exception a component's Dispose throws.
        var failure = Record.Exception(context.DisposeComponents);

        Assert.Null(failure);
    }

    // Navigating away with a dialog open: the page re-renders without its grid, then MudDialogProvider closes every
    // dialog on the same location change, so the dialog's page is disposed after its grid, in a live circuit.
    [Theory]
    [InlineData(ViewState.Create)]
    [InlineData(ViewState.Update)]
    public void Navigation_WithADialogOpen_DisposesTheDialogPageWithoutAnError(ViewState dialog)
    {
        using var context = CreateContext();
        var host = context.RenderComponent<NavigationHost>();
        OpenDialog(host, dialog);

        context.Services.GetRequiredService<NavigationManager>().NavigateTo("/elsewhere");

        host.WaitForAssertion(() => Assert.Empty(host.FindComponents<GenPage<Row>>()));
        Assert.Empty(host.FindComponents<GenGrid<Row>>());
        var unhandled = context.Renderer.UnhandledException;
        Assert.False(unhandled.IsCompleted, unhandled.IsCompleted ? unhandled.Result.ToString() : null);
    }

    // A dialog closed while its grid is alive: the page's dispose cancels the edit on the grid. The Cancel button closes
    // the dialog through the page; Escape and the dialog's X call the dialog instance's Cancel, which skips the page.
    [Theory]
    [InlineData("the Cancel button")]
    [InlineData("the dialog's own Cancel")]
    public void Dismissal_WhileTheGridIsAlive_CancelsTheEdit(string dismissal)
    {
        using var context = CreateContext();
        var host = context.RenderComponent<GridHost>();
        OpenDialog(host, ViewState.Update);
        var grid = host.FindComponent<GenGrid<Row>>().Instance;
        host.Find(".mud-dialog input").Change("changed");
        Assert.Equal("changed", grid.DataSource.Single().Name);

        if (dismissal == "the Cancel button")
            host.FindComponents<MudButton>().Single(button => button.Markup.Contains(grid.CancelText)).Find("button").Click();
        else
            host.InvokeAsync(host.FindComponent<MudDialogInstance>().Instance.Cancel);

        host.WaitForAssertion(() => Assert.Empty(host.FindComponents<GenPage<Row>>()));
        Assert.Equal(ViewState.None, grid.ViewState);
        Assert.Equal("first", grid.DataSource.Single().Name);
    }

    private static TestContext CreateContext()
    {
        var context = new TestContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.Services.AddMudServices();
        context.Services.RegisterGeneratorComponents();
        return context;
    }

    private static void OpenDialog(IRenderedFragment host, ViewState dialog)
    {
        var icon = dialog == ViewState.Create ? Icons.Material.Filled.AddCircle : Icons.Material.Outlined.Edit;
        host.FindComponents<MudIconButton>().Single(button => button.Instance.Icon == icon).Find("button").Click();
        host.WaitForAssertion(() => Assert.Single(host.FindComponents<GenPage<Row>>()));
        Assert.Equal(dialog, host.FindComponent<GenPage<Row>>().Instance.ViewState);
    }
}
