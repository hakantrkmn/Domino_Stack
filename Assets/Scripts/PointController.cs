using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{

    public List<Transform> points;
    public float gapBetweenPoints;
    public Transform pointsHolder;
    public Transform target;
    public float followSpeed;
    private void OnValidate()
    {
        target = GameObject.FindObjectOfType<PlayerController>().transform;
        points.Clear();

        for (int i = 0; i < pointsHolder.childCount ; i++)
        {
            points.Add(pointsHolder.GetChild(i));
        }

        for (int i = 0; i < points.Count; i++)
        {
            if (!points[i].gameObject.GetComponent<Point>())
            {
                points[i].gameObject.AddComponent<Point>();

            }
            points[i].localPosition = new Vector3(0, .7f, 1+(i * gapBetweenPoints));
        }
        
    }
    private void OnEnable()
    {
        EventManager.GetPoints += GetPoints;
    }

    private void OnDisable()
    {
        EventManager.GetPoints -= GetPoints;

    }

    private List<Transform> GetPoints()
    {
        return points;
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
            transform.position = new Vector3(transform.position.x, transform.position.y, target.transform.position.z);
        }
    }
}
