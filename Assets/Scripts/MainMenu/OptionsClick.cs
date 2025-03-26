using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsClick : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnButtonClick()
    {
        SceneManager.LoadScene("Ui_Setting");
    }    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
