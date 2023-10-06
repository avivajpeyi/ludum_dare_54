using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRandomComponent : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> gos;

    // Start is called before the first frame update
    void Start()
    {
        // Disable all game objects
        foreach (GameObject go in gos)
        {
            go.SetActive(false);
        }
        int randomIndex = Random.Range(0, gos.Count);
        gos[randomIndex].SetActive(true); 
        
        // remove this script
        Destroy(this);
    }
    
}
