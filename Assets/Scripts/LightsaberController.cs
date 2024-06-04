using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsaberController : MonoBehaviour
{
    public Lightsaber lightsaber; // Reference to the Lightsaber script

    void EnableCollider()
    {
        Debug.Log("Collider Enabled");
        lightsaber.EnableCollider();
    }

    void DisableCollider()
    {
        Debug.Log("Collider Disabled");
        lightsaber.DisableCollider();
    }
}
