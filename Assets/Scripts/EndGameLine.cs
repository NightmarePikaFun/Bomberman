using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameLine : MonoBehaviour
{
    [SerializeField]
    private GameObject textEndGame;
    [SerializeField]
    private GameObject terrain;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //Find game object with tag and remove tag
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.tag = "Untagged";//New tag PlayerFalse or kill ???
            textEndGame.SetActive(true);
            Debug.Log("EndGame");
            //this.gameObject.SetActive(false);
        }
    }
}
