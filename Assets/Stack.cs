using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stack : MonoBehaviour
{
    public bool stacked;
    public bool onDomino;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Stack>())
        {
            if (!other.transform.GetComponent<Stack>().stacked && stacked)
            {
                other.transform.GetComponent<Rigidbody>().isKinematic = true;

                EventManager.GetStack(other.gameObject);

            }
        }
        else if (other.GetComponent<StackGate>())
        {
            GetComponent<Collider>().isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;
            onDomino = true;
            EventManager.GetDominoStack(gameObject);
        }
        else if (other.GetComponent<MathDoor>())
        {
            other.gameObject.SetActive(false);
            for (int i = 0; i < other.GetComponent<MathDoor>().value; i++)
            {
                var temp = Instantiate(gameObject, transform.position, Quaternion.identity);
                EventManager.GetStack(temp);
            }
        }
        else if (other.GetComponent<Obstacle>())
        {
            if (!onDomino)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-2f, 2f), 2, 3) * 2, ForceMode.Impulse);
                Vector3 torque;
                torque.x = Random.Range (-200, 200);
                torque.y = Random.Range (-200, 200);
                torque.z = Random.Range (-200, 200);
                GetComponent<Rigidbody>().angularVelocity = torque;
                stacked = false;
                EventManager.RemoveStack(gameObject);
            }
           
        }

        if (other.CompareTag("Finish"))
        {
            GetComponent<Collider>().isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;
            onDomino = true;
            EventManager.GetDominoStack(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.GetComponent<Stack>())
        {
            if (!other.transform.GetComponent<Stack>().stacked && stacked)
            {
                other.transform.GetComponent<Rigidbody>().isKinematic = true;

                EventManager.GetStack(other.gameObject);

            }
        }
    }
}