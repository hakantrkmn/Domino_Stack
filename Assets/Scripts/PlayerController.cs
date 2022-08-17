using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 mouseStartPos;

    public float speed;
    public float forwardSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            var tempVector = (mouseStartPos - Input.mousePosition);
            transform.position += new Vector3(-tempVector.x, 0, 0)*Time.deltaTime*speed;
            transform.position += new Vector3(0, 0, forwardSpeed) * Time.deltaTime;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Stack>())
        {
            if (!other.GetComponent<Stack>().stacked)
            {
                other.GetComponent<Rigidbody>().isKinematic = true;
                EventManager.GetStack(other.gameObject);


            }
        }
    }

   
}
