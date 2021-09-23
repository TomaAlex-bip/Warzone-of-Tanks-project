using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInput : MonoBehaviour
{
    public bool Shoot { get; set; }

    private bool isPlayer;

    private EnemyBehaviour enemyBehaviour;

    private void Start()
    {
        if(transform.CompareTag("Enemy"))
        {
            isPlayer = false;
            // TO DO: surely there is an easier and more elegant methot to get the enemyBehaviour
            enemyBehaviour = transform.parent.parent.parent.GetComponent<EnemyBehaviour>();
        }
        else if(transform.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }


    private void Update()
    {
        if (isPlayer)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Shoot = true;
            }
        }
        else
        {
            if(enemyBehaviour!= null && enemyBehaviour.ReadyToShoot)
            {
                Shoot = true;
            }
        }

    }


}
