using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private GameObject pathCreator;
    [SerializeField]
    private GameObject playerModel;

    private Animator playerAnimtor;
    // Start is called before the first frame update
    void Start()
    {
        pathCreator = GameObject.FindGameObjectWithTag("Terrain");
        playerAnimtor = playerModel.GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerAnimtor.SetBool("Move", true);
            playerModel.transform.rotation = Quaternion.Euler(0, 90, 0);
            CanMove(new Vector3(-speed, 0, 0));
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerAnimtor.SetBool("Move", true);
            playerModel.transform.rotation = Quaternion.Euler(0, -90, 0);
            CanMove(new Vector3(speed, 0, 0));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            playerAnimtor.SetBool("Move", true);
            playerModel.transform.rotation = Quaternion.Euler(0, 0, 0);
            CanMove(new Vector3(0, 0, -speed));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerAnimtor.SetBool("Move", true);
            playerModel.transform.rotation = Quaternion.Euler(0, -180, 0);
            CanMove(new Vector3(0, 0, +speed));
        }
        else
        {
            playerAnimtor.SetBool("Move", false);
        }
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
