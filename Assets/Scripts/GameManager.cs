using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    //private Text fuelText;

    public static GameManager instance = null;

    public float playerFuelPoints = 1000.00f;

    //World Vars
    public float worldCoordModifier = 1.00f;
    public HashSet<int> usedStarSystemCoords = new HashSet<int>();
    public ParticleSystem.Particle[] starSystemParticles = new ParticleSystem.Particle[1000];

    //Player Ship Vars
    public float fuelUsage = 0.75f;

    // Use this for initialization
    void Awake () {
        
	   if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        InitGame();

    }
	
    void InitGame()
    {
        //foreach(var starSystem in starSystemCoords)
        //Debug.Log(starSystem.x + "," + starSystem.y);

        //Debug.Log("Last Coord: " + starSystemCoords[999].x + "," + starSystemCoords[999].y);

        //doingSetup = true;
        //fuelText = GameObject.Find("FuelText").GetComponent<Text>();
        //fuelText.text = "Fuel: 1000";
    }
	
    
    
    // Update is called once per frame
	void Update () {
	
	}
}
