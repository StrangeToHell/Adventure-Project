using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    public Teleporter exitTeleporter;
    public float exitOffset = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            if (exitTeleporter != null)
            {
                Player player = other.GetComponent<Player>();
                player.Teleport( exitTeleporter.transform.position + exitTeleporter.transform.forward * exitOffset);
            }
        }
    }
}
