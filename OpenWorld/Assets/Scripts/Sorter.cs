public abstract class Sorter<T>
{
    protected abstract int Compare(T x, T y);
    public virtual void QuickSort(T[] data, int left, int right)
    {
        int i = left, j = right;
        T pivot = data[(left + right) / 2];
        while (i <= j)
        {
            while (Compare(data[i], pivot) < 0) i++;
            while (Compare(data[j], pivot) > 0) j--;
            if (i <= j)
            {
                T temp = data[i];
                data[i] = data[j];
                data[j] = temp;
                i++; j--;
            }
        }
        if (left < j) QuickSort(data, left, j);
        if (i < right) QuickSort(data, i, right);
    }
}