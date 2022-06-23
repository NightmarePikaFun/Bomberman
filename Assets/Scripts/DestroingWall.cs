using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroingWall : MonoBehaviour
{
    private GameObject terrain;

    void Start()
    {
        terrain = GameObject.FindGameObjectWithTag("Terrain");
    }

    private void OnDestroy()
    {
        Debug.Log("WallDestroy");
        //CheckNormalCoords
        terrain.GetComponent<WallCreator>().RemoveWall(new Vector2Int((int)this.transform.position.x/2, (int)this.transform.position.z/2));
    }

}
