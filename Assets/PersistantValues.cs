using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantValues : MonoBehaviour
{
    public static int shipModel;
    public static bool inBattle;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
