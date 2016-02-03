using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

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
    void FixedUpdate()
    {
        transform.Rotate(0.0f, 0.0f, -Input.GetAxis("Horizontal") * TurnSpeed);

        if(Input.GetKeyDown("space"))
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<BoxCollider2D>().size.x);

            if (colliders != null && colliders.Length > 1)
            {
                if (colliders[1].GetComponentInParent<BaseStarSystem>() != null)
                {
                    Debug.Log("Number Of Planets: " + colliders[1].GetComponentInParent<BaseStarSystem>().Planets.Length);
                    var baseStarSystem = colliders[1].GetComponentInParent<BaseStarSystem>();

                    GameManager.instance.CurrentStarSystem = baseStarSystem;

                    Debug.Log("base star system is null before level load: " + (GameManager.instance.CurrentStarSystem == null).ToString());

                    SceneManager.LoadScene("TestStarSystem");

                }
            }
        }
        //if(GameManager.instance.BaseStarArray.Any(x => (int)x.transform.position.x == (int)transform.position.x && (int)x.transform.position.y == (int)transform.position.y))
        //Debug.Log("Player ship is on star");
        //}

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
