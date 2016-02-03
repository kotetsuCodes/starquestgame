using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Linq;

public class GameManager : MonoBehaviour {

    //private Text fuelText;

    public static GameManager instance = null;
    public PlayerShip playerShip;

    //World Vars
    public float worldCoordModifier = 1.00f;
    public HashSet<int> UsedStarSystemCoords = new HashSet<int>();
    public ParticleSystem.Particle[] StarSystemParticles = new ParticleSystem.Particle[1000];
    public BaseStarSystem CurrentStarSystem;
    //private ParticleSystem ps;

    public int maxStarCount = 1000;
    public int starRadius = 1000;
    public float minStarSize = 0.05f;
    public float maxStarSize = 1.0f;
    public float minStarDistance = 1000.0f;

    //Planet Vars
    public float minPlanetSize = 0.1f;
    public float maxPlanetSize = 1.0f;
    public List<Sprite> RandomPlanetSprites = new List<Sprite>(9);

    public GameObject BaseStarPrefab;
    public List<GameObject> BaseStarArray = new List<GameObject>();
    public List<Material> RandomStarMaterials = new List<Material>(3);

    //UI
    public Text fuelText;
    public Text xCoordText;
    public Text yCoordText;

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

        //Generate Star Sprites
        for(var i = 0; i < maxStarCount; i++)
        {
            int materialIndex = UnityEngine.Random.Range(1, 300);
            
            if(materialIndex >= 1 && materialIndex < 100)
                materialIndex = 0;
            else if(materialIndex >= 100 && materialIndex < 200)
                materialIndex = 1;
            else
                materialIndex = 2;
            
            Vector2 randomPosition = UnityEngine.Random.insideUnitCircle * starRadius;

            //Debug.Log(Physics.OverlapSphere(randomPosition, minStarDistance).Length);

            var colliders = Physics2D.OverlapCircleAll(randomPosition, minStarDistance);

            while(colliders.Length > 0)
            {
                if(colliders.Length == 1)
                {
                    if(colliders[0].tag == "Player")
                    {
                        //Debug.Log("Player collider found...breaking");
                        break;
                    }
                }
                
                //Debug.Log("Star at " + randomPosition.x + "," + randomPosition.y + " is too close to another star");
                randomPosition = UnityEngine.Random.insideUnitCircle * starRadius;
                colliders = Physics2D.OverlapCircleAll(randomPosition, minStarDistance);
            }            
             
            float randomStarSize = UnityEngine.Random.Range(minStarSize, maxStarSize);
            
            //example: 100 stars max. Need 5% to be large stars
            if(BaseStarArray.Where(x => x.transform.localScale.x > 1.5).Count() < maxStarCount * 0.05 ) 
                randomStarSize = UnityEngine.Random.Range(1.5f, maxStarSize);
            else
                randomStarSize = UnityEngine.Random.Range(minStarSize, 1.5f);

            GameObject starSystem = (GameObject)Instantiate(BaseStarPrefab, randomPosition, Quaternion.Euler(0, 0, 0));

            BaseStarSystem starSystemProperties = starSystem.GetComponent<BaseStarSystem>();

            starSystemProperties.SizeOfStar = randomStarSize;
            starSystemProperties.Planets = new Planet[UnityEngine.Random.Range(0, 9)];
            
            for(var j = 0; j < starSystemProperties.Planets.Length; j++)
            {
                starSystemProperties.Planets[j] = new Planet(UnityEngine.Random.Range(minPlanetSize, maxPlanetSize), new Vector3(j, j, 0.0f), RandomPlanetSprites[UnityEngine.Random.Range(0, RandomPlanetSprites.Count)], UnityEngine.Random.Range(0, 8));
            }

            //Set sprite display properties
            var spriteRenderer = starSystem.GetComponent<SpriteRenderer>();
            spriteRenderer.transform.position = randomPosition;
            spriteRenderer.material = RandomStarMaterials[materialIndex];
            spriteRenderer.transform.localScale = new Vector3(randomStarSize, randomStarSize, 0.0f);

            BaseStarArray.Add(starSystem);

        }

        //playerShip = GameObject.Find("PlayerShip").GetComponent<PlayerShip>();

        //fuelText = GameObject.Find("FuelText").GetComponent<Text>();
        //xCoordText = GameObject.Find("XCoord").GetComponent<Text>();
        //yCoordText = GameObject.Find("YCoord").GetComponent<Text>();
        //ps = GameObject.Find("Star Particle System").GetComponent<ParticleSystem>();
        //fuelText.GetComponent<Text>().text = "Fuel: " + playerShip.CurrentFuel;
    }
    
    // Update is called once per frame
	void Update () 
    {
        //fuelText.GetComponent<Text>().text = "Fuel: " + playerShip.CurrentFuel.ToString("0.00");
        //xCoordText.GetComponent<Text>().text = PlayerShip.GetPlayerShipCoordsDisplayFormatted(playerShip).x.ToString();
        //yCoordText.GetComponent<Text>().text = PlayerShip.GetPlayerShipCoordsDisplayFormatted(playerShip).y.ToString();

        //Debug.Log(Vector3.Distance(new Vector3(0.0f,0.0f,0.0f), new Vector3(playerShip.transform.position.x, playerShip.transform.position.y, 0.0f)));

        //Debug.Log(PlayerShip.GetPlayerShipCoordsDisplayFormatted(playerShip).x.ToString() + "," + PlayerShip.GetPlayerShipCoordsDisplayFormatted(playerShip).y.ToString());
        //Debug.Log(playerShip.CurrentFuel);

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
                    //Debug.Log("Generating New Star");
                    newStars[i].position = new Vector3(sd.Stars.StarSystems[i].xPos, sd.Stars.StarSystems[i].yPos, sd.Stars.StarSystems[i].zPos);
                    newStars[i].startSize = sd.Stars.StarSystems[i].starSize;
                    newStars[i].startColor = new Color32(sd.Stars.StarSystems[i].starColor[0], sd.Stars.StarSystems[i].starColor[1], sd.Stars.StarSystems[i].starColor[2], sd.Stars.StarSystems[i].starColor[3] );
                }

                //ps.SetParticles(newStars, newStars.Length);

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