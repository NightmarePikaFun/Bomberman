using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
            this.transform.Translate(new Vector3(-speed, 0, 0));
        else if (Input.GetKey(KeyCode.S))
            this.transform.Translate(new Vector3(speed, 0, 0));
        else if (Input.GetKey(KeyCode.A))
            this.transform.Translate(new Vector3(0, 0, -speed));
        else if (Input.GetKey(KeyCode.D))
            this.transform.Translate(new Vector3(0, 0, speed));
    }

    public float GetSpeed()
    {
        return speed;
    }
}
