using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;




public abstract class IPathfinding
{
    /// <summary>
    /// Method finds path between two nodes in a graph.
    /// </summary>
    /// <param name="edges">
    /// Graph edges represented as nested dictionaries. Outer dictionary contains all nodes in the graph, inner dictionary contains 
    /// its neighbouring nodes with edge weight.
    /// </param>
    /// <returns>
    /// If a path exist, method returns list of nodes that the path consists of. Otherwise, empty list is returned.
    /// </returns>

    protected List<T> GetNeigbours<T>(Dictionary<T, Dictionary<T, int>> edges, T node) where T : IGraphNode
    {
        if (!edges.ContainsKey(node))
        {
            return new List<T>();
        }
        return edges[node].Keys.ToList();
    }
}


/// <summary>
/// Represents a prioritized queue.
/// </summary>
public interface IPriorityQueue<T>
{
    /// <summary>
    /// Number of items in the queue.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Method adds item to the queue.
    /// </summary>
    void Enqueue(T item, int priority);
    /// <summary>
    /// Method returns item with the LOWEST priority value.
    /// </summary>
    T Dequeue();
}

/// <summary>
/// Represents a node in a priority queue.
/// </summary>
class PriorityQueueNode<T> : IComparable
{
    public T Item { get; private set; }
    public float Priority { get; private set; }

    public PriorityQueueNode(T item, float priority)
    {
        Item = item;
        Priority = priority;
    }

    public int CompareTo(object obj)
    {
        return Priority.CompareTo((obj as PriorityQueueNode<T>).Priority);
    }
}

class HeapPriorityQueue<T> : IPriorityQueue<T>
{
    private List<PriorityQueueNode<T>> _queue;

    public HeapPriorityQueue()
    {
        _queue = new List<PriorityQueueNode<T>>();
    }

    public int Count
    {
        get { return _queue.Count; }
    }

    public void Enqueue(T item, int priority)
    {
        _queue.Add(new PriorityQueueNode<T>(item, priority));
        int ci = _queue.Count - 1;
        while (ci > 0)
        {
            int pi = (ci - 1) / 2;
            if (_queue[ci].CompareTo(_queue[pi]) >= 0)
                break;
            var tmp = _queue[ci];
            _queue[ci] = _queue[pi];
            _queue[pi] = tmp;
            ci = pi;
        }
    }
    public T Dequeue()
    {
        int li = _queue.Count - 1;
        var frontItem = _queue[0];
        _queue[0] = _queue[li];
        _queue.RemoveAt(li);

        --li;
        int pi = 0;
        while (true)
        {
            int ci = pi * 2 + 1;
            if (ci > li) break;
            int rc = ci + 1;
            if (rc <= li && _queue[rc].CompareTo(_queue[ci]) < 0)
                ci = rc;
            if (_queue[pi].CompareTo(_queue[ci]) <= 0) break;
            var tmp = _queue[pi]; _queue[pi] = _queue[ci]; _queue[ci] = tmp;
            pi = ci;
        }
        return frontItem.Item;
    }
}
