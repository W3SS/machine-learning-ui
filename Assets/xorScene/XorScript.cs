using System.Collections;
using System.Collections.Generic;
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
    void Start () {
		
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
}
