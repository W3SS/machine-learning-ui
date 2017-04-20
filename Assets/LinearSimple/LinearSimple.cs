using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LinearSimple : MonoBehaviour
{
    public GameObject bluePrefab;
    public GameObject redPrefab;
    public GameObject greenPrefab;
    public GameObject whitePrefab;

    public List<GameObject> blueSamples = new List<GameObject>();
    public List<GameObject> redSamples = new List<GameObject>();
    public List<GameObject> whiteSamples = new List<GameObject>();
    public List<GameObject> greenSamples = new List<GameObject>();

    private double[] myModelClassification;

	// Use this for initialization
	void Start () {
        myModelClassification = MyFirstDLLWrapper.linear_create_model(2);
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


    public void MultiClassSoftComplexe()
    {
        EquationDeDroite droite1 = new EquationDeDroite();
        droite1.calclulDroiteParAbscisse(5f);
        EquationDeDroite droite2 = new EquationDeDroite();
        droite2.calclulDroite(3f, 3f, 5f, 5f);
        EquationDeDroite droite3 = new EquationDeDroite();
        droite3.calclulDroite(1f, 9f, 9f, 1f);
        List<float> numberaleat = new List<float>();
        clearData();

        for (int i = 0; i < 50; i++)
        {
            var xrand = Random.Range(1f, 10f);
            var yrand = Random.Range(1f, 10f);
            bool check1 = droite1.EquationLessToZero(xrand, yrand);

            bool check2 = droite2.EquationMoreToZero(xrand, yrand);

            bool check3 = droite1.EquationMoreToZero(xrand, yrand);

            bool check4 = droite3.EquationMoreToZero(xrand, yrand);

            bool check5 = droite2.EquationLessToZero(xrand, yrand);
            bool check6 = droite3.EquationLessToZero(xrand, yrand);
            if (check1 && check2)
            {
                greenSamples.Add(Instantiate(greenPrefab, new Vector3(xrand, 1f, yrand), Quaternion.identity));
            }
            else if (check3 && check4)
            {
                redSamples.Add(Instantiate(redPrefab, new Vector3(xrand, 1f, yrand), Quaternion.identity));
            }
            else if (check5 && check6)
            {
                blueSamples.Add(Instantiate(bluePrefab, new Vector3(xrand, 1f, yrand), Quaternion.identity));
            }
        }


        for (int a = 1; a <= 10; a++)
        {
            for (int b = 1; b <= 10; b++)
            {
                whiteSamples.Add(Instantiate(whitePrefab, new Vector3(a, 0f, b), Quaternion.identity));
            }
        }
    }
    public void GenerateTestValues(int nbValues)
    {
        clearData();

        for (int i = 0; i < nbValues; i++)
        {
            if (i % 2 == 0)
            {   // Bleu
                blueSamples.Add(Instantiate(bluePrefab, new Vector3(Random.Range(1f, 10f), 1f, Random.Range(1f, 5f)), Quaternion.identity));
            }
            else
            {   // Rouge
                redSamples.Add(Instantiate(redPrefab, new Vector3(Random.Range(1f, 10f), 1f, Random.Range(6f, 10f)), Quaternion.identity));
            }
        }

        for (int a = 1; a <= 10; a++)
        {
            for (int b = 1; b <= 10; b++)
            {
                whiteSamples.Add(Instantiate(whitePrefab, new Vector3(a, 0f, b), Quaternion.identity));
            }
        }

        Start();
    }

    public void ResolveLinear()
    {
        double[] myModel = MyFirstDLLWrapper.linear_create_model(2);
        
        double[,] inputs = new double[blueSamples.Count + redSamples.Count,2];
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

       

        foreach (var white in whiteSamples)
        {
           Destroy(white);
        }
        whiteSamples.Clear();

        for (int a = 1; a <= 10; a++)
        {
            for (int b = 1; b <= 10; b++)
            {
                var rslt = MyFirstDLLWrapper.linear_predict(ref myModel, new double[] {a, b});

                var elt = Instantiate(whitePrefab, new Vector3(a, 0f, b), Quaternion.identity);
                elt.GetComponent<Renderer>().material.color = new Color((float) rslt, 0, 1 - (float)rslt);
                whiteSamples.Add(elt);
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
			double[] input = new double[]{ball.transform.position.x, ball.transform.position.z};
			double[] output = new double[] { blueSamples.Contains(ball) ? 1 : -1 };

			MyFirstDLLWrapper.linear_fit_classification_rosenblatt(ref myModelClassification, input, output, 100, 0.01);

		}

		foreach (var white in whiteSamples)
		{
			Destroy(white);
		}
		whiteSamples.Clear();

		for (int a = 1; a <= 10; a++)
		{
			for (int b = 1; b <= 10; b++)
			{
				var rslt = MyFirstDLLWrapper.linear_classify(ref myModelClassification, new double[] { a, b });

				if (rslt > 0)
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

public class EquationDeDroite
{
    public float X { get; set; }
    public float Y { get; set; }

    public float A { get; set; }
    public float P { get; set; }

    public EquationDeDroite(float a, float p)
    {
        A = a;
        P = p;
    }
    public EquationDeDroite()
    {
        X = -100000000;
        Y = -100000000;
    }

    public void calclulDroiteParAbscisse(float p)
    {
        P = p;
        Y = 0;
    }

    public void calclulDroiteParOrdonnée(float p)
    {
        P = p;
        X = 0;
    }

    public void calclulDroite()
    {

        //calcul de A
        A = 1;
        // calcul de P
        P = 0;

    }
    public void calclulDroite(float Xa, float Ya, float Xb, float Yb)
    {
        if (Xa == Xb)
        {
            P = Xa;
            Y = 0;
        }
        else if(Xa == Ya && Xb == Yb)
        {
            P = 0;
            A = 1;
        }
        else
        {
            //calcul de A
            A = (Yb - Ya) / (Xb - Xa);
            // calcul de P
            P = Yb - (A * Xb);
        }
    }
    public bool EquationEqualsToZero(float x, float y)
    {
        if ((A * x + P) == y)
        {
            return true;
        }
        return false;
    }

    public bool EquationMoreToZero(float x, float y)
    {
        if (A == 0 && Y == 0)
        {
            return (P > x) ? false : true;
        }

        if ((A * x + P) < y)
        {
            return true;
        }
        return false;
    }

    public bool EquationLessToZero(float x, float y)
    {
        if(A == 0 && Y == 0)
        {
            return (P > x) ? true : false;
        }
        if ((A * x + P) > y)
        {
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        string str;
        if (P == 0 && A == 1)
        {
            str = "y = x";
        }
        else if (X == 0 && Y != 0)
        {
            str = "y=" + P;
        }
        else if( X !=0 && Y == 0)
        {
            str = "x=" + P;
        }
        else
        {
            str = "y=" + A + "x+" + P;
        }
        return str;
    }
}
