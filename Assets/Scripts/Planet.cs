using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour
{
    public float PlanetSize;
    public UnityEngine.Sprite PlanetSprite;
    public int NumberOfCities;
    public Vector3 PlanetLocation;
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
