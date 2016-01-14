using UnityEngine;
using System.Collections;

public class ScrollingScript : MonoBehaviour {

    // public Vector2 Speed = new Vector2(2,2);
    // public Vector2 Direction = new Vector2(-1,0);
    // public bool isLinkedToCamera = false;
    public float speed = 0;
    float pos = 0;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Vector3 movement = new Vector3(Speed.x * Direction.x, Speed.y * Direction.y, 0);
        // movement *= Time.deltaTime;
        // transform.Translate(movement);
        // if (isLinkedToCamera)
        // {
        //     Camera.main.transform.Translate(movement);
        // }

        if(Input.GetAxisRaw("Vertical") == 1)
        {
            pos += speed;
            if(pos > 1.0f)
                pos -= 1.0f;

            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(pos, 0);
            
        }



    }
    
}
