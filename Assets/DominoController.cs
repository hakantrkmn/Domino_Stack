using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DominoController : MonoBehaviour
{
    public CinemachineVirtualCamera dominoCam;

    public List<PathController> paths;
    public List<Transform> createdPoints;
    public Transform playerPos;

    private void OnEnable()
    {
        EventManager.LevelEnd += LevelEnd;
        EventManager.GetLevelEndPlayerPos += GetLevelEndPlayerPos;

    }

    private Transform GetLevelEndPlayerPos()
    {
        return playerPos;

    }


    private void OnDisable()
    {
        EventManager.GetLevelEndPlayerPos -= GetLevelEndPlayerPos;

        EventManager.LevelEnd -= LevelEnd;
    }

    private void LevelEnd()
    {
        dominoCam.Priority = 11;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var path in paths)
        {
            path.Generate();
            foreach (var point in path.createdPoints)
            {
                createdPoints.Add( point);

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
