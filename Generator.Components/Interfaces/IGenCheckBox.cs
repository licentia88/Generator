﻿namespace Generator.Components.Interfaces;

public interface IGenCheckBox : IGenComponent
{

    public string TrueText { get; set; }

    public string FalseText { get; set; }

    //public  Action<bool> ValueChangedAction { get; set; }
    public void SetValue(bool value);



}
