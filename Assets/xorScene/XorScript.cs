using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class XorScript : MonoBehaviour {

    public GameObject bluePrefab;
    public GameObject redPrefab;
    public GameObject greenPrefab;
    public GameObject whitePrefab;

    public List<GameObject> blueSamples = new List<GameObject>();
    public List<GameObject> redSamples = new List<GameObject>();
    public List<GameObject> whiteSamples = new List<GameObject>();
    public List<GameObject> greenSamples = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void clearData()
    {
        foreach (var blue in blueSamples)
        {
            Destroy(blue);
        }
        foreach (var red in redSamples)
        {
            Destroy(red);
        }
        foreach (var white in whiteSamples)
        {
            Destroy(white);
        }
        foreach (var green in greenSamples)
        {
            Destroy(green);
        }
        blueSamples.Clear();
        redSamples.Clear();
        whiteSamples.Clear();
        greenSamples.Clear();
    }

    public void GenerateTestValueXor()
    {
        clearData();

        blueSamples.Add(Instantiate(bluePrefab, new Vector3(3f, 1f, 3f), Quaternion.identity));
        redSamples.Add(Instantiate(redPrefab, new Vector3(3f, 1f, 8f), Quaternion.identity));
        blueSamples.Add(Instantiate(bluePrefab, new Vector3(8f, 1f, 8f), Quaternion.identity));
        redSamples.Add(Instantiate(redPrefab, new Vector3(8f, 1f, 3f), Quaternion.identity));

        for (int a = 1; a <= 10; a++)
        {
            for (int b = 1; b <= 10; b++)
            {
                whiteSamples.Add(Instantiate(whitePrefab, new Vector3(a, 0f, b), Quaternion.identity));
            }
        }
    }

    public void ResolveRegressionMLP()
    {
        object myModel = MyFirstDLLWrapper.mlp_create_model(3, new int[] {2, 3, 1});

        double[][] inputs = new double[blueSamples.Count + redSamples.Count][];
        double[] outputs = new double[blueSamples.Count + redSamples.Count];
        int i = 0;
        foreach (var blue in blueSamples)
        {
            inputs[i] = new double[2];
            inputs[i][0] = blue.transform.position.x;
            inputs[i][1] = blue.transform.position.z;
            outputs[i++] = 0;
        }
        foreach (var red in redSamples)
        {
            inputs[i] = new double[2];
            inputs[i][0] = red.transform.position.x;
            inputs[i][1] = red.transform.position.z;
            outputs[i++] = 1;
        }
        MyFirstDLLWrapper.mlp_fit_regression_backdrop(myModel, inputs, outputs, 10000, 0.1);



        foreach (var white in whiteSamples)
        {
            Destroy(white);
        }
        whiteSamples.Clear();

        for (int a = 1; a <= 10; a++)
        {
            for (int b = 1; b <= 10; b++)
            {
                var rslt = MyFirstDLLWrapper.mlp_predict(myModel, new double[] { a, b });
                Debug.Log(rslt[0]);
                var elt = Instantiate(whitePrefab, new Vector3(a, 0f, b), Quaternion.identity);
                elt.GetComponent<Renderer>().material.color = new Color((float)rslt[0], 0, 1 - (float)rslt[0]);
                whiteSamples.Add(elt);
            }
        }
    }

    public void ResolveClassificationMLP()
    {
        object myModel = MyFirstDLLWrapper.mlp_create_model(3, new int[] { 2, 3, 1 });

        double[][] inputs = new double[blueSamples.Count + redSamples.Count][];
        double[] outputs = new double[blueSamples.Count + redSamples.Count];
        int i = 0;
        foreach (var blue in blueSamples)
        {
            inputs[i] = new double[2];
            inputs[i][0] = blue.transform.position.x;
            inputs[i][1] = blue.transform.position.z;
            outputs[i++] = 1;
        }
        foreach (var red in redSamples)
        {
            inputs[i] = new double[2];
            inputs[i][0] = red.transform.position.x;
            inputs[i][1] = red.transform.position.z;
            outputs[i++] = -1;
        }

        MyFirstDLLWrapper.mlp_fit_classification_backdrop(myModel, inputs, outputs, 10000, 0.1);

        foreach (var white in whiteSamples)
        {
            Destroy(white);
        }
        whiteSamples.Clear();

        for (int a = 1; a <= 10; a++)
        {
            for (int b = 1; b <= 10; b++)
            {
                var rslt = MyFirstDLLWrapper.mlp_classify(myModel, new double[] { a, b });
                //Debug.Log(rslt[0]); 
                if (rslt[0] > 0)
                {
                    whiteSamples.Add(Instantiate(bluePrefab, new Vector3(a, 0, b), Quaternion.identity));
                }
                else
                {
                    whiteSamples.Add(Instantiate(redPrefab, new Vector3(a, 0, b), Quaternion.identity));
                }
            }
        }
    }
}
