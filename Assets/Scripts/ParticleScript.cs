using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {

    public int starsMax = 100;
    public float minStarSize = 0.1f;
    public float maxStarSize = 1.0f;
    public float starDistance = 10.0f;

    private float starDistanceSqr;
    private Transform tx;
    private ParticleSystem.Particle[] points;
    private ParticleSystem ps;

    // Use this for initialization
    void Start () {
        tx = transform;
        ps = GetComponent<ParticleSystem>();
        starDistanceSqr = starDistance * starDistance;
    }
	
    void CreateStars()
    {
        points = new ParticleSystem.Particle[starsMax];
        
        for(int i = 0; i < starsMax; i++){
            Vector2 randomVector2 = Random.insideUnitCircle;

            Color32 blueStar = new Color32(System.Convert.ToByte(Random.Range(0, 60)), System.Convert.ToByte(Random.Range(200, 255)), 255, 255);
            Color32 yellowStar = new Color32(255, 255, System.Convert.ToByte(Random.Range(0, 255)), 255);
            Color32 orangeStar = new Color32(255, System.Convert.ToByte(Random.Range(140,200)),0,255);

            int randomStar = Random.Range(0, 2);

            points[i].position = new Vector3(randomVector2.x, randomVector2.y, 0.0f) * starDistance + new Vector3(tx.position.x, tx.position.y, ps.transform.position.z);
            
            switch(randomStar){
                case 0: points[i].startColor = blueStar;
                    break;
                case 1: points[i].startColor = yellowStar;
                    break;
                case 2: points[i].startColor = orangeStar;
                    break;
            }
            
            
            points[i].startSize = Random.Range(minStarSize,maxStarSize);
        }
    }
    
	// Update is called once per frame
	void Update () {
	   if(points == null) CreateStars();

       for(int i = 0; i < starsMax; i++){
           Vector2 randomVector2 = Random.insideUnitCircle;
           
           if((points[i].position - tx.position).sqrMagnitude > starDistanceSqr){
               points[i].position = new Vector3(randomVector2.x, randomVector2.y, 1.0f) * starDistance + new Vector3(tx.position.x, tx.position.y, ps.transform.position.z);
           }
       }

        ps.SetParticles(points, points.Length);
    }
}
