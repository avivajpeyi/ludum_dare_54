using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropable : MonoBehaviour
{
    [SerializeField] GameObject dropableObject;

    float timer;
    float timeBetweenDrops = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Drop") && timer >= timeBetweenDrops && Time.timeScale != 0)
        {
            DropObject();
        }
        
    }

    void DropObject()
    {
        timer = 0f;
        // gameObject.transform.GetLocalPositionAndRotation(out var location, out var rotation);
        Instantiate(dropableObject, gameObject.transform.localPosition, gameObject.transform.localRotation);
    }
}
