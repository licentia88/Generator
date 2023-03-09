using System.Reflection;
using System.Reflection.Emit;

namespace Generator.Server.Helpers;

public class ClassGenerator
{
    AssemblyName asemblyName;

    public ClassGenerator(string ClassName)
    {
        asemblyName = new AssemblyName(ClassName);
    }

    public ClassGenerator()
    {
        asemblyName = new AssemblyName("Dynamic"); 
    }



    public object GenerateClass(IDictionary<string, Type> item)
    {
        if (item.Count == 0) return null;

        var DynamicClass = CreateClass();

        CreateConstructor(DynamicClass);

        foreach (var field in item)
        {
            CreateProperty(DynamicClass, field.Key, field.Value);

        }

        Type type = DynamicClass.CreateTypeInfo();

        return Activator.CreateInstance(type);
    }

    public object GenerateClass(IDictionary<string, object> item)
    {
        if (item.Count == 0) return null;

        var DynamicClass = CreateClass();

        CreateConstructor(DynamicClass);

        foreach (var field in item)
        {
            CreateProperty(DynamicClass, field.Key, field.Value.GetType());

        }

        Type type = DynamicClass.CreateTypeInfo();

        return Activator.CreateInstance(type);
    }

   

    private TypeBuilder CreateClass()
    {
        AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(asemblyName, AssemblyBuilderAccess.Run);
        ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
        TypeBuilder typeBuilder = moduleBuilder.DefineType(asemblyName.FullName
                            , TypeAttributes.Public |
                            TypeAttributes.Class |
                            TypeAttributes.AutoClass |
                            TypeAttributes.AnsiClass |
                            TypeAttributes.BeforeFieldInit |
                            TypeAttributes.AutoLayout
                            , null);



        Type[] ctorParams = new Type[] {  };
        ConstructorInfo classCtorInfo = typeof(SerializableAttribute).GetConstructor(ctorParams);

        CustomAttributeBuilder myCABuilder = new CustomAttributeBuilder(
                            classCtorInfo, new object[] { });
        //new object[] );


        typeBuilder.SetCustomAttribute(myCABuilder);

        return typeBuilder;
    }
    private void CreateConstructor(TypeBuilder typeBuilder)
    {
        typeBuilder.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
    }
    private void CreateProperty(TypeBuilder typeBuilder, string propertyName, Type propertyType)
    {
        FieldBuilder fieldBuilder = typeBuilder.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

        PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
        MethodBuilder getPropMthdBldr = typeBuilder.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
        ILGenerator getIl = getPropMthdBldr.GetILGenerator();

        getIl.Emit(OpCodes.Ldarg_0);
        getIl.Emit(OpCodes.Ldfld, fieldBuilder);
        getIl.Emit(OpCodes.Ret);

        MethodBuilder setPropMthdBldr = typeBuilder.DefineMethod("set_" + propertyName,
              MethodAttributes.Public |
              MethodAttributes.SpecialName |
              MethodAttributes.HideBySig,
              null, new[] { propertyType });

        ILGenerator setIl = setPropMthdBldr.GetILGenerator();
        Label modifyProperty = setIl.DefineLabel();
        Label exitSet = setIl.DefineLabel();

        setIl.MarkLabel(modifyProperty);
        setIl.Emit(OpCodes.Ldarg_0);
        setIl.Emit(OpCodes.Ldarg_1);
        setIl.Emit(OpCodes.Stfld, fieldBuilder);

        setIl.Emit(OpCodes.Nop);
        setIl.MarkLabel(exitSet);
        setIl.Emit(OpCodes.Ret);

        propertyBuilder.SetGetMethod(getPropMthdBldr);
        propertyBuilder.SetSetMethod(setPropMthdBldr);
    }
}
 