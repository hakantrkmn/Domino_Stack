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

    private void OnEnable()
    {
        EventManager.RemoveStackByNumber += RemoveStackByNumber;
        EventManager.LevelEnd += LevelEnd;
        EventManager.ReArrangeStack += ReArrangeStack;
        EventManager.GetStack += GetStack;
        EventManager.GetDominoStack += GetDominoStack;
        EventManager.RemoveStack += RemoveStack;
    }

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
            tempObj.transform.parent = null;
            templist.Add(tempObj);
            tempObj.GetComponent<Rigidbody>().isKinematic = false;
            tempObj.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-.1f, .1f), .2f, 5) * 10, ForceMode.VelocityChange);
            Vector3 torque;
            torque.x = Random.Range(-100, 100);
            torque.y = Random.Range(-100, 100);
            torque.z = Random.Range(-100, 100);
            //tempObj.GetComponent<Rigidbody>().angularVelocity = torque;
            tempObj.GetComponent<Stack>().stacked = false;
        }

        foreach (var Tobj in templist)
        {
            stackObjects.Remove(Tobj);
        }

        stackIndex = stackObjects.Count;
        //stackIndex--;
        //stackObjects.Remove(obj);
        //ReArrangeStack();
    }

    private void RemoveStackByNumber(int amount)
    {
        var tempList = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            var tempObj = stackObjects[stackIndex - 1 - i];
            tempObj.transform.parent = null;
            tempList.Add(tempObj);
            tempObj.GetComponent<Renderer>().enabled = false;
            tempObj.GetComponent<Rigidbody>().isKinematic = true;
            tempObj.GetComponent<Collider>().isTrigger = true;
            tempObj.transform.GetChild(0).gameObject.SetActive(true);

            tempObj.GetComponent<RayfireBomb>().Explode(0);

            //Destroy(tempObj, 2);
            /*tempObj.GetComponent<RayfireRigid>().Demolish();
            tempObj.GetComponent<RayfireBomb>().Explode(0);*/
            /*tempObj.GetComponent<Collider>().enabled = false;
            tempObj.GetComponent<Rigidbody>().isKinematic = true;
            tempObj.transform.DOScale(0, 1);*/
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
            //obj.transform.DOScale(obj.transform.localScale*2, .5f);
            stackObjects.Remove(obj);
            ReArrangeStack();
        }
        else
        {
            if (!dominosObjects.Contains(obj))
            {
                Debug.Log("sa");

                obj.transform.parent = stackController.createdPoints[dominoStackIndex];
                obj.transform.DOLocalJump(Vector3.zero, 3, 3, 1f);
                obj.transform.DOLocalRotate(Vector3.zero, .5f);
                //obj.transform.DOScale(obj.transform.localScale*2, .5f);
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
        EventManager.LevelEnd -= LevelEnd;
        EventManager.RemoveStackByNumber -= RemoveStackByNumber;
        EventManager.ReArrangeStack -= ReArrangeStack;

        EventManager.GetStack -= GetStack;
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