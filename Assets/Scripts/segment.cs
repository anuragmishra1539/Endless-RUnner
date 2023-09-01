using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class segment : MonoBehaviour
{
    public int segid { set; get; }
    public bool transition;
    public int length;
    public int beganY1, beganY2, beganY3;
    public int endY1, endY2, endY3;
 

    public void spawn()
    {
        gameObject.SetActive(true);
      
        
       
    }
    public void despawn()
    {
        gameObject.SetActive(false);
        

    }


}
