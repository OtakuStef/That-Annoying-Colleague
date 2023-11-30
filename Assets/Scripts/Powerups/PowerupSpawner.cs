using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class PowerupSpawner : MonoBehaviour
{
    private List<Vector3> planeCorners = new List<Vector3>();
    private List<Vector3> edgeVectors = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        findPlaneCoordinates();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 spawnPoint = getRandomSpawnPoint();
        Debug.Log(spawnPoint);
    }

    private void findPlaneCoordinates()
    {
        GameObject spawnArea = GameObject.FindGameObjectWithTag("SpawnArea");
        List<Vector3> planeVerticeList = new List<Vector3>(spawnArea.GetComponent<MeshFilter>().sharedMesh.vertices);

        CalculateCornerPoints(planeVerticeList);
    }

    private void CalculateCornerPoints(List<Vector3> planeVerticeList)
    {
        planeCorners.Clear();

        planeCorners.Add(transform.TransformPoint(planeVerticeList[0]));
        planeCorners.Add(transform.TransformPoint(planeVerticeList[10]));
        planeCorners.Add(transform.TransformPoint(planeVerticeList[110]));
        planeCorners.Add(transform.TransformPoint(planeVerticeList[120]));
    }

    void CalculateEdgeVectors(int VectorCorner)
    {
        edgeVectors.Clear();

        edgeVectors.Add(planeCorners[3] - planeCorners[VectorCorner]);
        edgeVectors.Add(planeCorners[1] - planeCorners[VectorCorner]);
    }

    public Vector3 getRandomSpawnPoint()
    {
        int randomCornerIdx = Random.Range(0, 2) == 0 ? 0 : 2; //there is two triangles in a plane, which tirangle contains the random point is chosen
                                                               //corner point is chosen for triangles as the variable
        CalculateEdgeVectors(randomCornerIdx); //in case of transform changes edge vectors change too

        float u = Random.Range(0.0f, 1.0f);
        float v = Random.Range(0.0f, 1.0f);

        if (v + u > 1) //sum of coordinates should be smaller than 1 for the point be inside the triangle
        {
            v = 1 - v;
            u = 1 - u;
        }

       return planeCorners[randomCornerIdx] + u * edgeVectors[0] + v * edgeVectors[1];
    }
}
