using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private int damage;

    [SerializeField] private float speed;

    private Rigidbody rb;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // BIG TO DO: take in consideration the caliber of the gun or the penetration power

        // Nice to have:
        // -sa iau in calcul si unghiul de impact, iar daca acesta este prea mare sa fie ricoseu

        
        //TO DO:
        // -animatie explozie proiectil(eventual ceva cu "scantei" ca in war thunder, de de "sparge" proeictilul)

        Destroy(transform.gameObject);

        if (other.CompareTag("FrontArmour"))
        {
            if(transform.CompareTag("PlayerProjectile"))
            {
                EnemyNonPenetration(other);

            }
            else if(transform.CompareTag("EnemyProjectile"))
            {
                PlayerNonPenetration(other);
            }

            
        
        }
        else if(other.CompareTag("SideArmour"))
        {
            if(CalculateAngle(transform.gameObject, other.transform.gameObject) < 45)
            {
                if(transform.CompareTag("PlayerProjectile"))
                {
                    EnemyNonPenetration(other);
                }
                else if(transform.CompareTag("EnemyProjectile"))
                {
                    PlayerNonPenetration(other);
                }
            }
            else
            {
                if (transform.CompareTag("PlayerProjectile"))
                {
                    EnemyPenetration(other);
                }
                else if (transform.CompareTag("EnemyProjectile"))
                {
                    PlayerPenetration(other);
                }
            }

        }
        else if(other.CompareTag("RearArmour"))
        {
            if (transform.CompareTag("PlayerProjectile"))
            {
                EnemyPenetration(other);
            }
            else if (transform.CompareTag("EnemyProjectile"))
            {
                PlayerPenetration(other);
            }

            //Destroy(transform.gameObject);
        }
        else
        {
            //TO DO:
            // -animatie explozie proiectil(aici sa fie ca si cum ar lovi o piatra, doar putin praf, nu e nevoie de "scantei")
            //Destroy(transform.gameObject);
        }



    }

    private void EnemyPenetration(Collider other)
    {
        GameObject enemyTank = other.transform.parent.parent.gameObject;
        
        if(enemyTank.CompareTag("Enemy"))
        {
            Debug.Log("Penetration");

            //Destroy(enemyTank);

            var eb = enemyTank.GetComponent<EnemyBehaviour>();
            if(eb != null)
            {
                eb.ReduceHitPoints(damage);
            }
        }
    }

    private void EnemyNonPenetration(Collider other)
    {
        GameObject enemyTank = other.transform.parent.parent.gameObject;
        if (enemyTank.CompareTag("Enemy"))
        {
            Debug.Log("Can't penetrate armour!");

            //TO DO:
            // - afisare de armour not penetrated ca in wot
        }
    }

    private void PlayerPenetration(Collider other)
    {
        GameObject enemyTank = other.transform.parent.parent.gameObject;
        if (enemyTank.CompareTag("Player"))
        {
            GameManagerForTanks.Instance.AddPenetration();
        }
    }

    private void PlayerNonPenetration(Collider other)
    {
        GameObject enemyTank = other.transform.parent.parent.gameObject;
        if (enemyTank.CompareTag("Player"))
        {
            GameManagerForTanks.Instance.AddBounce();
        }
    }



    private float CalculateAngle(GameObject obj1, GameObject obj2)
    {
        float angle = Mathf.Abs(Mathf.Abs(obj1.transform.rotation.eulerAngles.y) - Mathf.Abs(obj2.transform.rotation.eulerAngles.y));

        //Debug.Log("Raw angle: " + angle);

        if(angle > 180)
        {
            angle = 360 - angle;
        }
        if(angle > 90)
        {
            angle = 180 - angle;
        }

        //Debug.Log("Processed Angle: " + angle);

        return angle;
    }



}
