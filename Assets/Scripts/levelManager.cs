using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UIElements;

public class levelManager : MonoBehaviour
{
    public static levelManager Instance { set; get; }

    private const bool Show_collider= true;
    private const float DISTANCE_BETWEEN_SPAWN=100.0f;
    private const int INITAL_SEGENT = 10;
    private const int Max_segment_on_screen = 15;
    private Transform cameracontainer;
    private int amountofActiveSegments;
    private int continousSegments;
    private int currentSpawnZ;
    private int Currentlevel;
    private int y1, y2, y3;



    public List<segment> availablesegments = new List<segment>();
    public List<segment> availableTransition= new List<segment>();
    public List<segment> segments = new List<segment>();

    private bool isMoving = false;


    private void Awake()
    {
        Instance = this;
        cameracontainer = Camera.main.transform;
        currentSpawnZ = 0;
        Currentlevel = 0;


    }
    private void Start()
    {
        for (int i = 0; i < INITAL_SEGENT; i++)
        {
            Generatesegement();
        }  
    }
    private void Update()
    {
        if(currentSpawnZ-cameracontainer.position.z<DISTANCE_BETWEEN_SPAWN)
        
            Generatesegement();
        
        if(amountofActiveSegments>=Max_segment_on_screen)
        {
            segments[amountofActiveSegments - 1].despawn();
            amountofActiveSegments--;

        }
    }
    private void Generatesegement()
    {
        Spawnsegement();
        if (Random.Range(0, 1) < (continousSegments * 0.25f))
        {
            continousSegments = 0;
            spawntransition();
        }
        else
        {
            continousSegments++;
        }
    }
    private void Spawnsegement()
    {
        List<segment> possibleseg = availablesegments.FindAll(x => x.beganY1 == y1 || x.beganY2 == y2 || x.beganY3 == y3);
        int id = Random.Range(0, possibleseg.Count);
        segment r = GetSegment(id, false);
        y1 = r.endY1;
        y2 = r.endY2;
        y3 = r.endY3;

        r.transform.SetParent(transform);
        r.transform.localPosition = Vector3.forward * currentSpawnZ;
        currentSpawnZ += r.length;
        amountofActiveSegments++;
        r.spawn();
    }
    private void spawntransition()
    {
        List<segment> possibletransition = availableTransition.FindAll(x => x.beganY1 == y1 || x.beganY2 == y2 || x.beganY3 == y3);
        int id = Random.Range(0, possibletransition.Count);
        segment r = GetSegment(id, true);
        y1 = r.endY1;
        y2 = r.endY2;
        y3 = r.endY3;

        r.transform.SetParent(transform);
        r.transform.localPosition = Vector3.forward * currentSpawnZ;
        currentSpawnZ += r.length;
        amountofActiveSegments++;
        r.spawn();
    }

    public segment GetSegment(int id,bool transition)
    {
        segment r= null;
        r = segments.Find(x => x.segid == id && x.transition == transition && !x.gameObject.activeSelf);
        if (r == null)
        {
            GameObject go = Instantiate((transition) ? availableTransition[id].gameObject :availablesegments[id].gameObject) as GameObject;

            r = go.GetComponent<segment>();
            r.segid = id;
            r.transition = transition;
            segments.Insert(0,r);
        }
        else
        {
            segments.Remove(r);
            segments.Insert(0, r);
        }
        return r;
    }
   
}

