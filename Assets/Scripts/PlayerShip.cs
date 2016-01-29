using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShip : MonoBehaviour {

    public float CurrentFuel;
    public float CurrentFuelUsage;
    public float TurnSpeed;
    public float MaxSpeed;
    Rigidbody2D rigidBody;
    
    
    // Use this for initialization
    void Start () 
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
	
    private void OnDisable()
    {
        //GameManager.instance.PlayerFuelPoints = fuel;
    }
    
	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(0.0f, 0.0f, -Input.GetAxis("Horizontal") * TurnSpeed);
        
        
        if(Input.GetAxisRaw("Vertical") == 1)
        {
            if(CurrentFuel - CurrentFuelUsage < 0.0f)
                CurrentFuel = 0.0f;
            else
            {
                CurrentFuel -= CurrentFuelUsage;
                rigidBody.AddForce(transform.up * MaxSpeed);
            }
        }
    }
    
    public static Vector2 GetPlayerShipCoordsDisplayFormatted(PlayerShip playership)
    {
        return new Vector2((int)(playership.transform.position.x * GameManager.instance.worldCoordModifier), (int)(playership.transform.position.y * GameManager.instance.worldCoordModifier));
    }
    
}
