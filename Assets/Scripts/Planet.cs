using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour
{
    public Planet(float planetSize, Vector3 planetLocation, UnityEngine.Sprite planetSprite, int numberOfCities)
    {
        this.PlanetSize = planetSize;
        this.PlanetLocation = planetLocation;
        this.PlanetSprite = planetSprite;
        this.NumberOfCities = numberOfCities;
    }
    
    public float PlanetSize;
    public UnityEngine.Sprite PlanetSprite;
    public int NumberOfCities;
    public Vector3 PlanetLocation;
    

}
