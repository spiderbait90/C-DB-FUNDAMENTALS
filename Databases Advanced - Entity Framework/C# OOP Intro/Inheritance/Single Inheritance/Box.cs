using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Box<T>
{
    private T[] data;
    private int count;

    public Box()
    {
        data = new T[4];
        Count = 0;
    }

    public int Count
    {
        get { return count; }
        set
        {
            if (value > data.Length)
            {
                throw new IndexOutOfRangeException("Collection full");
            }
            count = value;
        }
    }

    public void Add(T element)
    {
        if (Count == data.Length)
        {
            var oldData = data;
            data = new T[Count * 2];
            oldData.CopyTo(data, 0);
        }
        data[Count] = element;
        Count++;
    }
    public T Remove()
    {
        var lastIndex = Count - 1;
        var item = data[lastIndex];
        data[lastIndex] = default(T);
        Count--;
        return item;
    }

    public override string ToString()
    {
        var str = new StringBuilder();

        for (int i = 0; i < Count; i++)
        {
            str.Append(data[i].ToString());

            if (i < Count - 1)
                str.Append(", ");
        }
        return str.ToString();
    }
}

