using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 mouseStartPos;

    public float speed;
    public float forwardSpeed;

    public bool canMove;

    public Transform forcePoint;
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
        transform.GetChild(0).GetComponent<Collider>().isTrigger = false;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddExplosionForce(30,forcePoint.position,5);
        Time.timeScale = 4;
    }

    // Start is called before the first frame update
    void Start()
    {
        DOTween.SetTweensCapacity(1000,500);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
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
        else if (other.GetComponent<Obstacle>())
        {
            EventManager.ReArrangeStack();
        }

        if (other.CompareTag("Finish"))
        {
            canMove = false;
            EventManager.LevelEnd();
            transform.position = EventManager.GetLevelEndPlayerPos().position;
        }
    }

   
}
