using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StackController : MonoBehaviour
{
    public List<Transform> points;

    public int stackIndex;

    public PathController stackController;
    public int dominoStackIndex;

    public List<GameObject> stackObjects;
    public List<GameObject> dominosObjects;
    private void OnEnable()
    {
        EventManager.GetStack += GetStack;
        EventManager.GetDominoStack += GetDominoStack;
        EventManager.RemoveStack += RemoveStack;
    }

    private void RemoveStack(GameObject obj)
    {
        obj.transform.parent = null;
        stackIndex--;
        stackObjects.Remove(obj);
        ReArrangeStack();
    }
    
    private void RemoveStackByNumber(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var tempObj = stackObjects[stackObjects.Count - 1 - i];
            tempObj.transform.parent = null;
            stackIndex--;
            stackObjects.Remove(tempObj);
            tempObj.GetComponent<Collider>().enabled = false;
            tempObj.GetComponent<Rigidbody>().isKinematic = true;
            tempObj.transform.DOScale(0, 1);

        }
        ReArrangeStack();
    }

    private void GetDominoStack(GameObject obj)
    {
        if (dominoStackIndex>stackController.createdPoints.Count)
        {
            obj.transform.parent = null;
            obj.GetComponent<Rigidbody>().isKinematic = false;
            //obj.transform.DOScale(obj.transform.localScale*2, .5f);
            stackObjects.Remove(obj);
            ReArrangeStack();
        }
        else
        {
            obj.transform.parent = stackController.createdPoints[dominoStackIndex];
            obj.transform.DOLocalJump(Vector3.zero, 3,3,1f);
            obj.transform.DOLocalRotate(Vector3.zero, .5f);
            //obj.transform.DOScale(obj.transform.localScale*2, .5f);
            dominoStackIndex++;
            stackObjects.Remove(obj);
            dominosObjects.Add(obj);
            ReArrangeStack();
        }
        
    }

    public void ReArrangeStack()
    {
        stackIndex = 0;
        foreach (var obj in stackObjects)
        {
            if (obj.transform.parent!=points[stackIndex])
            {
                obj.GetComponent<Stack>().stacked = true;
                obj.transform.parent = points[stackIndex];
                obj.transform.localPosition = Vector3.zero;
                
                obj.transform.DOLocalRotate(Vector3.zero, .1f);
            }
            else
            {
                
            }
            stackIndex++;

        }
    }

    private void OnDisable()
    {
        EventManager.GetDominoStack -= GetDominoStack;
        EventManager.RemoveStack -= RemoveStack;

        EventManager.GetStack -= GetStack;
    }

    private void GetStack(GameObject obj)
    {
        obj.GetComponent<Stack>().stacked = true;
        obj.transform.parent = points[stackIndex];
        obj.transform.DOLocalJump(Vector3.zero, 2,1,.5f);
        obj.transform.DOLocalRotate(Vector3.zero, .5f);
        stackIndex++;
        stackObjects.Add(obj);
        
    }

  

    // Start is called before the first frame update
    void Start()
    {
        points = EventManager.GetPoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            foreach (var obj in dominosObjects)
            {
                obj.GetComponent<Rigidbody>().isKinematic = false;
                obj.GetComponent<Collider>().isTrigger = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RemoveStackByNumber(5);
        }
    }


}
