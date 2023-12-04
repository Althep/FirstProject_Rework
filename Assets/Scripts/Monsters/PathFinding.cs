using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public struct Node : IComparable<Node>
{
    public int x;
    public int y;
    public int F;
    public int G;
    //F = G+H;
    //G = Cost
    //H = Distance;
    public int CompareTo(Node other)
    {
        if (F == other.F)
            return 0;
        return F > other.F ? 1 : -1;
    }
}
public class PriorityQueue<T> where T : IComparable<T> 
{
    //작은 순으로 정렬하는 우선순위 큐
    //parents index = (n-1)/2
    //child lfet index = n*2+1
    //child right index = n*2+1
    List<T> heap = new List<T>();
    public void Push(T data)
    {
        int lastIndex = heap.Count - 1;
        int now = lastIndex;
        heap.Add(data);
        while (now>0) 
        {
            int next = (now - 1) / 2;
            if (now == next)
            {
                break;
            }   
            if (heap[now].CompareTo(heap[next]) > 0)
            {
                break;
            }
            T temp = heap[now];
            heap[now] = heap[next];
            heap[next] = temp;
            now = next;
        }
    }
    public T Pop()
    {
        if (heap.Count == 0)
        {
            throw new InvalidOperationException("The heap is empty.");
        }
        T ret = heap[0];
        int lastIndex = heap.Count - 1;
        int now = lastIndex;
        heap[0] = heap[lastIndex];
        heap.RemoveAt(lastIndex);
        lastIndex--;
        while (true)
        {
            int left = now * 2 + 1;
            int right = now * 2 + 2;
            int next = now;
            if (heap.Count < 1)
            {
                break;
            }
            if (left<heap.Count && heap[next].CompareTo(heap[left])<0)
            {
                next = left;
            }
            if (right<heap.Count &&heap[next].CompareTo(heap[right])<0)
            {
                next = right;
            }
            if (now == next)
            {
                break;
            }
            T temp = heap[now];
            heap[now] = heap[next];
            heap[next] = temp;
            now = next;
        }
        return ret;
    }
    public int Count()
    {
        return heap.Count;
    }

}
public class PathFinding : MonoBehaviour
{
    TileType[,] map;
    bool[,] closed;
    int[,] open;
    MapMake mapMakeScript;
    MonsterActSate monsterAct;
    Vector2 drawingStart;
    Vector2 drawingEnd;
    public Vector2 destination;
    public List<Node> path = new List<Node>();
    void Start()
    {
        mapMakeScript = GameObject.Find("GameManager").transform.GetComponent<MapMake>();
    }
    public void Astar(Vector2 Dest) 
    {
        map = mapMakeScript.map;
        Node[,] parents = new Node[mapMakeScript.ySize, mapMakeScript.xSize];
        PriorityQueue<Node> q = new PriorityQueue<Node>();
        Node start = new Node();
        closed = new bool[mapMakeScript.ySize,mapMakeScript.xSize];
        open = new int[mapMakeScript.ySize, mapMakeScript.xSize];
        //int[,] cost = new int[mapMakeScript.ySize,mapMakeScript.xSize];
        int[] dx = new int[] { -1, 0, 1, -1, 1, -1, 0, 1 };
        int[] dy = new int[] { 1, 1, 1, 0, 0, -1, -1, -1 };
        int[] cost = new int[] { 14, 10, 14, 10, 10, 14, 10, 14 };
        Vector2 nowPos = new Vector2();
        for (int i = 0; i < mapMakeScript.ySize; i++)
            for (int j = 0; j < mapMakeScript.xSize; j++)
                open[i, j] = Int32.MaxValue;
        nowPos = this.gameObject.transform.position;
        start.x = (int)nowPos.x;
        start.y = (int)nowPos.y;
        start.F = (int)(Vector2.Distance(Dest, nowPos) * 10) ;//((int)Math.Abs(Dest.x - start.x) + (int)Math.Abs(Dest.y - start.y))*10;
        start.G = 0;
        parents[start.y, start.x] = start;
        q.Push(start);
        while (q.Count()>0)
        {
            Node now = q.Pop();
            closed[now.y,now.x] = true;
            if (now.x == Dest.x && now.y == Dest.y)
            {
                GetPath(now, Dest, parents);
                break;
            }
            for (int i = 0; i < dx.Length; i++)
            {
                Vector2 closePos = new Vector2();
                closePos.x = now.x + dx[i];
                closePos.y = now.y + dy[i];
                int f = (int)(Vector2.Distance(Dest, closePos) * 10)  + cost[i];//((int)Math.Abs(Dest.x - closePos.x) + (int)Math.Abs(Dest.y - closePos.y)) * 10 + cost[i];//
                if (closePos.x < 0 || closePos.y < 0 || closePos.x > map.GetLength(1) - 1 || closePos.y > map.GetLength(0) - 1)
                    continue;
                if (closed[(int)closePos.y, (int)closePos.x])
                    continue;
                if (f > open[(int)closePos.y, (int)closePos.x])
                    continue;
                if (mapMakeScript.map[(int)closePos.y, (int)closePos.x] != TileType.tile && mapMakeScript.map[(int)closePos.y, (int)closePos.x] != TileType.door)
                    continue;
                Node next = new Node()
                {
                    x = (int)closePos.x,
                    y = (int)closePos.y,
                    F = f,// ((int)Math.Abs(Dest.y-closePos.y) + (int)Math.Abs(Dest.x-closePos.x))*10+cost[i],
                    G=cost[i]
                };
                q.Push(next);
                open[next.y, next.x] = next.F;
                parents[next.y, next.x] = now;
            }
        }
    }

    public void GetPath(Node LastNode,Vector2 Dest,Node[,]parents)
    {
        
        while(LastNode.x!=(int)this.gameObject.transform.position.x||LastNode.y!=(int)this.gameObject.transform.position.y)
        {
            LastNode = parents[LastNode.y, LastNode.x];
            path.Add(LastNode);
        }

        for (int i = 0; i < path.Count-1; i++)
        {
            drawingStart = new Vector2(path[i].x, path[i].y);
            drawingEnd = new Vector2(path[i+1].x, path[i+1].y);
            Debug.DrawLine(drawingStart, drawingEnd, Color.red,30f);
        }
        
    }
    

}
