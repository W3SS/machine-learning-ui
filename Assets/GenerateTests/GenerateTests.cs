using System.Collections;
using System.Collections.Generic;
//using System;
using UnityEngine;

public class GenerateTests : MonoBehaviour
{

	public int trainingSetLength;

	public GameObject redPrefab;
	public GameObject bluePrefab;
	public GameObject greenPrefab;
	public GameObject plotPrefab;

	public List<GameObject> blueSamples = new List<GameObject>();
	public List<GameObject> redSamples = new List<GameObject>();
	public List<GameObject> greenSamples = new List<GameObject>();
	public List<GameObject> plotSamples = new List<GameObject>();

	public Material redMaterial;
	public Material blueMaterial;
	public Material greenMaterial;

	public void ClearGraph() {
		foreach (var blue in blueSamples)
		{
			Destroy(blue);
		}
		foreach (var red in redSamples)
		{
			Destroy(red);
		}
		foreach (var green in greenSamples)
		{
			Destroy(green);
		}
		foreach (var plotPoint in plotSamples)
		{
			Destroy(plotPoint);
		}
		greenSamples.Clear();
		redSamples.Clear();
		blueSamples.Clear();
		plotSamples.Clear();
	}

	public void GenerateMultiClassHardModel() {
		ClearGraph ();

		int indexColor = 0;

		for (int areaToPopulateX = -10; areaToPopulateX < 10; areaToPopulateX = areaToPopulateX+ 5) {
			for (int areaToPopulateZ = -10; areaToPopulateZ < 10; areaToPopulateZ = areaToPopulateZ + 5) {
				for (var i = 0; i < 10; i++) {
					var xRdm = Random.Range (areaToPopulateX, areaToPopulateX + 5);
					var zRdm = Random.Range (areaToPopulateZ, areaToPopulateZ + 5);

					if (indexColor > 2) {
						indexColor = 0;
					}

					if (indexColor == 0) {
						blueSamples.Add (Instantiate (bluePrefab, new Vector3 (xRdm, 1f, zRdm), Quaternion.identity));
					} else if (indexColor == 1) {
						redSamples.Add (Instantiate (redPrefab, new Vector3 (xRdm, 1f, zRdm), Quaternion.identity));
					} else if (indexColor == 2) {
						greenSamples.Add (Instantiate (greenPrefab, new Vector3 (xRdm, 1f, zRdm), Quaternion.identity));
					}
				}
				indexColor++;
			}
        }

        for (int a = -10; a < 10; a++)
        {
            for (int b = -10; b < 10; b++)
            {
                plotSamples.Add(Instantiate(plotPrefab, new Vector3(a, 0f, b), Quaternion.identity));
            }
        }

    }
}
