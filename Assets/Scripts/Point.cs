using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public Transform target;
    public float followSpeed;
    private void OnValidate()
    {
        if (transform.GetSiblingIndex()!=0)
        {
            target = transform.parent.GetChild(transform.GetSiblingIndex() - 1);

        }
        else
        {
            target = GameObject.FindObjectOfType<PlayerController>().transform;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (target!=null)
        {
             transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.transform.position.x,Time.deltaTime*followSpeed), transform.position.y,
                 transform.position.z);
        }
    }
}
