using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunShoot : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    [SerializeField] private float reloadTime;

    [SerializeField] private bool reloaded = true;

    [SerializeField] private Image shootImg;

    private GunInput gunInput;

    


    private void Start()
    {
        reloaded = true;

        gunInput = transform.GetComponent<GunInput>();
    }
    private void Update()
    {
        //TO DO: move the input somewhere else in order to make this class reusable for the enemy tanks
        if(gunInput.Shoot)
        {
            gunInput.Shoot = false;
            Shoot();
        }

        if(shootImg != null)
        {
            if(reloaded)
            {
                shootImg.color = Color.green;
            }
            else
            {
                shootImg.color = Color.red;
            }
        }
    }


    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        reloaded = true;
    }


    private GameObject Shoot()
    {
        if (reloaded)
        {
            reloaded = false;
            StartCoroutine(Reload());
            return Instantiate(projectile, transform.position, transform.rotation);
        }
        return null;
    }
}
