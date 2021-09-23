using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{ 

    private Vector3 targetPosition;

    private GameObject invisibleGun;

    private void Start()
    {
        invisibleGun = transform.parent.Find("InvisibleGun").gameObject;
    }

    private void Update()
    {
        targetPosition = TargetBehaviour.Instance.transform.position;


        //TO DO: make it so that the gun rotates only around the X(I think) axis, and find a method that doesn't use another "InvisibleThing"
        invisibleGun.transform.LookAt(targetPosition);

        Quaternion optimisedRotation = Quaternion.Euler(Mathf.Clamp(invisibleGun.transform.rotation.eulerAngles.x, 0f, 10f), transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

       
        transform.rotation = Quaternion.RotateTowards(transform.rotation, optimisedRotation, 100f * Time.deltaTime);


    }
}
