using UnityEngine;
using System.Collections;

public class StarSystemView : MonoBehaviour {

    GameObject PlanetPrefab;

    // Use this for initialization
    void Start () {

        //Debug.Log("Game manager instance is null: " + (GameManager.instance == null).ToString());
        //Debug.Log("Current Star System is null:" + (GameManager.instance.CurrentStarSystem == null).ToString());

        // foreach(var planet in GameManager.instance.CurrentStarSystem.Planets)
        // {
        //     GameObject planetObj = (GameObject)Instantiate(PlanetPrefab, p.PlanetLocation, Quaternion.Euler(0.0f, 0.0f, 0.0f));
        //     planetObj.GetComponent<SpriteRenderer>().sprite = p.PlanetSprite;
        // }
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
