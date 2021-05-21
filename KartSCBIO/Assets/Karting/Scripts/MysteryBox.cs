using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    public float speedRotation;
    public float speedTransition;
    public float offsetY;
    public GameObject VFX;
    public AudioClip pickBox;
    float direction;
    Vector3 init;
    private void Start() {
        init = transform.position;
        direction = 1f;
    }

    private void FixedUpdate() 
    {
        if(transform.position.y >= init.y + offsetY - 0.1f)
        {
            direction = -1f;
        }
        if(transform.position.y <= init.y - offsetY + 0.1f)
        {
            direction = 1f;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * new Quaternion(0,1,0,1), Time.fixedDeltaTime * speedRotation);
        transform.position = Vector3.Lerp(transform.position, init + (Vector3.up * offsetY * direction), Time.fixedDeltaTime * speedTransition);
    }
    private void OnDestroy() 
    {
        if(GetComponentInParent<SpawnObjects>()) 
        {
            GameObject VFX = Instantiate(this.VFX,transform.position,transform.rotation);
            VFX.GetComponent<ParticleSystem>().Play();
            AudioUtility.CreateSFX(pickBox, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
            Destroy(VFX, 2f);
            GetComponentInParent<SpawnObjects>().canInstantiate = true;
        }
    }
}
