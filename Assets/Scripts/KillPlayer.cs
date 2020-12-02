using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private PlayerController player;
    private void Start()
    {
        player = PlayerController.player;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<PlayerController>() != null && tag == "Bus")
        {
            //Destroy(collision.gameObject);
            player.transform.localScale = new Vector3(1, 0.2f, 1);
            player.Lose();
        }
        else if (collision.collider.GetComponent<PlayerController>() != null && tag == "Water")
        {
            Destroy(collision.gameObject);
            //player.transform.localScale = new Vector3(1, 0.2f, 1);
            player.Lose();
        }
    }
}
