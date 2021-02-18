using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameChooser : MonoBehaviour
{
    // Start is called before the first frame update
    public int ship;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        PersistantValues.shipModel = ship;
        SceneManager.LoadScene("battle");
    }

}
