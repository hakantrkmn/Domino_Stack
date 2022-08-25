using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
  
    public static Func<List<Transform>> GetPoints;
    public static Func<Transform> GetLevelEndPlayerPos;
    public static Action<GameObject> GetStack;
    public static Action<GameObject> RemoveStack;
    public static Action<int> RemoveStackByNumber;
    public static Action<GameObject> GetDominoStack;
    public static Action LevelEnd;
    public static Action ReArrangeStack;
    public static Action<Transform> MoveCube;
    
}