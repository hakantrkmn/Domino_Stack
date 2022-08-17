using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
  
    public static Func<List<Transform>> GetPoints;
    public static Action<GameObject> GetStack;
    public static Action<GameObject> RemoveStack;
    public static Action<GameObject> GetDominoStack;
    
}