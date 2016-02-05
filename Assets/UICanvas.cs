using UnityEngine;
using System.Collections;

public class UICanvas : MonoBehaviour {
    
    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }
}
