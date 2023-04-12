using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBehaviour : MonoBehaviour
{


    public float tracerFireRate = 3f;
    private float tracerNextFire = 0;



    // Start is called before the first frame update




    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {

        FireTracerEgg();
        
    }
    





    public void FireTracerEgg()
    {
        if (Time.time > tracerNextFire)
        {
            tracerNextFire = Time.time + tracerFireRate;
            GameObject b = Instantiate(Resources.Load("Prefabs/Tracer") as GameObject);
            b.transform.localPosition = transform.localPosition;
            b.transform.rotation = transform.rotation;
            
        }
        
    }





}
