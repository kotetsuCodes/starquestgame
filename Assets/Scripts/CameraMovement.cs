using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public Transform target;
    Camera camera;

    // Use this for initialization
    void Start () {
        camera = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        if(target)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, -10), 0.1f);

        }
	}
}
