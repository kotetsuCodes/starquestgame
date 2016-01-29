using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameManager : MonoBehaviour {

    //private Text fuelText;

    public static GameManager instance = null;
    public PlayerShip playerShip;

    //World Vars
    public float worldCoordModifier = 1.00f;
    public HashSet<int> UsedStarSystemCoords = new HashSet<int>();
    public ParticleSystem.Particle[] StarSystemParticles = new ParticleSystem.Particle[1000];
    private ParticleSystem ps;

    //UI
    Text fuelText;
    Text xCoordText;
    Text yCoordText;

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

        playerShip = GameObject.Find("Player_Ship").GetComponent<PlayerShip>();
        

        fuelText = GameObject.Find("FuelText").GetComponent<Text>();
        xCoordText = GameObject.Find("XCoord").GetComponent<Text>();
        yCoordText = GameObject.Find("YCoord").GetComponent<Text>();
        ps = GameObject.Find("Star Particle System").GetComponent<ParticleSystem>();
        fuelText.text = "Fuel: " + playerShip.CurrentFuel;
    }
	
    
    
    // Update is called once per frame
	void Update () 
    {
        fuelText.text = "Fuel: " + playerShip.CurrentFuel.ToString("0.00");
        xCoordText.text = PlayerShip.GetPlayerShipCoordsDisplayFormatted(playerShip).x.ToString();
        yCoordText.text = PlayerShip.GetPlayerShipCoordsDisplayFormatted(playerShip).y.ToString();

        //Debug.Log("Ship Display Position: " + PlayerShip.GetPlayerShipCoordsDisplayFormatted(playerShip).x.ToString() + "," + PlayerShip.GetPlayerShipCoordsDisplayFormatted(playerShip).y.ToString());
        //Debug.Log("Ship Actual Position: " + playerShip.transform.position.x + "," + playerShip.transform.position.y);
        //Debug.Log("Player Fuel: " + playerShip.CurrentFuel);
    }
    
    public void Save()
    {
        var starSystems = new SingleStarSystem[1000];
        var starSystemData = new StarSystemData(starSystems);
    
        for (int i = 0; i < StarSystemParticles.Length; i++)
        {
            
            var star = StarSystemParticles[i];
            var starSystem = new SingleStarSystem(star.position.x, star.position.y, star.position.z, star.startSize, new byte[] {star.startColor.r, star.startColor.g, star.startColor.b, star.startColor.a });
            starSystemData.StarSystems[i] = starSystem;
            
        }
        
        Debug.Log(Application.persistentDataPath);

        BinaryFormatter binaryFormatter = new BinaryFormatter();
        using(FileStream fileStream = File.Create(Application.persistentDataPath + "/saveData.dat"))
        {
            saveData sd = new saveData(playerShip.transform.position.x, playerShip.transform.position.y, playerShip.CurrentFuel, starSystemData, UsedStarSystemCoords);
            binaryFormatter.Serialize(fileStream, sd);    
        }

    }
    
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/saveData.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Open))
            {
                saveData sd = (saveData)binaryFormatter.Deserialize(fileStream);

                //Set game variables back to what is in save file
                playerShip.transform.position = new Vector3(sd.PlayerShipLocationDisplayX, sd.PlayerShipLocationDisplayY, -8.0f);
                playerShip.CurrentFuel = sd.PlayerFuelPoints;
                //ps.SetParticles(new ParticleSystem.Particle[1000], 1000);

                var newStars = new ParticleSystem.Particle[1000];

                for(int i = 0; i < newStars.Length; i++)
                {
                    Debug.Log("Generating New Star");
                    newStars[i].position = new Vector3(sd.Stars.StarSystems[i].xPos, sd.Stars.StarSystems[i].yPos, sd.Stars.StarSystems[i].zPos);
                    newStars[i].startSize = sd.Stars.StarSystems[i].starSize;
                    newStars[i].startColor = new Color32(sd.Stars.StarSystems[i].starColor[0], sd.Stars.StarSystems[i].starColor[1], sd.Stars.StarSystems[i].starColor[2], sd.Stars.StarSystems[i].starColor[3] );
                }

                ps.SetParticles(newStars, newStars.Length);

                //StarSystemParticles = sd.StarSystemParticles;
                //UsedStarSystemCoords = sd.UsedStarSystemCoords;

            }
        }
    }
}

[Serializable]
public class saveData
{
    public saveData(float playerShipLocationDisplayX, float playerShipLocationDisplayY, float playerFuelPoints, StarSystemData stars, HashSet<int> usedStarSystemCoords)
    {
        this.PlayerShipLocationDisplayX = playerShipLocationDisplayX;
        this.PlayerShipLocationDisplayY = playerShipLocationDisplayY;
        this.PlayerFuelPoints = playerFuelPoints;
        this.Stars = stars;
        //this.StarSystemParticles = starSystemParticles;
        //this.UsedStarSystemCoords = usedStarSystemCoords;
    }

    public float PlayerShipLocationDisplayX { get; private set; }
    public float PlayerShipLocationDisplayY { get; private set; }
    public float PlayerFuelPoints { get; private set; }
    public StarSystemData Stars { get; private set; }
    //public ParticleSystem.Particle[] StarSystemParticles { get; private set; }
    //public HashSet<int> UsedStarSystemCoords { get; private set; }




}

[Serializable]
public class StarSystemData
{
    public StarSystemData(SingleStarSystem[] starSystems)
    {
        this.StarSystems = starSystems;
    }

    public SingleStarSystem[] StarSystems { get; private set;}
}

[Serializable]
public class SingleStarSystem
{
    public SingleStarSystem(float xPos, float yPos, float zPos, float starSize, byte[] starColor)
    {
        this.xPos = xPos;
        this.yPos = yPos;
        this.zPos = zPos;
        this.starSize = starSize;
        this.starColor = starColor;
    }

    public float xPos { get; private set; }
    public float yPos { get; private set; }
    public float zPos { get; private set; }
    public float starSize { get; private set; }
    public byte[] starColor { get; private set; }

}