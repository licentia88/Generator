﻿namespace Generator.Components.Interfaces;

public interface IGenComponent : IGenCompRenderer
{
    Type DataType { get; set; }

    bool IsSearchField { get; set; }

    public Func<object,bool> EditorVisibleFunc { get;set;}

    public Func<object, bool> EditorEnabledFunc { get; set; }

    object GetDefaultValue { get; }

    void SetSearchValue(object Value);

    object GetSearchValue();

    void ValidateObject();

    public string BindingField { get; set; }

    public int Order { get; set; }

    public bool EditorEnabled { get; set; } 

    public bool EditorVisible { get; set; } 

    public bool GridVisible { get; set; } 

    public int xs { get; set; }

    public int sm { get; set; }

    public int md { get; set; }

    public int lg { get; set; }

    public int xl { get; set; }

    public int xxl { get; set; }

    public INonGenGrid ParentGrid { get; set; }

    public object Model { get; set; }

    public string Label { get; set; }

    public bool Required { get; set; }

    public bool Error { get; set; }

    public string ErrorText { get; set; }
 
    void SetEmpty();

    public object GetValue();

    public void SetValue(object value);

    public Task Clear();

    public bool Validate();
}

