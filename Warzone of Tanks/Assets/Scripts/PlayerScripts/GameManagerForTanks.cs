using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManagerForTanks : MonoBehaviour
{
    
    public static GameManagerForTanks Instance { get; private set; }

    [SerializeField] private Text penetrationsText;
    [SerializeField] private Text bouncesText;

    private int penetrations;
    private int bounces;



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

        penetrations = 0;
        bounces = 0;

    }

    private void Update()
    {
        penetrationsText.text = "Penetrations: " + penetrations;
        bouncesText.text = "Bounces: " + bounces;
    }

    public void AddPenetration()
    {
        penetrations++;
    }

    public void AddBounce()
    {
        bounces++;
    }

}
