using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private InputField count;

    private int enemyMaxCount = -1;

    //tmp active
    private bool active = true;

    // Update is called once per frame
    void Update()
    {
        //TMP code
        if(Input.GetKeyUp(KeyCode.J) && active)
        {
            active = false;
            Invoke("Spawn", 5);
        }
    }

    private void Spawn()
    {
        Debug.Log("Spawn");
        System.Random rand = new System.Random();
        int posX = rand.Next(0,20);//tmp const value
        GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");
        if ((enemyMaxCount > 0 && enemyMaxCount != go.Length) || enemyMaxCount < 0)
            Instantiate(enemy, new Vector3(48, 1, posX * 2), Quaternion.identity);
        if(GameObject.FindGameObjectWithTag("Player")!=null)
            Invoke("Spawn", 5);
    }

    public void StartSpawn()
    {
        if (count.text != "")
            enemyMaxCount = int.Parse(count.text);
        else
            enemyMaxCount = -1;
        Debug.Log("StartSpawn");
        Invoke("Spawn", 5);
    }
}
