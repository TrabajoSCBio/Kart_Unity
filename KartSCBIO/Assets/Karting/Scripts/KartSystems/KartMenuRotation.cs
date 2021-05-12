using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartMenuRotation : MonoBehaviour
{
    [SerializeField] private float speedRotation;
    [SerializeField] private float offsetX;
    [SerializeField] private Camera cam;
    private float initX;
    private void OnMouseDrag() 
    {
        
        if(Input.mousePosition.x > initX + offsetX)
            transform.Rotate(Vector3.up * speedRotation * -1);
        else if(Input.mousePosition.x < initX - offsetX) 
        {
            transform.Rotate(Vector3.up * speedRotation);
        }
    }
    private void OnMouseDown() 
    {
        initX = cam.WorldToScreenPoint(transform.position).x;
    }
}
