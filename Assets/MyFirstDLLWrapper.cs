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

    public static int linear_fit_classification_rosenblatt(ref double[] model, double[,] inputs,
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

    public static MLP mlp_create_model(int numLayers, int[] npl)
    {
        return SourceMulti.mlp_create_model(numLayers, npl);
    }

    public static int mlp_fit_regression_backdrop(object model, double[][] inputs, double[] outputs, int iterationNumber, double step)
    {
        return SourceMulti.mlp_fit_regression_backdrop((MLP)model, inputs, outputs, iterationNumber, step);
    }

    public static int mlp_fit_classification_backdrop(object model, double[][] inputs, double[] outputs, int iterationNumber, double step)
    {
        return SourceMulti.mlp_fit_classification_backdrop((MLP)model, inputs, outputs, iterationNumber, step);
    }

    public static double[] mlp_classify(object model, double[] input)
    {
        return SourceMulti.mlp_classify((MLP)model, input);
    }

    public static double[] mlp_predict(object model, double[] input)
    {
        return SourceMulti.mlp_predict((MLP)model, input);
    }
}
