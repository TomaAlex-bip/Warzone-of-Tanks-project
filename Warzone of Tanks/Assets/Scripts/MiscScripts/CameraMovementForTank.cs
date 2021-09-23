using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementForTank : MonoBehaviour
{
    [SerializeField] private GameObject objToFollow;

    [SerializeField] private GameObject cameraPivot;

    [SerializeField] private float followSpeed;

    private Vector3 offset;


    private void Start()
    {
        offset = transform.position - objToFollow.transform.position;
        //offset = cameraPivot.transform.position - objToFollow.transform.position;
    }


    private void FixedUpdate()
    {

        transform.position = Vector3.Lerp(transform.position, objToFollow.transform.position + offset, followSpeed * Time.deltaTime);

        //transform.position = Vector3.Lerp(transform.position, cameraPivot.transform.position, followSpeed * Time.deltaTime);

    }
}
