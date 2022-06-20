using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private GameObject xWall, zWall;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explose", 3);
    }

    private void Explose()
    {
        xWall.SetActive(true);
        zWall.SetActive(true);
        Invoke("DestroyBomb", 0.2f);
    }

    private void DestroyBomb()
    { 
        Destroy(this.gameObject);
    }

}
