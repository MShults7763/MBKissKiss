using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform player; // Reference to the player's transform

    void LateUpdate()
    {
        // Update the camera's position to match the player's position
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
