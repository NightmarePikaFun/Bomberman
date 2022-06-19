using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject boxModel;

    [SerializeField]
    private GameObject nodeModel;
    [SerializeField]
    private Terrain landscape = null;
    [SerializeField]
    private int gridDelta = 100;

    private PathNode[,] grid = null;
    private int[,] mapa = null;

    private int sizeX;
    private int sizeZ;

    void Start()
    {
        Vector3 terrainSize = landscape.terrainData.bounds.size;
        sizeX = (int)(terrainSize.x / gridDelta);
        sizeZ = (int)(terrainSize.z / gridDelta);
        mapa = new int[sizeX, sizeZ];
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                mapa[x, z] = 0;
            }
        }
        mapa[4 / gridDelta, 10 / gridDelta] = 1;
        mapa[(int)(14/ gridDelta), (int)(16/gridDelta)] = 1;

        for(int i = 1;i< mapa.GetLength(0); i+=2)
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
                    Instantiate(boxModel, new Vector3(i * gridDelta, 0, j * gridDelta), Quaternion.identity);
            }
        }
    }

    void InitStart()
    {
        Vector3 terrainSize = landscape.terrainData.bounds.size;
        sizeX = (int)(terrainSize.x / gridDelta);
        sizeZ = (int)(terrainSize.z / gridDelta);
        grid = new PathNode[sizeX, sizeZ];
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                grid[x, z] = new PathNode(nodeModel, false, new Vector3(x * gridDelta, -1, z * gridDelta));
                grid[x, z].ParentNode = null;
                //grid[x, z].Fade();
                //Instantiate(nodeModel, new Vector3(x+1*x+1, 1, z+1*z+1), Quaternion.identity);
            }
        }
    }

    private void CheckWalkableNodes()
    {
        for(int i = 0; i <grid.GetLength(0);i++)
        {
            for(int j = 0; j<grid.GetLength(1);j++)
            {
                grid[i,j].walkable = true;
                if (mapa[i, j] == 1)
                    grid[i, j].walkable = false;
            }
        }
        /*foreach(PathNode node in grid)//переписать на for
        {
            node.walkable = true;
            if (Physics.CheckSphere(node.body.transform.position, 0.1f))
            {
                //node.walkable = false;
            }
            if (node.walkable)
                node.Fade();
            else
            {
                node.Red();
            }
        }*/
    }

    private List<Vector2Int> GetNeighbours(Vector2Int current)
    {
        List<Vector2Int> nodes = new List<Vector2Int>();
        int x = current.x;
        int y = current.y;
        if(x-1>=0)
        {
            nodes.Add(new Vector2Int(x-1, y));
        }
        if(y-1>=0)
        {
            nodes.Add(new Vector2Int(x, y-1));
        }
        if(y+1<grid.GetLength(1))
        {
            nodes.Add(new Vector2Int(x, y+1));
        }
        if(x+1<grid.GetLength(0))
        {
            nodes.Add(new Vector2Int(x+1, y));
        }
        //8 соседий
        /*for (int x = current.x - 1; x <= current.x + 1; ++x)
            for (int y = current.y - 1; y <= current.y + 1; ++y)
                if (x >= 0 && y >= 0 && x < grid.GetLength(0) && y < grid.GetLength(1) && (x != current.x || y != current.y))
                    nodes.Add(new Vector2Int(x, y));*/
        return nodes;
    }

    public Vector3 Astar(Vector2Int startNode, Vector2Int finishNode)
    {
        InitStart();
        int ds = 0;
        //  Очищаем все узлы - сбрасываем отметку родителя, снимаем подсветку
        foreach (var node in grid)
        {
            //node.Fade();
            node.ParentNode = null;
        }
        CheckWalkableNodes();
        Debug.Log(sizeX+" t "+ sizeZ);
        Debug.Log(startNode.x+ " n "+ startNode.y);
        PathNode start = grid[startNode.x, startNode.y];
        start.ParentNode = null;
        start.Distance = 0;
        PriorityQueue<float, Vector2Int> pq = new PriorityQueue<float, Vector2Int>();
        pq.Enqueue(1, startNode);
        while (pq.Count != 0)
        {
            Vector2Int current = pq.Dequeue();
            float len;
            //  Если достали целевую - можно заканчивать (это верно и для A*)
            if (current == finishNode)
                break;
            //  Получаем список соседей
            var neighbours = GetNeighbours(current);
            foreach (var pathPoint in neighbours)
            {
                if (grid[pathPoint.x, pathPoint.y].walkable && grid[pathPoint.x, pathPoint.y].Distance > grid[current.x, current.y].Distance + PathNode.Dist(grid[pathPoint.x, pathPoint.y], grid[current.x, current.y]))
                {
                    //grid[pathPoint.x, pathPoint.y].Red();
                    grid[pathPoint.x, pathPoint.y].ParentNode = grid[current.x, current.y];
                    len = grid[pathPoint.x, pathPoint.y].Distance + PathNode.Dist(grid[pathPoint.x, pathPoint.y], grid[finishNode.x, finishNode.y]);//(Mathf.Sqrt(Mathf.Pow(finishNode.x - pathPoint.x, 2)+Mathf.Pow(finishNode.y- pathPoint.y,2)));
                    pq.Enqueue(len, pathPoint);
                }
                ds++;
            }
        }
        var pathElem = grid[finishNode.x, finishNode.y];
        Vector3 outElem = pathElem.worldPosition;
        while (pathElem != null)
        {
            //pathElem.Illuminate();
            //Instantiate(nodeModel2, pathElem.worldPosition, Quaternion.identity);
            if(pathElem.ParentNode!=null || ds==0)
            {
                outElem = pathElem.worldPosition;
            }
            pathElem = pathElem.ParentNode;
        }
        grid = null;
        if ((outElem.x / gridDelta == finishNode.x && outElem.x / gridDelta != startNode.x)
            && (outElem.z / gridDelta == finishNode.y && outElem.z / gridDelta != startNode.y))
            outElem = new Vector3(startNode.x * gridDelta, 0, startNode.y * gridDelta);
        return outElem;
    }
}
