using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChangeHealthEventArgs : EventArgs 
{
    public bool spawnNumberTextMesh = true;
    public float healthDifference = 0f; 
}
