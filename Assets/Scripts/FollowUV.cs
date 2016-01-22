using UnityEngine;
using System.Collections;

public class FollowUV : MonoBehaviour {

    float factor;
    float parallax = 2f;
    MeshRenderer meshRenderer;
    
    // Use this for initialization
    void Start () {
	   meshRenderer = GetComponent<MeshRenderer>();
       factor = 1.0f / transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        
        meshRenderer.material.SetTextureOffset("_MainTex", new Vector2(Camera.main.transform.position.x * factor, Camera.main.transform.position.y * factor));
        
        
	}
}
