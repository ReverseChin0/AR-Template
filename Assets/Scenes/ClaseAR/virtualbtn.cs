using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class virtualbtn : MonoBehaviour, IVirtualButtonEventHandler
{

    VirtualButtonBehaviour virtbutt;

    public GameObject obj;

    private void Start()
    {
        virtbutt = GetComponent<VirtualButtonBehaviour>();
        virtbutt.RegisterEventHandler(this);
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        Destroy(obj);
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
       
    }

    
}
