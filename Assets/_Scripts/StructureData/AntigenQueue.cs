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
    private bool shouldResize = true;
    public AntigenQueue(int Size = 10, float ResizeFactor = 2f)
    {
        queue = new QueueType[size];
        resizeFactor = ResizeFactor;
        size = Size;
    }
    public void Queue(QueueType value)
    {
        lastIndex++;
        queue[lastIndex] = value;
        if (lastIndex + 1 >= queue.Length)
        {
            if (shouldResize)
            {
                if (firstIndex > size/2)
                {
                    Reorganize();
                }
                else
                {
                    Resize();
                }
            }
            else if (firstIndex != 0)
            {
                Reorganize();
            }
            else
            {
                return;
            }
        }
    }
    public QueueType Unqueu()
    {
        QueueType temp = queue[firstIndex];
        queue[firstIndex] = default(QueueType);
        firstIndex++;
        return temp;
    }
    public bool IsEmpty()
    {
        return firstIndex > lastIndex;
    }
    private void Resize()
    {
        if (queue == null || lastIndex == -1)
        {
            Debug.LogError("Invalid fila or not initialized");
            Debug.Break();
            return;
        }
        QueueType[] tempFila = queue;
        queue = new QueueType[(int)(tempFila.Length * resizeFactor)];
        int tempInt = 0;
        foreach (QueueType p in tempFila)
        {
            queue[tempInt] = p;
            tempInt++;
        }
#if UNITY_EDITOR 
        //Debug.Log("Resized");
#endif
    }
    public void Reorganize()
    {
        string tempInt = "";
        foreach (QueueType f in queue)
        {
            tempInt += "\n" + f;
        }
#if UNITY_EDITOR
        Debug.Log(tempInt);
#endif
        QueueType[] tempFila = queue;
        int offsetFromStart = firstIndex;
        queue = new QueueType[queue.Length];
        int counter = 0;
        while (offsetFromStart < lastIndex + 1)
        {
            queue[counter] = tempFila[offsetFromStart];
            counter++;
            offsetFromStart++;
        }
        firstIndex = 0;
        lastIndex = offsetFromStart;

        tempInt = "";
        foreach (QueueType f in queue)
        {
            tempInt += "\n" + f;
        }
        //Debug.Log(tempInt);
    }
    public void DebugQueue(string debugDenonimator = "Non Especified Debug:")
    {
        string tempS = debugDenonimator + "\n FirstIndex:" + firstIndex + "\n LastIndex:" + lastIndex + "\n";
        foreach (QueueType q in queue)
        {
            tempS += q + "\n";
        }
#if UNITY_EDITOR
        Debug.Log(tempS);
#endif
    }
    public QueueType GetFirstValue()
    {
        return queue[firstIndex];
    }
    public QueueType[] GetArray()
    {
        return queue;
    }
}
