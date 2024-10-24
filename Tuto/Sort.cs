using System.ComponentModel.DataAnnotations;

namespace Sort
{
  public delegate Ordering Compare<T>(T a, T b);

  public enum Ordering
  {
    Less,
    Greater,
    Equal,
  }

  public interface ISort<T>
  {
    void Sort(ISortable<T> col, Compare<T> cmp);
  }

  public interface ISortable<T>
  {
    T this[uint idx] { get; set; }
    uint Length { get; }
  }

  public class QuickSort<T> : ISort<T>
  {
    public void Sort(ISortable<T> col, Compare<T> cmp)
    {
      if (col.Length == 0) return;
      quickSort(col, 0, col.Length - 1, cmp);
    }

    private static void quickSort(ISortable<T> col, uint low, uint high, Compare<T> cmp)
    {
      if (low < high)
      {
        T pivot = col[low];
        uint l = low - 1;
        uint h = high + 1;
        for (;;)
        {
          do { h--; } while (cmp(col[h], pivot) == Ordering.Greater);
          do { l++; } while (cmp(col[l], pivot) == Ordering.Less);
          if (l >= h) break;

          (col[l], col[h]) = (col[h], col[l]);
        }

        quickSort(col, low, h, cmp);
        quickSort(col, h + 1, high, cmp);
      }
    }
  }
}