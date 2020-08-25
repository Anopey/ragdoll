﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomFaceContactApplySpeedObject : MonoBehaviour //TODO we use this because the normal force script is not a force object. in the future we could combine the two!
{

    private void Start()
    {
        massMultiplier = isPure ? 1 : sourceMassObject.GetMass();
    }

    [SerializeField]
    private ForceObject sourceMassObject;

    [SerializeField]
    private bool isPure;

    [SerializeField]
    private float speedTransferranceMultipler = 1;

    private float massMultiplier = 1f;

    private Dictionary<ForceObject, Vector3> lastSpeedApplied = new Dictionary<ForceObject, Vector3>();

    private void OnCollisionEnter(Collision collision)
    {
        ForceObject forceObject = collision.collider.transform.GetComponent<ForceObject>();
        if (forceObject == null)
            return;

        lastSpeedApplied.Add(forceObject, Vector3.zero);
    }

    private void OnCollisionStay(Collision collision)
    {
        ForceObject forceObject = collision.collider.transform.GetComponent<ForceObject>();
        if (forceObject == null)
            return;

        Vector3 normal = collision.GetContact(0).normal;

        if (Vector3.Angle(sourceMassObject.GetRecentNetSpeed(), normal) > 90)
            return;

        Vector3 projection = Vector3.Project(sourceMassObject.GetRecentNetSpeed() + sourceMassObject.GetRecentNetAcceleration(), normal); //TODO this bad. better if after physics

        Vector3 speedApplication = projection * massMultiplier / (isPure ? 1 : forceObject.GetMass()) * speedTransferranceMultipler;

        forceObject.DirectAdjustAddSpeed(speedApplication - lastSpeedApplied[forceObject]);

        lastSpeedApplied[forceObject] = speedApplication; //APPLY AFTER SYSTEM
    }

    private void OnCollisionExit(Collision collision)
    {
        ForceObject forceObject = collision.collider.transform.GetComponent<ForceObject>();
        if (forceObject == null)
            return;

        lastSpeedApplied.Remove(forceObject);
    }

}