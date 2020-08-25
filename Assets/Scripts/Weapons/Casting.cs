﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    public float TargetDistance;

    // Update is called once per frame
    void Update()
    {
        RaycastHit TheHit;
        
        if (Physics.Raycast (transform.position, transform.TransformDirection(Vector3.forward), out TheHit)) {
            TargetDistance = TheHit.distance;
            
        }
    }
}