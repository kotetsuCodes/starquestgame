using UnityEngine;
using System.Collections;

public class BaseStarSystem : MonoBehaviour
{
    public float SizeOfStar;
    public GameObject[] Planets;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
