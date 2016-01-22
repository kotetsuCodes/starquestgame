using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShip : MonoBehaviour {

    public Text fuelText;
    public Text xCoordText;
    public Text yCoordText;

    private float fuel;

    public float TurnSpeed;
    public float MaxSpeed;
    Rigidbody2D rigidBody;
    
    
    // Use this for initialization
    void Start () {
        
        rigidBody = GetComponent<Rigidbody2D>();

        fuel = GameManager.instance.playerFuelPoints;

        fuelText.text = "Fuel: " + fuel.ToString("0.00");
        xCoordText.text = gameObject.transform.position.x.ToString("0.00");
        yCoordText.text = gameObject.transform.position.y.ToString("0.00");
    }
	
    private void OnDisable(){
        GameManager.instance.playerFuelPoints = fuel;
    }
    
	// Update is called once per frame
	void Update () 
    {
        transform.Rotate(0.0f, 0.0f, -Input.GetAxis("Horizontal") * TurnSpeed);
        
        
        if(Input.GetAxisRaw("Vertical") == 1)
        {
            if(fuel - GameManager.instance.fuelUsage < 0.0f)
                fuel = 0.0f;
            else
            {
                fuel -= GameManager.instance.fuelUsage;
                rigidBody.AddForce(transform.up);
            }

            fuelText.text = "Fuel: " + fuel.ToString("0.00");

            int x = (int)(gameObject.transform.position.x * GameManager.instance.worldCoordModifier);
            int y = (int)(gameObject.transform.position.y * GameManager.instance.worldCoordModifier);

            xCoordText.text = x.ToString();
            yCoordText.text = y.ToString();

        }
        //transform.position += transform.up * Time.deltaTime * MaxSpeed;

    }
    
    
    
}
