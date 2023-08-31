using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Fila<FilaType>
{
    private FilaType[] _content = null;
    private int _size = 0;
    private int _front = 0;
    private int _rear = 0;
    private int _resize = 0;

    public Fila(int size, int factor = -1)
    {
        _content = new FilaType[size];
        _size = size;
        _resize = factor;

    }

    public void Enqueue(FilaType value)
    {
        if (_rear == _size)
        {
            Resize();
        }
        _content[_rear++] = value;
    }

    public FilaType Dequeue()
    {
        UnityEngine.Debug.Assert(_rear != _front);
        int index = _front++;
        if (_front == _rear)
        {
            _front = _rear = 0;
        }
        return _content[index];
    }
    public bool isEmpty()
    {
        return _rear == _front;
    }

    public int size()
    {
        return _rear - _front;
    }
    private void Resize()
    {
        int reSizeLenght = _resize == -1 ? (int)_size / 2 : _resize;
        _size += reSizeLenght;
        FilaType[] newArray = new FilaType[_size];
        for (int i = _front; i < _content.Length; i++)
        {
            newArray[i - _front] = _content[i];
        }
        _rear = -_front;
        _front = 0;

        _content = newArray;
    }
    public int SeeInn()
    {
        int temp= (_rear - _front);
        return temp;
    }
}
