namespace Generator.Components.Interfaces;

public interface IGenComponent:IGenCompRenderer
{
    public Func<object,bool> EditorVisibleIf { get;set;}

    public Func<object, bool> DisabledIf { get; set; }
    
    public IPageBase Parent { get; set; }

    string BindingField { get; set; }
    
    public int Order { get; set; }

    //public bool EditorEnabled { get; set; } 

    public bool EditorVisible { get; set; } 

    public bool GridVisible { get; set; }
    
    public int xs { get; set; }

    public int sm { get; set; }

    public int md { get; set; }

    public int lg { get; set; }

    public int xl { get; set; }

    public int xxl { get; set; }

    public object Model { get; set; }

    string Label { get; set; }
 
    bool IsEditorVisible(object Model);

  
}
public interface IGenControl : IGenComponent
{
    Type DataType { get; set; }

    bool IsSearchField { get; set; }
 
    public Func<object, bool> RequiredIf { get; set; }

    public bool Required { get; set; }
    bool IsRequired(object Model);
    
    object GetDefaultValue { get; }

    void SetSearchValue(object Value);

    object GetValue();

    void ValidateObject();
 
    public bool ClearIfNotVisible { get; set; }
 

 
    public bool Error { get; set; }

    public string ErrorText { get; set; }
 
    void SetEmpty();

    // public object GetValue();

    public void SetValue(object value);

    public Task ClearAsync();

    public bool Validate();

    void ValidateField();

   
}

