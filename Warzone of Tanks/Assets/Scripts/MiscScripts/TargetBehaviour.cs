using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    public static TargetBehaviour Instance { get; private set; }

    [SerializeField] private LayerMask layerMask;

    private GameObject enemy;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out RaycastHit raycastHit, 50, layerMask) && Input.GetMouseButton(0))
        {
            if(raycastHit.transform.CompareTag("Ground"))
            {
                transform.position = raycastHit.point + new Vector3(0f, 0.0f, 0f);
                enemy = null;
            }
            else if(raycastHit.transform.CompareTag("Enemy"))
            {
                enemy = raycastHit.transform.gameObject;
            }
        }

        if(enemy)
        {
            transform.position = enemy.transform.position;
        }

    }
}
