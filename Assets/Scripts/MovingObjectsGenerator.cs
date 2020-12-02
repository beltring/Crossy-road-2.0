using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovingObjectsGenerator : MonoBehaviour
{
    private const int BUS_GO_LIST_ID = 7;
    private const int LOG_GO_LIST_ID = 6;

    private ObjectPooler OP;
    private List<GameObject> currentMovingObjects = new List<GameObject>();
    [SerializeField] private GameObject movingObject;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;

    private void Start()
    {
        OP = ObjectPooler.SharedInstance;
        StartCoroutine(SpawnVehicle());
    }

    private IEnumerator SpawnVehicle()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            if (movingObject.tag == "Bus")
            {
                movingObject = OP.GetPooledObject(BUS_GO_LIST_ID);
            }
            else if (movingObject.tag == "Log")
            {
                movingObject = OP.GetPooledObject(LOG_GO_LIST_ID);
            }
            movingObject.transform.position = spawnPosition.position;
            movingObject.transform.rotation = spawnPosition.rotation;
            movingObject.SetActive(true);
        }
    }
}
