using UnityEngine;
using System.Collections;

public class StarParticleSystem : MonoBehaviour {

    public int minStarSystemDistance = 4;
    public int maxStarSystemDistance = 10;
    public int minStarParticleSize = 4;
    public int maxStarParticleSize = 128;
    public int maxStarCount = 1000;
    public int starRadius = 1000;

    private ParticleSystem ps;

    // Use this for initialization
    void Start () {
        ps = GetComponent<ParticleSystem>();
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
            
            while(GameManager.instance.usedStarSystemCoords.Contains(randomCoordHash))
            {
                randomCoords = getRandomXY(starRadius);
                randomCoordHash = (randomCoords[0] + randomCoords[1]).GetHashCode();
                //Debug.Log("In while loop: " + randomXcoord + "," + randomYcoord);
            }
                GameManager.instance.usedStarSystemCoords.Add(randomCoordHash);
                
            GameManager.instance.starSystemParticles[i].position = new Vector3(randomCoords[0], randomCoords[1], 0.0f);
            
            int randomStarColor = Random.Range(0, 2);
            
            switch(randomStarColor){
                case 0: GameManager.instance.starSystemParticles[i].startColor = blueStar;
                    break;
                case 1: GameManager.instance.starSystemParticles[i].startColor = yellowStar;
                    break;
                case 2: GameManager.instance.starSystemParticles[i].startColor = orangeStar;
                    break;
            }
            
            
            GameManager.instance.starSystemParticles[i].startSize = Random.Range(minStarParticleSize,maxStarParticleSize);
                
            
        }
        
        ps.SetParticles(GameManager.instance.starSystemParticles, GameManager.instance.starSystemParticles.Length);
        
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
