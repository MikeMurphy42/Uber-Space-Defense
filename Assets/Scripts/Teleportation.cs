using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    
    private bool canTeleport;
    
    // Start is called before the first frame update
    void Start()
    {
        canTeleport = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canTeleport && Input.GetKeyDown(KeyCode.Space))
        {
            // If the player is at point1, teleport to point2
            if (transform.position == point1.position)
            {
                transform.position = point2.position;
            }
            // If the player is at point2, teleport to point1
            else if (transform.position == point2.position)
            {
                transform.position = point1.position;
            }
            canTeleport = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        canTeleport = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        canTeleport = false;
    }
}