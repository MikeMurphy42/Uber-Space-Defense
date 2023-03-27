using MoreMountains.TopDownEngine;
using UnityEngine;

public class DelayedSpawner : MonoBehaviour
{
    public float delayTime = 5f; // Delay time in seconds
    public TimedSpawner spawner; // Reference to the timed spawner component

    private void Start()
    {
        spawner.enabled = false; // Disable the spawner component at the start
        Invoke("EnableSpawner", delayTime); // Invoke the method after the specified delay time
    }

    private void EnableSpawner()
    {
        spawner.enabled = true; // Enable the spawner component after the delay time has passed
    }
}
