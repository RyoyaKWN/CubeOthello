using System;
using System.Collections;
using System.Collections.Generic;

// コレクションのインデックスを循環させる
public class CircArray<T> : IEnumerable<T>
{
    private T[] array;
    public int size;

    public CircArray(T[] elements)
    {
        size = elements.Length;
        array = new T[size];
        Array.Copy(elements, array, size);
    }

    public T this[int index]
    {
        get { return array[GetCircularIndex(index)]; }
        set { array[GetCircularIndex(index)] = value; }
    }

    private int GetCircularIndex(int index)
    {
        if (index < 0)
        {
            // 負のインデックスの場合、正の範囲に変換する
            index = size + (index % size);
        }
        else if (index >= size)
        {
            // インデックスがサイズ以上の場合、循環して最初の要素に戻る
            index %= size;
        }
        return index;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new CircularArrayEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private class CircularArrayEnumerator : IEnumerator<T>
    {
        private readonly CircArray<T> circularArray;
        private int currentIndex;

        public CircularArrayEnumerator(CircArray<T> array)
        {
            circularArray = array;
            currentIndex = -1;
        }

        public T Current => circularArray[currentIndex];

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            currentIndex++;
            return currentIndex < circularArray.size;
        }

        public void Reset()
        {
            currentIndex = -1;
        }

        public void Dispose()
        {
        }
    }
}