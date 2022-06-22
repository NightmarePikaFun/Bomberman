using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBombExplose : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeleteFireWall", 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player" || other.gameObject.tag == "Wall")
        {
            Debug.Log("¡¿’");
            Destroy(other.gameObject);
        }
    }

    private void DeleteFireWall()
    {
        Destroy(this.gameObject);
    }
}
