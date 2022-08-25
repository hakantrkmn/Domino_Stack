using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public PathCreator pathCreator;

    public GameObject prefab;
    public GameObject holder;

    public float spacing;

    public List<Transform> createdPoints;





    // Start is called before the first frame update
    void Start()
    {
        //Generate();

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Generate()
    {
        DestroyPoints();
        if (pathCreator != null && prefab != null && holder != null) {

            VertexPath path = pathCreator.path;

            float dst = 0;

            while (dst < path.length) {
                Vector3 point = path.GetPointAtDistance (dst);
                Quaternion rot = path.GetRotationAtDistance (dst);
                var temp = Instantiate (prefab, point, rot, holder.transform);
                temp.transform.localPosition += new Vector3(0, 1, 0);
                createdPoints.Add(temp.transform);
                dst += spacing;
            }
        }
    }

    public void DestroyPoints()
    {
        foreach (var point in createdPoints)
        {
            Destroy(point.gameObject);
        }
        createdPoints.Clear();
    }
}
