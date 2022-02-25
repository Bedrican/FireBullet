using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camera;

    // Update is called once per frame
    private void Start()
    {
        camera = GameObject.Find("Main Camera");
    }

    void Update () {
        camera.transform.position = transform.position + new Vector3(0, 25, -25);
    }
}
