using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void GoToMainGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void GoToShop()
    {
        SceneManager.LoadScene("SkinShop");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
