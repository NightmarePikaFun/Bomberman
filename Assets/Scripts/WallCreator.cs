using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject PathCreator;

    private int[,] mapa = new int[1000,1000];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2Int[] GetCoordsExplose(Vector3 input)
    {
        List<Vector2Int> outVec = new List<Vector2Int>();
        Vector2Int vec = PathCreator.GetComponent<PathCreator>().CheckCoordsRight(input);
        vec = vec / 2;
        outVec.Add(vec);
        outVec.AddRange(CheckSide(vec, new Vector2Int(1, 0)));
        outVec.AddRange(CheckSide(vec, new Vector2Int(-1, 0)));
        outVec.AddRange(CheckSide(vec, new Vector2Int(0, 1)));
        outVec.AddRange(CheckSide(vec, new Vector2Int(0, -1)));
        //Принимать массив, проходить по нему и добавлять в наш список

        return outVec.ToArray();//new Vector2Int[0];
    }

    private Vector2Int[] CheckSide(Vector2Int point, Vector2Int side)
    {
        List<Vector2Int> outVec = new List<Vector2Int>();
        //New tmp point to coords
        Vector2Int sumVec = point;
        for (int i = 0; i <3; i++)
        {
            if (point.x+side.x>=0 && point.x+side.x<mapa.GetLength(0) 
                && point.y + side.y >= 0 && point.y + side.y < mapa.GetLength(1))
            {
                sumVec += side;
                outVec.Add(sumVec);
                if (mapa[sumVec.x,sumVec.y]==1)              
                    break;
            }
        }
        return outVec.ToArray();
    }
}
