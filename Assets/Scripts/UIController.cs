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
    [SerializeField]
    private AudioSource sourceMusic, mainMusic;
    [SerializeField]
    private AudioClip loseClip, winClip;

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
        obj = GameObject.FindGameObjectsWithTag("Player");
        DestroyObject(obj);
        //TMP
        obj = GameObject.FindGameObjectsWithTag("destroy");
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
        mainMusic.Stop();
        sourceMusic.clip = loseClip;
        sourceMusic.Play();
        uiLose.SetActive(true);

    }

    public void PlayWinMusic()
    {
        mainMusic.Stop();
        sourceMusic.clip = winClip;
        sourceMusic.Play();
    }

    public void StartGame()
    {
        mainMusic.Play();
        canDeleteWallOnMap = true;
        //SpawnPlayer
        Instantiate(playerPref, startPos, Quaternion.identity);
        //StartSpawnEnemy
        terrain.GetComponent<EnemySpawner>().StartSpawn();
        //Close all UI
        uiLose.SetActive(false);
        uiStart.SetActive(false);
        uiWin.SetActive(false);
        //Generate Space
        terrain.GetComponent<WallCreator>().CreateMap();
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
