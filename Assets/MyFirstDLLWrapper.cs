using System;
using System.Reflection;
using MyDll;

public static class MyFirstDLLWrapper
{
    public static double[] linear_create_model(int inputDimension)
    {
        return Source.linear_create_model(inputDimension);
    }

    public static int linear_fit_regression(ref double[] model, double[,] inputs, double[] outputs)
    {
        return Source.linear_fit_regression(ref model, inputs, outputs);
    }

    public static int linear_fit_classification_rosenblatt(ref double[] model, double[] inputs,
        double[] outputs, int iterationNumber, double step)
    {
        return Source.linear_fit_classification_rosenblatt(ref model, inputs, outputs, iterationNumber, step);
    }

    public static double linear_classify(ref double[] model, double[] input)
    {
        return Source.linear_classify(ref model, input);
    }

    public static double linear_predict(ref double[] model, double[] input)
    {
        return Source.linear_predict(ref model, input);
    }
    
}
