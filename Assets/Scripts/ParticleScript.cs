using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {

    public int starsMax = 100;
    public float minStarSize = 0.1f;
    public float maxStarSize = 1.0f;
    public float starRadius = 10.0f;

    private float starDistanceSqr;
    private Transform tx;
    private ParticleSystem.Particle[] points;
    private ParticleSystem ps;

    // Use this for initialization
    void Start () {
        tx = transform;
        ps = GetComponent<ParticleSystem>();
    }
	
    void CreateStars()
    {
        points = new ParticleSystem.Particle[starsMax];
        
        for(int i = 0; i < starsMax; i++){
            Vector2 randomVector2 = Random.insideUnitCircle * starRadius;

            Color32 blueStar = new Color32(System.Convert.ToByte(Random.Range(0, 60)), System.Convert.ToByte(Random.Range(200, 255)), 255, 255);
            Color32 yellowStar = new Color32(255, 255, System.Convert.ToByte(Random.Range(0, 255)), 255);
            Color32 orangeStar = new Color32(255, System.Convert.ToByte(Random.Range(140,200)),0,255);

            int randomStar = Random.Range(1, 300);

            points[i].position = new Vector3(randomVector2.x, randomVector2.y, 1.0f);
            
            if(randomStar < 100)
            {
                points[i].startColor = blueStar;
                Debug.Log("Generating blue star");
            }
            else if(randomStar > 100 && randomStar < 200)
            {
                points[i].startColor = yellowStar;
                Debug.Log("Generating yellow star");
            }
            else
            {
                points[i].startColor = orangeStar;
                Debug.Log("Generating orange star");
            }
            
            points[i].startSize = Random.Range(minStarSize,maxStarSize);
        }
    }
    
	// Update is called once per frame
	void Update () {
	   if(points == null)
       {
            Debug.Log("Creating Stars");
            CreateStars();    
       } 

       for(int i = 0; i < starsMax; i++){
           Vector2 randomVector2 = Random.insideUnitCircle * starRadius;
           
           if(Vector3.Distance(points[i].position, GameManager.instance.playerShip.transform.position) > starRadius)
           {
               
               Debug.Log("Star at max distance. Moving");
               points[i].position = new Vector3(randomVector2.x, randomVector2.y, 1.0f);
           }
       }

        ps.SetParticles(points, points.Length);
    }
}
