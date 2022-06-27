using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject PathCreator;
    [SerializeField]
    private GameObject boxModel;
    [SerializeField]
    private GameObject destroyWall;

    private int[,] mapa = new int[1000,1000];
    // Start is called before the first frame update
    void Start()
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
        Vector2Int sumVec = point;
        for (int i = 0; i <3; i++)
        {
            if (sumVec.x+side.x>=0 && sumVec.x+side.x<mapa.GetLength(0) 
                && sumVec.y + side.y >= 0 && sumVec.y + side.y < mapa.GetLength(1))
            {
                sumVec += side;
                outVec.Add(sumVec);
                if (mapa[sumVec.x,sumVec.y]>0)              
                    break;
            }
        }
        return outVec.ToArray();
    }

    public int[,] GetMapa()
    {
        return mapa;
    }

    public void RemoveWall(Vector2Int input)
    {
        Debug.Log("RemoveWall");
        mapa[input.x, input.y] = 0;
    }


    public void CreateMap()
    {
        int[] data = PathCreator.GetComponent<PathCreator>().GetSize();
        mapa = new int[data[0], data[1]];
        for (int x = 0; x < data[0]; x++)
        {
            for (int z = 0; z < data[1]; z++)
            {
                mapa[x, z] = 0;
            }
        }
        //mapa[4 / data[2], 10 / data[2]] = 1;
        //mapa[(int)(14 / data[2]), (int)(16 / data[2])] = 1;
        for (int i = 1; i < mapa.GetLength(0); i += 2)
        {
            for (int j = 1; j < mapa.GetLength(1); j += 2)
            {
                mapa[i, j] = 1;
            }
        }
        for (int i = 0; i < mapa.GetLength(0); i++)
        {
            for (int j = 0; j < mapa.GetLength(1); j++)
            {
                if (mapa[i, j] == 1)
                    Instantiate(boxModel, new Vector3(i * data[2], 1, j * data[2]), Quaternion.identity);
            }
        }
        CreateMapWall();
    }

    private void CreateMapWall()
    {
        System.Random randForPath = new System.Random();
        int xPathCoord = 2, yPasCoord = 4,xPlusPathCoord =0,yPlusPathCoord = 0;
        for(int i = 0; i < 6;i++)
        {
            if(i%2==0)
            {
                yPlusPathCoord = randForPath.Next(0, 5)*2;
                SafeRoot(yPasCoord, xPathCoord, yPlusPathCoord, 1);
            }
            else
            {
                xPlusPathCoord = randForPath.Next(0, 5)*2;
                SafeRoot(xPathCoord,yPasCoord,xPlusPathCoord,0);
            }
            xPathCoord += xPlusPathCoord;
            yPasCoord += yPlusPathCoord;
            yPlusPathCoord = 0;
            xPlusPathCoord = 0;
        }

        if(xPathCoord<mapa.GetLength(0))
        {
            xPlusPathCoord = mapa.GetLength(0) - 1 - xPathCoord;
        }
        for (int x = xPathCoord; x < xPathCoord + xPlusPathCoord; x++)
        {
            mapa[x, yPasCoord] = -1;
        }

        mapa[2, 4] = -1;
        mapa[3, 4] = -1;
        mapa[1, 4] = -1;
        mapa[2, 3] = -1;
        System.Random rand = new System.Random();
        for (int i = 0; i < mapa.GetLength(0)-1;i++)
        {
            for(int j = 0; j <mapa.GetLength(1);j++)
            {
                if (mapa[i,j]!=1 && rand.Next(0, 4) == 2 &&mapa[i,j]!=-1)
                {
                    mapa[i, j] = 2;
                    Instantiate(destroyWall, new Vector3(i * 2, 1, j * 2), Quaternion.identity);
                }
            }
        }
    }

    /// <summary>
    /// Make safe space root
    /// </summary>
    /// <param name="startPosChange">change parametre x or y</param>
    /// <param name="startPosConst">const not change parametre y or x</param>
    /// <param name="counterValue">change value</param>
    /// <param name="mapStr">0 or 1 map column or line</param>
    private void SafeRoot(int startPosChange,int startPosConst, int counterValue, int mapStr)
    {

        if (startPosChange + counterValue > mapa.GetLength(mapStr) - 1)
        {
            counterValue = mapa.GetLength(mapStr) - 1-startPosChange;
        }

        for (int i = startPosChange; i < startPosChange + counterValue; i++)
        {
            if (mapStr == 0)
                mapa[i, startPosConst] = -1;
            else
                mapa[startPosConst, i] = -1;
        }
    }

}
