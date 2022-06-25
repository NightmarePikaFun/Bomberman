using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject pathCreator;

    //private Vector3[] pathToTarget;
    private Vector3 pointToGo;
    private Vector3 vectorToGo;

    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pathCreator = GameObject.FindGameObjectWithTag("Terrain");
        if (player != null)
        {
            speed = player.GetComponent<PlayerMove>().GetSpeed();
            GetNewPoint();
        }
        /*pathCreator.GetComponent<PathCreator>().Astar(new Vector2Int((int)this.transform.position.x/2,
            (int)this.transform.position.z/2), new Vector2Int((int)player.transform.position.x / 2, (int)player.transform.position.z / 2));*/
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            if (!EqualsPoint(this.transform.position, pointToGo))
            {
                this.transform.Translate(vectorToGo);
            }
            else
            {
                GetNewPoint();
                //Получить следующую точку
            }
        }
    }

    private void GetNewPoint()
    {
        Vector2Int startPoint = pathCreator.GetComponent<PathCreator>().CheckCoordsRight(this.transform.position);//new Vector2Int((int)this.transform.position.x, (int)this.transform.position.z);
        Vector2Int playerPos = pathCreator.GetComponent<PathCreator>().CheckCoordsRight(player.transform.position);// new Vector2Int((int)player.transform.position.x, (int)player.transform.position.z);
        pointToGo = pathCreator.GetComponent<PathCreator>().Astar(new Vector2Int(startPoint.x / 2, startPoint.y / 2), 
                new Vector2Int(playerPos.x / 2, playerPos.y / 2));
        vectorToGo = GetSideMove(this.transform.position, pointToGo);
    }


    private bool EqualsPoint(Vector3 point1, Vector3 point2)
    {
        bool retValue = false;
        if (Mathf.Abs(point1.x - point2.x) <= 0.1 && Mathf.Abs(point1.z - point2.z) <= 0.1)
        {
            retValue = true;
        }
        return retValue;
    }

    private Vector3 GetSideMove(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 returnVector = new Vector3();
        float xSide = 0,
            zSide = 0;
        xSide = endPoint.x-startPoint.x;
        zSide = endPoint.z - startPoint.z;
        if(Mathf.Abs(xSide)> Mathf.Abs(zSide))
        {
            if(xSide>0)
            {
                returnVector = new Vector3(speed, 0, 0);
            }
            else
            {
                returnVector = new Vector3(-speed, 0, 0);
            }
        }
        else
        {
            if (zSide > 0)
            {
                returnVector = new Vector3(0, 0, speed);
            }
            else
            {
                returnVector = new Vector3(0, 0, -speed);
            }
        }
        return returnVector;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
