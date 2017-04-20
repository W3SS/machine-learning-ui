using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateCrossScript : MonoBehaviour
{
    public int trainingSetLength;

    public GameObject redPrefab;
    public GameObject bluePrefab;
    public GameObject plotPrefab;

    public List<GameObject> blueSamples = new List<GameObject>();
    public List<GameObject> redSamples = new List<GameObject>();
    public List<GameObject> plotSamples = new List<GameObject>();

    private double[] myModelClassification;

    public void GenerateCross()
    {
        foreach (var blue in blueSamples)
        {
            Destroy(blue);
        }
        foreach (var red in redSamples)
        {
            Destroy(red);
        }
        foreach (var plotPoint in plotSamples)
        {
            Destroy(plotPoint);
        }
        redSamples.Clear();
        blueSamples.Clear();
        plotSamples.Clear();
        for (var i = 0; i < trainingSetLength; i++)
        {
            var xRdm = Random.Range(-10f, 10f);
            var zRdm = Random.Range(-10f, 10f);

            if (Mathf.Abs(xRdm) <= 2.5f || Mathf.Abs(zRdm) <= 2.5f)
            {
                blueSamples.Add(
                    Instantiate(bluePrefab, new Vector3(xRdm, 0f, zRdm), Quaternion.identity));
            }
            else
            {
                redSamples.Add(
                Instantiate(redPrefab, new Vector3(xRdm, 0f, zRdm), Quaternion.identity));
            }
        }
        for (var x = -10; x <= 10; x++)
        {
            for (var z = -10; z <= 10; z++)
            {
                plotSamples.Add(
                    Instantiate(plotPrefab, new Vector3(x, -2f, z), Quaternion.identity));
            }
        }

        myModelClassification = MyFirstDLLWrapper.linear_create_model(2);
    }

    public void ResolveLinear()
    {
        double[] myModel = MyFirstDLLWrapper.linear_create_model(2);

        double[,] inputs = new double[blueSamples.Count + redSamples.Count, 2];
        double[] outputs = new double[blueSamples.Count + redSamples.Count];
        int i = 0;
        foreach (var blue in blueSamples)
        {
            inputs[i, 0] = blue.transform.position.x;
            inputs[i, 1] = blue.transform.position.z;
            outputs[i++] = 0;
        }
        foreach (var red in redSamples)
        {
            inputs[i, 0] = red.transform.position.x;
            inputs[i, 1] = red.transform.position.z;
            outputs[i++] = 1;
        }
        MyFirstDLLWrapper.linear_fit_regression(ref myModel, inputs, outputs);



        foreach (var white in plotSamples)
        {
            Destroy(white);
        }
        plotSamples.Clear();

        for (int a = -10; a <= 10; a++)
        {
            for (int b = -10; b <= 10; b++)
            {
                var rslt = MyFirstDLLWrapper.linear_predict(ref myModel, new double[] { a, b });

                var elt = Instantiate(plotPrefab, new Vector3(a, -2f, b), Quaternion.identity);
                elt.GetComponent<Renderer>().material.color = new Color((float)rslt, 0, 1 - (float)rslt);
                plotSamples.Add(elt);
            }
        }
    }

    public void ResolveClassification()
    {
        List<GameObject> balls = new List<GameObject>();
        balls.AddRange(blueSamples);
        balls.AddRange(redSamples);
        System.Random rnd = new System.Random();
        balls = balls.OrderBy(x => rnd.Next()).ToList();

        foreach (var ball in balls)
        {
            double[] input = new double[] { ball.transform.position.x, ball.transform.position.z };
            double[] output = new double[] { blueSamples.Contains(ball) ? 1 : -1 };

            MyFirstDLLWrapper.linear_fit_classification_rosenblatt(ref myModelClassification, input, output, 100, 0.01);

        }

        foreach (var white in plotSamples)
        {
            Destroy(white);
        }
        plotSamples.Clear();

        for (int a = -10; a <= 10; a++)
        {
            for (int b = -10; b <= 10; b++)
            {
                var rslt = MyFirstDLLWrapper.linear_classify(ref myModelClassification, new double[] { a, b });

                if (rslt > 0)
                {
                    plotSamples.Add(Instantiate(bluePrefab, new Vector3(a, -2, b), Quaternion.identity));
                }
                else
                {
                    plotSamples.Add(Instantiate(redPrefab, new Vector3(a, -2, b), Quaternion.identity));
                }
            }
        }
    }
}