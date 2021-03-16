using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class mitrackBehaviour : MonoBehaviour
{
    bool obj1State = false;
    bool obj2State = false;
    Transform tr1, tr2;
    LineRenderer l1, l2;
    
    public TextMeshProUGUI miText;

    string distext = "0";
    
    void Update()
    {
        if(obj1State && obj2State)
        {
            /*l1.SetPosition(0,tr1.position);
            l1.SetPosition(1,tr2.position);

            l2.SetPosition(0, tr2.position );
            l2.SetPosition(1, tr1.position);*/

            Vector3 dist = tr2.position - tr1.position;
            //miText.transform.position = dist * 0.5f;

            distext = dist.magnitude.ToString();
            miText.text ="distancia: " + distext;
        }
        else
        {
            distext = "No estan en pantalla";
            miText.text = "distancia: " + distext;
        }
    }

    protected void OnGUI()
    {
        /*GUI.skin.label.fontSize = Screen.width / 20;
        if(distext!="0")
            GUILayout.Label("Distancia: " + distext);*/

    }

    internal void Notify(Transform tr, LineRenderer miline)
    {
        if (miline != null)
        {
            obj1State = true;
            tr1 = tr;
            //l1 = miline;
        }
        else
        {
            obj2State = true;
            tr2 = tr;
            //l2 = miline;
        }
            
    }

    internal void NotifyLost(LineRenderer mitext)
    {
        if (mitext != null)
        {
            obj1State = false;
        }
        else
        {
            obj2State = false;
        }
    }
}
