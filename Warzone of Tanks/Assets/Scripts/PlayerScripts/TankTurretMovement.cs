using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTurretMovement : MonoBehaviour
{
    [SerializeField] private float turretRotationSpeed;

    private GameObject invisibleTurret;

    private Vector3 targetPosition;


    private void Start()
    {

        invisibleTurret = transform.parent.Find("InvisibleTurret").gameObject;
    }

    private void Update()
    {
        targetPosition = TargetBehaviour.Instance.transform.position;

        invisibleTurret.transform.LookAt(targetPosition);

        Quaternion optimisedRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 
                                                        invisibleTurret.transform.rotation.eulerAngles.y, 
                                                        transform.rotation.eulerAngles.z);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, optimisedRotation, Time.deltaTime * turretRotationSpeed);
       
    }



}
