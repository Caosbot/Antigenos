using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntigenQueue<QueueType>
{
    private QueueType[] queue;
    private float resizeFactor = 2f;
    private int firstIndex = 0;
    private int lastIndex = -1;
    private int size = 10;
    public AntigenQueue(int Size = 10, float ResizeFactor = 2f)
    {
        queue = new QueueType[size];
        resizeFactor = ResizeFactor;
        size = Size;
    }
    public void Queue(QueueType toAdd)
    {
        lastIndex++;
        queue[lastIndex] = toAdd;
        if (lastIndex + 1 == size)
        {
            Resize();
        }
    }
    public QueueType Unqueu()
    {
        if (IsEmpty())
        {
            return default(QueueType);
        }
        QueueType tempData = queue[firstIndex];
        queue[firstIndex] = default(QueueType);
        firstIndex++;
        if (firstIndex >= (queue.Length / 4))
        {
            Arrange();
        }
        return tempData;
    }
    public bool IsEmpty()
    {
        return firstIndex > lastIndex;
    }
    private void Resize()
    {
        QueueType[] tempQueue = queue;
        int tempInt = 0;
        queue = new QueueType[(int)(queue.Length * resizeFactor)];
        foreach (QueueType q in tempQueue)
        {
            queue[tempInt] = q;
            tempInt++;
        }
        Debug.Log("Resized");
    }
    private void Arrange()
    {
        DebugQueue("Arrange:");
        QueueType[] tempQueue = queue;
        int tempInt = firstIndex;
        int tempLocation = 0;
        queue = new QueueType[queue.Length];
        while (tempInt < lastIndex)
        {
            queue[tempLocation] = tempQueue[tempInt];
            tempLocation++;
            tempInt++;
        }
        queue[tempLocation] = tempQueue[tempInt];
        lastIndex = tempLocation;
        firstIndex = 0;
        Debug.Log("Rearranged");
    }
    public void DebugQueue(string debugDenonimator = "Non Especified Debug:")
    {
        string tempS = debugDenonimator + "\n";
        foreach (QueueType q in queue)
        {
            tempS += q + "\n";
        }
        Debug.Log(tempS);
    }
}
