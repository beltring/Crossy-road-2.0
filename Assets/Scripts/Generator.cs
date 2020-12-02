using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private ObjectPooler OP;
    private bool firstTerrain = true;
    private List<GameObject> currentTerrains = new List<GameObject>();
    [SerializeField] private List<string> terrainsTag;

    [Header("Minimum distance from player")]
    public int minDistanceFromPlayer;

    [HideInInspector]
    public Vector3 currentPosition = new Vector3(0, 0, 0);

    [Header("Maximum number of terrain elements")]
    public int maxTerrainCount;

    void Start()
    {
        OP = ObjectPooler.SharedInstance;

        for (int i = 0; i < maxTerrainCount; i++)
        {
            SpawnTerrain(true, new Vector3(0, 0, 0));
        }
    }

    void Update()
    {
    }

    public void SpawnTerrain( bool isStart, Vector3 playerPosition)
    {
        if ((currentPosition.x - playerPosition.x < minDistanceFromPlayer) || isStart)
        {
            GameObject terrain = GetRandomObject();

            currentTerrains.Add(terrain);
            terrain.transform.position = currentPosition;
            terrain.SetActive(true);

            if (!isStart)
            {
                if (currentTerrains.Count > maxTerrainCount)
                {
                    currentTerrains[0].SetActive(false);
                    currentTerrains.RemoveAt(0);
                }
            }

            currentPosition.x++;
        }
    }

    public GameObject GetRandomObject()
    {
        string tag = terrainsTag[Random.Range(0, 3)];
        GameObject terrain = null;
        GameObject coin = null;

        if (firstTerrain)
        {
            terrain = OP.GetPooledObject(Random.Range(2, 6));
            firstTerrain = false;
        }
        else
        {
            if (tag == "Grass")
            {
                terrain = OP.GetPooledObject(Random.Range(2, 6));
            }
            else if (tag == "Road")
            {
                terrain = OP.GetPooledObject(Random.Range(0, 2));
            }
            else if (tag == "Water")
            {
                terrain = OP.GetPooledObject(Random.Range(8, 10));
            }
        }
        
        return terrain;
    }
}
