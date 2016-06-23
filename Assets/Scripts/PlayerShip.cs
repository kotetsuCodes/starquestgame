using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerShip : MonoBehaviour {

    public float BaseFuelLevel = 10000;
    public float BaseFuelUsage = 0.75f;
    public float BaseTurnSpeed = 1.25f;
    public float BaseMaxSpeed = 2.0f;

    public float CurrentFuelLevel = 10000;
    public float CurrentFuelUsage = 0.75f;
    public float CurrentTurnSpeed = 1.25f;
    public float CurrentMaxSpeed = 2.0f;
    Rigidbody2D rigidBody;
    
    
    // Use this for initialization
    void Start () 
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
	
    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }
    
    private void OnDisable()
    {
        //GameManager.instance.PlayerFuelPoints = fuel;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0.0f, 0.0f, -Input.GetAxis("Horizontal") * BaseTurnSpeed);

        if(Input.GetKeyDown("space"))
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, GetComponent<BoxCollider2D>().size.x);

            if (colliders != null && colliders.Length > 1)
            {
                if (colliders[1].GetComponentInParent<BaseStarSystem>() != null)
                {
                    Debug.Log("player is in range of planet");
                    var baseStarSystem = colliders[1].GetComponentInParent<BaseStarSystem>();
                    GameManager.instance.CurrentStarSystem = baseStarSystem;

                    StartCoroutine(ChangeLevel());

                }
            }
        }
        
        if(Input.GetAxisRaw("Vertical") == 1)
        {
            if(CurrentFuelLevel - CurrentFuelUsage < 0.0f)
                CurrentFuelLevel = 0.0f;
            else
            {
                CurrentFuelLevel -= CurrentFuelUsage;
                rigidBody.AddForce(transform.up * BaseMaxSpeed);
            }
        }
    }
    
    IEnumerator ChangeLevel()
    {
        float fadeTime = GameManager.instance.GetComponent<Assets.Scripts.UI.ScreenFade>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);

        SceneManager.LoadScene("TestStarSystem");
    }

    public static Vector2 GetPlayerShipCoordsDisplayFormatted(PlayerShip playership)
    {
        return new Vector2((int)(playership.transform.position.x * GameManager.instance.GalacticCoordModifier), (int)(playership.transform.position.y * GameManager.instance.GalacticCoordModifier));
    }
    
}
