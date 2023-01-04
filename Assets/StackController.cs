using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RayFire;
using UnityEngine;
using Random = UnityEngine.Random;

public class StackController : MonoBehaviour
{
    public List<Transform> points;

    public int stackIndex;

    public DominoController stackController;
    public int dominoStackIndex;

    public List<GameObject> stackObjects;
    public List<GameObject> dominosObjects;

    #region enable disable

    private void OnEnable()
    {
        EventManager.RemoveStackByNumber += RemoveStackByNumber;
        EventManager.LevelEnd += LevelEnd;
        EventManager.ReArrangeStack += ReArrangeStack;
        EventManager.GetStack += GetStack;
        EventManager.GetDominoStack += GetDominoStack;
        EventManager.RemoveStack += RemoveStack;
    }
    private void OnDisable()
    {
        EventManager.GetDominoStack -= GetDominoStack;
        EventManager.RemoveStack -= RemoveStack;
        EventManager.LevelEnd -= LevelEnd;
        EventManager.RemoveStackByNumber -= RemoveStackByNumber;
        EventManager.ReArrangeStack -= ReArrangeStack;

        EventManager.GetStack -= GetStack;
    }

    #endregion
    
    private void LevelEnd()
    {
        foreach (var obj in dominosObjects)
        {
            obj.GetComponent<Rigidbody>().isKinematic = false;
            obj.GetComponent<Collider>().isTrigger = false;
        }
    }

    private void RemoveStack(GameObject obj)
    {
        var index = stackObjects.IndexOf(obj);
        var templist = new List<GameObject>();
        for (int i = index; i < stackObjects.Count; i++)
        {
            var tempObj = stackObjects[i];
            templist.Add(tempObj);
            tempObj.GetComponent<Stack>().RemoveFromStack();
        }
        foreach (var Tobj in templist)
        {
            stackObjects.Remove(Tobj);
        }
        stackIndex = stackObjects.Count;

    }

    private void RemoveStackByNumber(int amount)
    {
        var tempList = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            var tempObj = stackObjects[stackIndex - 1 - i];
            tempList.Add(tempObj);
            tempObj.GetComponent<Stack>().CrashStack();
            
        }

        foreach (var obj in tempList)
        {
            stackObjects.Remove(obj);
        }

        stackIndex = stackObjects.Count;


        ReArrangeStack();
    }

    private void GetDominoStack(GameObject obj)
    {
        if (dominoStackIndex + 1 > stackController.createdPoints.Count)
        {
            obj.transform.parent = null;
            obj.GetComponent<Rigidbody>().isKinematic = false;
            stackObjects.Remove(obj);
            ReArrangeStack();
        }
        else
        {
            if (!dominosObjects.Contains(obj))
            {

                obj.GetComponent<Stack>().GoToDomino(stackController.createdPoints[dominoStackIndex]);
                dominoStackIndex++;
                stackObjects.Remove(obj);
                dominosObjects.Add(obj);
                ReArrangeStack();
            }
        }
    }

    public void ReArrangeStack()
    {
        stackIndex = 0;
        foreach (var obj in stackObjects)
        {
            if (obj.transform.parent != points[stackIndex])
            {
                obj.GetComponent<Stack>().stacked = true;
                obj.transform.parent = points[stackIndex];
                obj.transform.localPosition = Vector3.zero;

                obj.transform.DOLocalRotate(Vector3.zero, .1f);
            }

            stackIndex++;
        }
    }



    private void GetStack(GameObject obj)
    {
        obj.GetComponent<Stack>().stacked = true;
        obj.transform.parent = points[stackIndex];
        obj.transform.DOLocalJump(Vector3.zero, 2, 1, .5f);
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
        if (Input.GetKeyDown(KeyCode.X))
        {
            RemoveStackByNumber(5);
        }
    }
}