using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour 
{
    public float TurnSpeed;
    public float MaxSpeed;
    Rigidbody2D rigidBody;
    
    // Use this for initialization
	void Start () 
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(0.0f, 0.0f, -Input.GetAxis("Horizontal") * TurnSpeed);
        
        
        if(Input.GetAxisRaw("Vertical") == 1)
            rigidBody.AddForce(transform.up);
        //transform.position += transform.up * Time.deltaTime * MaxSpeed;

    }
    
}
