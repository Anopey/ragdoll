﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTeleport : MonoBehaviour
{
    public Transform checkpoint;
    public GameObject Player;

    void OnTriggerEnter(Collider other)
    {
        Player.transform.position = checkpoint.transform.position;
    }

    void Update()
    {
        
    }
}
