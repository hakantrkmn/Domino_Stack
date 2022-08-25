using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.MoveCube += MoveCube;
    }

    private void OnDisable()
    {
        EventManager.MoveCube -= MoveCube;
    }

    private void MoveCube(Transform obj)
    {
        transform.position = obj.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
