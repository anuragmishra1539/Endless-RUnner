﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


  public enum peicetype
{ 
    none=-1,
    ramp=0,
    longblock =1,
    jump=2,
    slide=3,



}
public class peices : MonoBehaviour
{
    public peicetype type;
    public int visualIndex;
}