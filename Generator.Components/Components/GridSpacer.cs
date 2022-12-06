using Generator.Components.Enums;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Generator.Components.Components;

public class GridSpacer : GenColumnBase
{

    //TODO gereksiz ise kaldir
    [Parameter, EditorBrowsable(EditorBrowsableState.Never)]
    public ComponentType ComponentType { get; set; }

 
     
}

