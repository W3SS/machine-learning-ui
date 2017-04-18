using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearSimple : MonoBehaviour
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
                blueSamples.Add(Instantiate(bluePrefab, new Vector3(Random.Range(1f, 10f), 1f, Random.Range(1f,5f)), Quaternion.identity));
            }
            else
            {   // Rouge
                redSamples.Add(Instantiate(redPrefab, new Vector3(Random.Range(1f, 10f), 1f, Random.Range(6f, 10f)), Quaternion.identity));
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
