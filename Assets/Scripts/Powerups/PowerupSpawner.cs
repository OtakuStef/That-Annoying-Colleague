using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class PowerupSpawner : MonoBehaviour
{
    private List<Vector3> planeCorners = new List<Vector3>();
    private List<Vector3> edgeVectors = new List<Vector3>();
    private List<Vector3> planeVerticeList = new List<Vector3>();
    public float meanSpawnDelay = 10.0f;
    private float spawnDelay = 10.0f;
    private float spawnCounter = 0.0f;
    public float spawnDelayVariance = 0.2f;
    private GameObject[] powerUps;
    public float yAxisPosition = -1.0f;
    public Vector3 point1 = new Vector3();
    private GameObject powerupContainer;

    // Start is called before the first frame update
    void Start()
    {
        findPlaneCoordinates();
        powerUps = Resources.LoadAll<GameObject>("Powerups");
        powerupContainer = GameObject.FindGameObjectWithTag("PowerupContainer");

    }

    // Update is called once per frame
    void Update()
    {
        spawnCounter += Time.deltaTime;
        if (spawnCounter >= spawnDelay)
        {
            setRandomSpawnDelay();
            spawnCounter = 0.0f;

            StartCoroutine(spawnRoutine());
        }
    }

    private IEnumerator spawnRoutine()
    {
        Vector3 spawnPoint = restrictSpawnCoordinates(getRandomSpawnPoint());
        GameObject powerUpToSpawn = GetRandomGameObject(powerUps);
        Instantiate(powerUpToSpawn, spawnPoint, Quaternion.identity, powerupContainer.transform);

        yield return null;
    }

    private GameObject GetRandomGameObject(GameObject[] objects)
    {
        if (objects != null)
        {
            int randomNumber = Random.Range(0, objects.Length);
            return objects[randomNumber];
        }
        return null;
    }

    private void setRandomSpawnDelay()
    {
        float clampedSpawnDelayVariance = Mathf.Clamp01(spawnDelayVariance);
        spawnDelay = Random.Range(meanSpawnDelay * (1- clampedSpawnDelayVariance), meanSpawnDelay * (1+ clampedSpawnDelayVariance));
    }

    private void findPlaneCoordinates()
    {
        planeVerticeList = new List<Vector3>(GetComponent<MeshFilter>().sharedMesh.vertices);

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
        int randomCornerIdx = Random.Range(0, 2) == 0 ? 0 : 2; 

        CalculateEdgeVectors(randomCornerIdx); 

        float u = Random.Range(0.0f, 1.0f);
        float v = Random.Range(0.0f, 1.0f);

        if (v + u > 1) 
        {
            v = 1 - v;
            u = 1 - u;
        }

        return planeCorners[randomCornerIdx] + u * edgeVectors[0] + v * edgeVectors[1];
    }

    public Vector3 restrictSpawnCoordinates(Vector3 spawnPoint)
    {
        float xAxis = Mathf.Clamp(spawnPoint.x, 22.1f, 35.5f);
        float zAxis = Mathf.Clamp(spawnPoint.z, -17.5f, -4.0f);

        return new Vector3(xAxis, this.yAxisPosition, zAxis);
    }

}
