using System;
using System.Reflection;

public static class MyFirstDLLWrapper
{
    private static Type myType;

    public static int return42()
    {
        return (int) callFunction("return42", new object[] {});
    }

    private static object callFunction(string functionName, object[] param)
    {
        MethodInfo m = myType.GetMethod(functionName);
        return m.Invoke(null, param);
    }

    static MyFirstDLLWrapper()
    {
        myType = Assembly.LoadFile(@"./Assets/2017-5A-AL2-MyFirstNativeDllForUnity.dll").GetType("MyDll.Source");
    }
}
