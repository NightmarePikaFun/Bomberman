using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathCreator : MonoBehaviour
{
    void Start()
    {
        //Debug.Log("start");
        
    }

    void InitStart()
    {
        Vector3 terrainSize = landscape.terrainData.bounds.size;
        int sizeX = (int)(terrainSize.x / gridDelta);
        int sizeZ = (int)(terrainSize.z / gridDelta);
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

    [SerializeField]
    private GameObject nodeModel;
    [SerializeField]
    private Terrain landscape = null;
    [SerializeField]
    private int gridDelta = 100;

    private PathNode[,] grid = null;

    private void CheckWalkableNodes()
    {
        foreach(PathNode node in grid)
        {
            node.walkable = true;
            /*if (Physics.CheckSphere(node.body.transform.position, 0.1f))
            {
                //node.walkable = false;
            }
            if (node.walkable)
                node.Fade();
            else
            {
                node.Red();
            }*/
        }
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
        //Debug.Log("Start A*");
        //  Очищаем все узлы - сбрасываем отметку родителя, снимаем подсветку
        foreach (var node in grid)
        {
            //node.Fade();
            node.ParentNode = null;
        }
        CheckWalkableNodes();
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
        //Debug.Log("End A* " + ds);
        grid = null;
        return outElem;
    }
}
