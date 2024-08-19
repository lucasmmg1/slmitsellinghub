using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    protected Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    void Update()
    {
        // todo: change this call to where we update the players position
        var direction = camera.transform.position - transform.position;
        transform.rotation = Quaternion.Euler(-camera.transform.rotation.eulerAngles.x, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, 0);
    }
}
