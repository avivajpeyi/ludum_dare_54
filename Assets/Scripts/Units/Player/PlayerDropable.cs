using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropable : AttackerBase
{
    [SerializeField] GameObject dropableObject;

    float timer;
    float timeBetweenDrops = 2f;


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (_canAttack &&
            Input.GetButton("Drop") &&
            timer >= timeBetweenDrops &&
            Time.timeScale != 0)
        {
            DropObject();
        }
    }

    void DropObject()
    {
        timer = 0f;
        Instantiate(dropableObject, gameObject.transform.position,
            gameObject.transform.rotation);
    }
}