using UnityEngine;
using System.Collections;

public class StarParticleSystem : MonoBehaviour {

    public int minStarSystemDistance = 4;
    public int maxStarSystemDistance = 10;
    public int minStarParticleSize = 4;
    public int maxStarParticleSize = 128;
    public int maxStarCount = 1000;
    public int starRadius = 1000;

    public ParticleSystem StarSystems;

    // Use this for initialization
    void Start () {
        StarSystems = GetComponent<ParticleSystem>();
        generateStarSystemParticles();
    }
    
    // Update is called once per frame
    void Update () {
    
    }
    
    void generateStarSystemParticles()
    {
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        Debug.Log(string.Format("Starting star generation at: {0}", sw.ElapsedMilliseconds));

        for(var i = 0; i < maxStarCount; i++)
        {
            Color32 blueStar = new Color32(System.Convert.ToByte(Random.Range(0, 60)), System.Convert.ToByte(Random.Range(200, 255)), 255, 255);
            Color32 yellowStar = new Color32(255, 255, System.Convert.ToByte(Random.Range(0, 255)), 255);
            Color32 orangeStar = new Color32(255, System.Convert.ToByte(Random.Range(140,200)),0,255);
            
            int[] randomCoords = getRandomXY(starRadius);

            int randomCoordHash = (randomCoords[0] + randomCoords[1]).GetHashCode();
            
            while(GameManager.instance.UsedStarSystemCoords.Contains(randomCoordHash))
            {
                randomCoords = getRandomXY(starRadius);
                randomCoordHash = (randomCoords[0] + randomCoords[1]).GetHashCode();
                //Debug.Log("In while loop: " + randomXcoord + "," + randomYcoord);
            }
                GameManager.instance.UsedStarSystemCoords.Add(randomCoordHash);
                
            GameManager.instance.StarSystemParticles[i].position = new Vector3(randomCoords[0], randomCoords[1], 0.0f);
            
            int randomStarColor = Random.Range(1, 300);
            
            if(randomStarColor < 100)
                GameManager.instance.StarSystemParticles[i].startColor = blueStar;
            else if(randomStarColor > 100 && randomStarColor < 200)
                GameManager.instance.StarSystemParticles[i].startColor = orangeStar;
            else
                GameManager.instance.StarSystemParticles[i].startColor = yellowStar;
            
            GameManager.instance.StarSystemParticles[i].startSize = Random.Range(minStarParticleSize,maxStarParticleSize);
                
            
        }
        
        StarSystems.SetParticles(GameManager.instance.StarSystemParticles, GameManager.instance.StarSystemParticles.Length);
        
        sw.Stop();
        Debug.Log(string.Format("Finished star generation at: {0}", sw.ElapsedMilliseconds));
    }
    
    private int[] getRandomXY(int radius)
    {
        Vector2 randomCoord = Random.insideUnitCircle * radius;
        int randomXcoord = (int)randomCoord.x;
        int randomYcoord = (int)randomCoord.y;

        while(randomXcoord == 0 || randomYcoord == 0)
        {
            randomCoord = Random.insideUnitCircle * radius;
            randomXcoord = (int)randomCoord.x;
            randomYcoord = (int)randomCoord.y;
        }

        return new int[] { randomXcoord, randomYcoord };

    }

}
