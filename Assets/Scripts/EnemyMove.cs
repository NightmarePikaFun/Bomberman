using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private PathCreator pathCreator;

    //private Vector3[] pathToTarget;
    private Vector3 pointToGo;
    private Vector3 vectorToGo;

    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = player.GetComponent<PlayerMove>().GetSpeed();
        GetNewPoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!EqualsPoint(this.transform.position, pointToGo))
        {
            this.transform.Translate(vectorToGo);
        }
        else
        {
            GetNewPoint();
            //Получить следующую точку
        }
    }

    private void GetNewPoint()
    {
        //pointToGo = pathCreator;//получаем точку
        pointToGo = player.transform.position;//TMP path
        vectorToGo = GetSideMove(this.transform.position, pointToGo);
    }

    private bool EqualsPoint(Vector3 point1, Vector3 point2)
    {
        bool retValue = false;
        if (Mathf.Abs(point1.x - point2.x) <= 0.1 && Mathf.Abs(point1.z - point2.z) <= 0.1)
            retValue = true;
        return retValue;
    }

    private Vector3 GetSideMove(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 returnVector = new Vector3();
        float xSide = 0,
            zSide = 0;
        xSide = endPoint.x-startPoint.x;
        zSide = endPoint.y - startPoint.y;
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
}
