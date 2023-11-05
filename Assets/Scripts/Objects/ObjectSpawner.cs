using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private GameObject[] objectsToSpawn;
    public float spawnDelay = 10.0f;
    private float spawnCounter = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        objectsToSpawn = Resources.LoadAll<GameObject>("WorkspacePresets");
    }

    // Update is called once per frame
    void Update()
    {
        this.spawnCounter += Time.deltaTime;
        if (this.spawnCounter >= spawnDelay)
        {
            this.spawnCounter = 0.0f;
            this.StartCoroutine(spawnRoutine());
        }
    }

    public GameObject GetRandomGameObject(GameObject[] objects)
    {
        if (objects != null)
        {
            int randomNumber = Random.Range(0, objects.Length);
            return objects[randomNumber];
        }
        return null;
    }

    IEnumerator spawnRoutine()
    {
        GameObject[] spawnpoints = GameObject.FindGameObjectsWithTag("ThrowableSpawner");
        
        var spawn = GetRandomGameObject(spawnpoints);
        var newObject = GameObject.Instantiate(GetRandomGameObject(objectsToSpawn));

        newObject.transform.position = spawn.transform.position;

        yield return null;
    }
}
