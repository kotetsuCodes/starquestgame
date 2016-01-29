using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveGameButtonClick : MonoBehaviour {

    public void SaveClick()
    {
        Debug.Log("Button Clicked");
        GameManager.instance.Save();
    }
    
    public void LoadClick()
    {
        Debug.Log("Load Button Clicked");
        GameManager.instance.Load();
    }
}
