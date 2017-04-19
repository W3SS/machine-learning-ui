using System.Collections;
using System.Collections.Generic;
//using System;
using UnityEngine;

public class LinearSoft : MonoBehaviour
{

    public GameObject bluePrefab;
    public GameObject redPrefab;
    public GameObject whitePrefab;

    public List<GameObject> blueSamples = new List<GameObject>();
    public List<GameObject> redSamples = new List<GameObject>();
    public List<GameObject> whiteSamples = new List<GameObject>();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GenerateTestValues(int nbValues)
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
        blueSamples.Clear();
        redSamples.Clear();
        whiteSamples.Clear();

        for (int i = 0; i < nbValues; i++)
        {
            if (i % 2 == 0)
            {   // Bleu
                float rnd = Random.Range(1f, 10f);
                //blueSamples.Add(Instantiate(bluePrefab, new Vector3(rnd, 1f, 11f - rnd), Quaternion.identity));
                blueSamples.Add(Instantiate(bluePrefab, new Vector3(Random.Range(1f, rnd), 1f, Random.Range(1f, 11f-rnd)), Quaternion.identity));
            }
            else
            {   // Rouge
                float rnd = Random.Range(1f, 10f);
                //blueSamples.Add(Instantiate(redPrefab, new Vector3(Random.Range(rnd, 10), 1f, Random.Range(10f - rnd, 10f)), Quaternion.identity));
                blueSamples.Add(Instantiate(redPrefab, new Vector3(Random.Range(rnd, 10), 1f, Random.Range(11f - rnd, 10f)), Quaternion.identity));
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
            {   // Bleu
                float rnd = Random.Range(1f, 10f);
                //blueSamples.Add(Instantiate(bluePrefab, new Vector3(rnd, 1f, 11f - rnd), Quaternion.identity));
                blueSamples.Add(Instantiate(bluePrefab, new Vector3(rnd, 1f, 11-rnd), Quaternion.identity));
            }
            else
            {   // Rouge
                float rnd = Random.Range(1f, 10f);
                //blueSamples.Add(Instantiate(redPrefab, new Vector3(Random.Range(rnd, 10), 1f, Random.Range(10f - rnd, 10f)), Quaternion.identity));
                blueSamples.Add(Instantiate(redPrefab, new Vector3(11-rnd, 1f, rnd), Quaternion.identity));
            }
        }

        for (int a = 1; a <= 10 ; a++)
        {
            for (int b = 1; b <= 10; b++)
            {
                whiteSamples.Add(Instantiate(whitePrefab, new Vector3(a, 0f, b), Quaternion.identity));
            }
        }
    }
}
