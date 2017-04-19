using System;
using System.Reflection;

public static class MyFirstDLLWrapper
{
    private static Type myType;

    public static IntPtr linear_create_model(int inputDimension)
    {
        return (IntPtr) callFunction("linear_create_model", new object[] {inputDimension});
    }

    public static void linear_remove_model(IntPtr model)
    {
        callFunction("linear_remove_model", new object[] {model});
    }

    public static int linear_fit_regression(IntPtr model, double[] inputs, int inputsSize, int inputSize, double[] outputs, int outputsSize)
    {
        return (int) callFunction("linear_fit_regression", new object[] {model, inputs, inputSize, outputs});
    }

    public static int linear_fit_classification_hebb(IntPtr model, double[] inputs, int inputsSize, int inputSize,
        int iterationNumber, double step)
    {
        return (int)callFunction("linear_fit_classification_hebb",
                new object[] {model, inputs, inputSize, iterationNumber, step});
    }

    public static int linear_fit_classification_rosenblatt(IntPtr model, double[] inputs, int inputsSize, int inputSize,
        double[] outputs, int outputsSize, int iterationNumber, double step)
    {
        return (int)callFunction("linear_fit_classification_rosenblatt",
                new object[] {model, inputs, inputSize, outputs, iterationNumber, step});
    }

    public static double linear_classify(IntPtr model, double[] input, int inputSize)
    {
        return (double) callFunction("linear_classify", new object[] {model, input});
    }

    public static double linear_predict(IntPtr model, double[] input, int inputSize)
    {
        return (double) callFunction("linear_predict", new object[] {model, input});
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

    class model
    {

    }
}
