using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject terrain;
    [SerializeField]
    private GameObject playerPref;
    [SerializeField]
    private Vector3Int startPos;
    [SerializeField]
    private GameObject uiStart,uiLose,uiWin;

    private bool canDeleteWallOnMap = true;

    public void RestartGame()
    {
        //ClearEnemy
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Enemy");
        DestroyObject(obj);
        //DestroyWall
        canDeleteWallOnMap = false;
        obj = GameObject.FindGameObjectsWithTag("Wall");
        DestroyObject(obj);
        obj = GameObject.FindGameObjectsWithTag("WallNotDestroy");
        DestroyObject(obj);
        Invoke("StartGame",1);
        
    }

    public bool GetCanDeleteWallOnMap()
    {
        return canDeleteWallOnMap;
    }

    private void DestroyObject(GameObject[] input)
    {
        foreach (var obj in input)
        {
            Destroy(obj);
        }
    }

    public void ShowUILose()
    {
        uiLose.SetActive(true);
    }

    public void StartGame()
    {
        canDeleteWallOnMap = true;
        //SpawnPlayer
        Instantiate(playerPref, startPos, Quaternion.identity);
        //StartSpawnEnemy
        terrain.GetComponent<EnemySpawner>().StartSpawn();
        //Close all UI
        uiLose.SetActive(false);
        uiStart.SetActive(false);
        //Generate Space
        terrain.GetComponent<WallCreator>().CreateMap();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
