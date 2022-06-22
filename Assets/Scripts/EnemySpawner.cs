using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [SerializeField]
    private GameObject enemy;

    //tmp active
    private bool active = true;

    // Update is called once per frame
    void Update()
    {
        //TMP code
        if(Input.GetKeyUp(KeyCode.J) && active)
        {
            active = false;
            Invoke("StartSpawn", 5);
        }
    }

    private void StartSpawn()
    {
        System.Random rand = new System.Random();
        int posX = rand.Next(0,20);//tmp const value
        Instantiate(enemy, new Vector3(44,1,posX*2),Quaternion.identity);
        Invoke("StartSpawn", 5);
    }
}
