using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_camRotator : MonoBehaviour
{
    [SerializeField] float velocRot = 0.1f;
    Vector3 rot = Vector3.zero;
    private void Start()
    {
        rot = new Vector3(0,velocRot,0);
    }
    private void Update()
    {
        transform.Rotate(rot);
    }
}
