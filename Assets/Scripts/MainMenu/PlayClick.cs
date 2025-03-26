using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayClick : MonoBehaviour
{
    public TextDisplay textDisplay;
    // Start is called before the first frame update
    public void OnButtonClick()
    {
        SceneManager.LoadScene("Map_Tutorial");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
