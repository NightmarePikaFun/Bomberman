using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //[SerializeField]
    //private GameObject xWall, zWall;
    [SerializeField]
    private GameObject exploseBlock;
    [SerializeField]
    private GameObject terrain;
    // Start is called before the first frame update
    void Start()
    {
        terrain = GameObject.FindGameObjectWithTag("Terrain");
        Invoke("Explose", 3);
    }

    private void Explose()
    {
        //Calc bomb-explose position spawn get Vec2Int[] coords
        Vector2Int[] coords = terrain.GetComponent<WallCreator>().GetCoordsExplose(this.transform.position);
        for(int i = 0;  i<coords.Length;i++)
        {
            Instantiate(exploseBlock, new Vector3(coords[i].x*2, 1, coords[i].y*2), Quaternion.identity);
        }
        //xWall.SetActive(true);
        //zWall.SetActive(true);
        Destroy(this.gameObject);
        //Invoke("DestroyBomb", 0.2f);
    }

    private void DestroyBomb()
    { 
        Destroy(this.gameObject);
    }

}
