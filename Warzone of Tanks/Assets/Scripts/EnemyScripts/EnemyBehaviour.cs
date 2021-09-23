using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public bool Taunted { get; private set; }

    public bool ReadyToShoot { get; private set; }
    public GameObject Target { get => target; private set => target = value; }


    [SerializeField] private int hitPoints;

    [SerializeField] private GameObject target;
   
    [Range(0f, 50f)] [SerializeField] private float rotationTurretSpeed;

    [SerializeField] private float minDistanceToTarget;
    [SerializeField] private float minDistanceToShoot;

    [SerializeField] private float distance; // only for inspecting because on debug mode there are too many things out there

    private GameObject destroyedMesh;
    private GameObject hull;
    private GameObject turret;
    private GameObject gun;
    private GameObject invisibleTurret;

    private void Start()
    {

        invisibleTurret = transform.Find("InvisibleTurret").gameObject;
        turret = transform.Find("Turret").gameObject;
        gun = turret.transform.Find("GunAssembly").gameObject;
        hull = transform.Find("Hull").gameObject;
        destroyedMesh = transform.Find("Destroyed").gameObject;

    }


    private void Update()
    {
        if(hitPoints <= 0)
        {
            //Change the mesh with a destroyed tank
            //Destroy(gameObject);

            destroyedMesh.SetActive(true);
            hull.SetActive(false);
            turret.SetActive(false);

            var move = transform.GetComponent<EnemyMovement>();
            Destroy(move);
            var beh = transform.GetComponent<EnemyBehaviour>();
            //Destroy(beh);

            StartCoroutine(DestroyTank());
        }

        // BIG TO DO: move all the movement of turret in other script!!!
        // breaking the S from solid

        distance = CalculateDistance();

        if(distance <= minDistanceToTarget)
        {
            //Debug.Log("In raza de actiune");
            invisibleTurret.transform.LookAt(target.transform.position);
            Taunted = true;
        }
        else
        {
            //Debug.Log("Prea departe");
            invisibleTurret.transform.rotation = transform.rotation;
            Taunted = false;
        }

        // TO DO: change Lerp with RotateTowards if possible
        // also if possible, make this change for the hull rotation too

        Quaternion optimisedTurretRotation = Quaternion.Euler(turret.transform.rotation.eulerAngles.x, invisibleTurret.transform.rotation.eulerAngles.y, turret.transform.rotation.eulerAngles.z);
        Quaternion optimisedGunRotation = Quaternion.Euler(invisibleTurret.transform.rotation.eulerAngles.x, gun.transform.rotation.eulerAngles.y, gun.transform.rotation.eulerAngles.z);

        gun.transform.rotation = Quaternion.RotateTowards(gun.transform.rotation, optimisedGunRotation, 100f * Time.deltaTime);

        turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, optimisedTurretRotation, rotationTurretSpeed * Time.deltaTime);


        if(Taunted && distance < minDistanceToShoot && Mathf.Abs(turret.transform.rotation.eulerAngles.y - optimisedTurretRotation.eulerAngles.y) < 10f)
        {
            ReadyToShoot = true;
        }
        else
        {
            ReadyToShoot = false;
        }

    }

    private float CalculateDistance() => Vector3.Distance(transform.position, target.transform.position);

    public void ReduceHitPoints(int value) => hitPoints -= value;


    private IEnumerator DestroyTank()
    {
        yield return new WaitForSeconds(5f);

        Destroy(gameObject);
    }

}
