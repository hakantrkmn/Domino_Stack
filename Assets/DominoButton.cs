using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DominoButton : MonoBehaviour
{
    public Transform ground;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        ground.DOLocalRotate(new Vector3(30, 0, 0), 3f, RotateMode.LocalAxisAdd);
    }
}
