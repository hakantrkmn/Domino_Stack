using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MathDoor : MonoBehaviour
{
    public TextMeshProUGUI mathText;

    public int value;

    private void OnValidate()
    {
        mathText.text = value.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
