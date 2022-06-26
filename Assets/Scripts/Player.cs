using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject bomb;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) || Input.GetKeyUp(KeyCode.Space))
            PutBomb();
    }

    private void PutBomb()
    {
        //Debug.Log("PutBomb");
        Vector3Int pos = new Vector3Int((int)this.transform.position.x, 1, (int)this.transform.position.z);
        if (this.transform.position.x - pos.x > 0.5)
            pos.x += 1;
        if (this.transform.position.z - pos.z > 0.5)
        {
            pos.z += 1;
        }
        pos.x = (int)(pos.x / 2);
        pos.x = pos.x*2;
        pos.z = (int)(pos.z / 2);
        pos.z = pos.z*2;
        GameObject bombInst = Instantiate(bomb, pos, Quaternion.identity);
        bombInst.SetActive(true);
    }

    private void OnDestroy()
    {  
        GameObject terrain = GameObject.FindGameObjectWithTag("Terrain");
        terrain.GetComponent<UIController>().ShowUILose();
    }
}
