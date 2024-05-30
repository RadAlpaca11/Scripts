using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    public Ball ballAccess;
    
    [Tooltip("This is the number of paddle hits before an obstacle spawns. Must be the same as Ball's Obstacle Target Count.")]
    public int obstacleTargetCount;

    [HideInInspector] public int obstacleProgressCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ballAccess.obstacleProgressCount == obstacleTargetCount)
        {
            // Spawn random obstacle
            Debug.Log("Obstacle");
            // Reset obstacle progress count
            ballAccess.obstacleProgressCount = 0;
        }
    }
}
