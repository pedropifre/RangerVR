using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailColider : MonoBehaviour
{
    public TrailRenderer trail; //the trail
    public Mesh TrailFollower;

    public int poolSize = 5;
    GameObject[] pool;

    void Update()
    {
        trail.BakeMesh(TrailFollower);
    }

    // Update is called once per frame
    
}
