using System.Reflection;
using Bunit;
using Generator.Components.Components;
using Generator.Components.Enums;
using Generator.Components.Extensions;
using MudBlazor;
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
        using var context = new TestContext();
        context.JSInterop.Mode = JSRuntimeMode.Loose;
        context.Services.AddMudServices();
        context.Services.RegisterGeneratorComponents();

        var host = context.RenderComponent<GridHost>();
        var icon = dialog == ViewState.Create ? Icons.Material.Filled.AddCircle : Icons.Material.Outlined.Edit;
        host.FindComponents<MudIconButton>().Single(button => button.Instance.Icon == icon).Find("button").Click();
        host.WaitForAssertion(() => Assert.Single(host.FindComponents<GenPage<Row>>()));
        Assert.Equal(dialog, host.FindComponent<GenPage<Row>>().Instance.ViewState);

        // When a circuit ends, Blazor disposes its components in the order its renderer holds them, where the grid can
        // come before the page its dialog shows. Disposing the host's components removes the grid first, as GridHost
        // renders it first, and reports any exception a component's Dispose throws.
        var failure = Record.Exception(context.DisposeComponents);

        Assert.Null(failure);
    }
}
