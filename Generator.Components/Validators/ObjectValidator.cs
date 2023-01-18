using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Reflection;
using Generator.Components.Interfaces;
using Generator.Shared.Extensions;
using Microsoft.FSharp.Data.UnitSystems.SI.UnitNames;
using static MudBlazor.CategoryTypes;

namespace Generator.Components.Validators;



//public class ObjectValidator<TComponent> where TComponent : IGenComponent
//{

//    public ICollection<ValidationResult> results  { get; }

//    public ObjectValidator()
//    {
//        results = new List<ValidationResult>();
//    }

//    internal void Validate(TComponent component)
//    {
//        if (component.Model is null) return;


//        if(component.Model is ExpandoObject or Dictionary<string, object>)
//        {
//            if (component.Required)
//            {
//                var resIsNull = component.Model.GetPropertyValue(component.BindingField).IsNullOrDefault();

//                if (resIsNull)
//                {
//                    component.Error = true;

//                    component.ErrorText = $"Required";

//                    return;
//                }               
//            }

//            //if (component.HasProperty(nameof(IGenTextField.MaxLength)))
//            //{
//            //    var maxlength = component.GetPropertyValue(nameof(IGenTextField.MaxLength)).CastTo<int>();

//            //    var compLength = component.Model.GetPropertyValue(component.BindingField)?.ToString()?.Length??0;

//            //    var lengthresult = compLength < maxlength;

//            //    if (!lengthresult)
//            //    {
//            //        component.Error = true;

//            //        component.ErrorText = $"The {component.BindingField} can not be more then {maxlength} characters";


//            //        return;
//            //    }
//            //}

//            component.ErrorText = string.Empty;
//            component.Error = false;

//            return;
//        }

//        var custArgument = component.Model.GetType()
//                    .GetProperty(component.BindingField).GetCustomAttribute<DisplayNameAttribute>(true);

//        var displayName = custArgument == null ? component.BindingField : custArgument.DisplayName.ToString();
  
//        var valCOntext = new ValidationContext(component.Model)
//        {
//            MemberName = component.BindingField,
//            DisplayName = displayName

//        };

       

//        var result = Validator.TryValidateProperty(component.Model.GetPropertyValue(component.BindingField), valCOntext, results);


//        component.Error = false;
//    }

//}