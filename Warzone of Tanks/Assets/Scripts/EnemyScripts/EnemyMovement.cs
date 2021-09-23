using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private GameObject pathParent;

    [SerializeField] private bool circularPath;

    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;

    [SerializeField] private float minTimeBeforeMove;
    [SerializeField] private float maxTimeBeforeMove;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;

    private List<Transform> path = new List<Transform>();

    private GameObject invisibleHull; // empty gameObject used to interpolate the rotation of the tank

    private float speed; // this is not the official speed, this is used to make the lerp move linear

    private float waitBeforeMoving;
    private bool hasArrived = true;
    private bool hasRotated = false;
    private bool readyToMove = false;

    private int currentPathIndex = 0;
    private bool listIsPositive = true;

    private Rigidbody rb;

    private Vector2 nextPos;

    private EnemyBehaviour enemyBehaviour;

    private void Start()
    {
        enemyBehaviour = transform.GetComponent<EnemyBehaviour>();


        rb = gameObject.GetComponent<Rigidbody>();

        invisibleHull = transform.Find("InvisibleHull").gameObject;
        if(pathParent != null)
        {
            foreach(Transform pathTransform in pathParent.transform)
            {
                path.Add(pathTransform);
            }
        }
    }

    private void FixedUpdate()
    {
        if(enemyBehaviour.Taunted)
        {
            if(Vector3.Distance(transform.position, enemyBehaviour.Target.transform.position) > 4f)
            {
                rb.velocity = transform.forward * movementSpeed * Time.deltaTime;
            }
        }
        else
        {
            rb.velocity = transform.forward * movementSpeed * Time.deltaTime;

        }
        
    }


    private void Update()
    {
        if(enemyBehaviour.Taunted)
        {
            nextPos = new Vector2(enemyBehaviour.Target.transform.position.x, enemyBehaviour.Target.transform.position.z);
        }
        else
        {
            if(hasArrived)
            {
                hasArrived = false;
                nextPos = CalculateNextPosition();
            }
            if(Vector3.Distance(transform.position, new Vector3(nextPos.x, transform.position.y, nextPos.y)) < 0.1f)
            {
                hasArrived = true;
            }
        }
        

        invisibleHull.transform.LookAt(new Vector3(nextPos.x, transform.position.y, nextPos.y));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, invisibleHull.transform.rotation, rotationSpeed * Time.deltaTime);



        /*
        if (!hasRotated)
        {
            hasRotated = true;

            nextPos = CalculateNextPosition();
            Debug.Log("apare cand se schimba rotatia");
            invisibleHull.transform.LookAt(new Vector3(nextPos.x, transform.position.y, nextPos.y));

            StartCoroutine(RotateTowards(invisibleHull.transform.rotation.eulerAngles.y));
        }

        if (readyToMove)
        {
            readyToMove = false;
            
            if (hasArrived)
            {
                hasArrived = false;

                StartCoroutine(MoveToPoint(new Vector3(nextPos.x, transform.position.y, nextPos.y)));
            }
        }
        */
    }

    private IEnumerator MoveToPoint(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            speed = 1 / Vector3.Distance(transform.position, target);
            
            transform.position = Vector3.Lerp(transform.position, target, speed  * movementSpeed * Time.deltaTime);

            yield return null;
        }

        waitBeforeMoving = Random.Range(minTimeBeforeMove, maxTimeBeforeMove);
        yield return new WaitForSeconds(waitBeforeMoving);
        hasArrived = true;
        hasRotated = false;
    }

    private IEnumerator RotateTowards(float yRotation)
    {   
        
        while(Mathf.Abs(yRotation - transform.rotation.eulerAngles.y) > 0.1f)
        {
            //Debug.Log("asta trebuie sa apara mereu");

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, yRotation, transform.rotation.eulerAngles.z), rotationSpeed * Time.deltaTime);
            
            yield return null;

        }
        //yield return new WaitForSeconds(2f);
        readyToMove = true;
    }


    private Vector2 CalculateNextPosition()
    {
        Vector2 nextPos = new Vector2(0f, 0f);

        if(path.Count <= 0)
        {
            nextPos.x = Random.Range(minPosition.x, maxPosition.x);
            nextPos.y = Random.Range(minPosition.y, maxPosition.y);

            //Debug.Log("Path is null");
        }
        else
        {
            if(circularPath)
            {
                //Debug.Log("Path is circular");
                nextPos.x = path[currentPathIndex].position.x;
                nextPos.y = path[currentPathIndex].position.z;
                currentPathIndex++;

                if(currentPathIndex >= path.Count)
                {
                    currentPathIndex = 0;
                }
            }
            else
            {
                //Debug.Log("Path is path");
                if (currentPathIndex == 0)
                {
                    listIsPositive = true;
                }
                if(currentPathIndex == path.Count-1)
                {
                    listIsPositive = false;
                }

                nextPos.x = path[currentPathIndex].position.x;
                nextPos.y = path[currentPathIndex].position.z;
                
                if(listIsPositive)
                {
                    //Debug.Log("Moving forward");
                    currentPathIndex++;
                }
                else
                {
                    //Debug.Log("Moving backward");
                    currentPathIndex--;
                }


            }
        }
        return nextPos;
    }

}
