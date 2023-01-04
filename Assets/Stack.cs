using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RayFire;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Stack : MonoBehaviour
{
    public bool stacked;
    public bool onDomino;

    public bool onLevelEnd;


    private void OnEnable()
    {
        EventManager.LevelEnd += LevelEnd;
    }

    private void OnDisable()
    {
        EventManager.LevelEnd -= LevelEnd;
    }

    private void LevelEnd()
    {
        onLevelEnd = true;
    }

    public void CrashStack()
    {
        transform.parent = null;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().isTrigger = true;
        transform.GetChild(0).gameObject.SetActive(true);

        GetComponent<RayfireBomb>().Explode(0);
    }

    public void RemoveFromStack()
    {
        transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-.1f, .1f), 2f, .5f) * 10, ForceMode.VelocityChange);
        GetComponent<Stack>().stacked = false;
    }

    public void GoToDomino(Transform parent)
    {
        transform.parent = parent;
        transform.DOLocalJump(Vector3.zero, 3, 3, 1f);
        transform.DOLocalRotate(Vector3.zero, .5f);
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
        else if (other.GetComponent<StackGate>() && !onDomino && stacked)
        {
            GetComponent<Collider>().isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;
            onDomino = true;
            EventManager.GetDominoStack(gameObject);
        }
        else if (other.GetComponent<MathDoor>())
        {
            if (other.GetComponent<MathDoor>().value>0)
            {
                other.gameObject.SetActive(false);
                for (int i = 0; i < other.GetComponent<MathDoor>().value; i++)
                {
                    var temp = Instantiate(gameObject, transform.position, Quaternion.identity);
                    EventManager.GetStack(temp);
                }
            }
            else
            {
                other.gameObject.SetActive(false);
                EventManager.RemoveStackByNumber(-other.GetComponent<MathDoor>().value);
            }
            
        }
        else if (other.GetComponent<Obstacle>())
        {
            if (!onDomino&& stacked)
            {
                /*GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-.1f, .1f), 0, 5) * 10, ForceMode.VelocityChange);
                Vector3 torque;
                torque.x = Random.Range(-200, 200);
                torque.y = Random.Range(-200, 200);
                torque.z = Random.Range(-200, 200);
                GetComponent<Rigidbody>().angularVelocity = torque;
                stacked = false;*/
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
            if (!other.transform.GetComponent<Stack>().stacked && stacked && !other.transform.GetComponent<Stack>().onDomino)
            {
                other.transform.GetComponent<Rigidbody>().isKinematic = true;

                EventManager.GetStack(other.gameObject);
            }

            if (other.transform.GetComponent<Stack>().onDomino&& onLevelEnd)
            {
                EventManager.MoveCube(transform);
                onLevelEnd = false;
            }
        }
    }
}