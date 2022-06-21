using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private GameObject pathCreator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
            CanMove(new Vector3(-speed, 0, 0));//this.transform.Translate(new Vector3(-speed, 0, 0));
        else if (Input.GetKey(KeyCode.S))
            CanMove(new Vector3(speed, 0, 0)); //this.transform.Translate(new Vector3(speed, 0, 0));
        else if (Input.GetKey(KeyCode.A))
            CanMove(new Vector3(0, 0, -speed)); //this.transform.Translate(new Vector3(0, 0, -speed));
        else if (Input.GetKey(KeyCode.D))
            CanMove(new Vector3(0, 0, +speed)); //this.transform.Translate(new Vector3(0, 0, speed));
    }

    private void CanMove(Vector3 inputTransform)
    {
        Vector2Int vect = pathCreator.GetComponent<PathCreator>().CheckCoordsRight(inputTransform+this.transform.position);
        if(pathCreator.GetComponent<PathCreator>().CheckWall(vect))
            this.transform.Translate(inputTransform);
    }

    public float GetSpeed()
    {
        return speed;
    }

    
}
