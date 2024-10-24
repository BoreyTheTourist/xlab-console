using System.Data;
using System.Data.SqlTypes;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Timers;
using Sort;
using Monad;

namespace Tuto
{
  public class Vector<T> : ISortable<T>
  {
    private const uint DefaultCapacity = 8;
    T[] _arr;
    uint _length = 0;
    uint _capacity = DefaultCapacity;
    public ISort<T> Sorter = new QuickSort<T>();
    public uint Length { get { return _length; } }
    public uint Capacity
    {
      get => _capacity;
      set
      {
        if (value < _length)
        {
          throw new IndexOutOfRangeException($"Tried to set capacity {value} while length was {_length}");
        } else if (value == _length)
        {
          return;
        }

        _capacity = value;
        var narr = new T[_capacity];
        for (uint i = 0; i < _arr.Length; ++i)
        {
          narr[i] = _arr[i];
        }
        _arr = narr;
      }
    }

    public T this[uint idx]
    {
      get
      {
        if (idx >= _length)
        {
          throw new IndexOutOfRangeException($"Index was {idx} while length was {_length}");
        }
        return _arr[idx];
      }

      set
      {
        if (idx >= _length)
        {
          throw new IndexOutOfRangeException($"Index was {idx} while length was {_length}");
        }
        _arr[idx] = value;
      }
    }

    public Vector()
    {
      _arr = new T[_capacity];
    }

    public Vector(uint capacity)
    {
      _capacity = capacity;
      _arr = new T[capacity];
    }

    public void Add(T item)
    {
      if (_length == _capacity)
      {
        Capacity <<= 1;
      }
      _arr[_length++] = item;
    }

    public void RemoveAt(uint idx)
    {
      if (idx >= _length)
      {
        throw new IndexOutOfRangeException($"Index was {idx} while length was {_length}");
      }
      
      _length--;
      for (uint i = idx; i < _length; ++i)
      {
        _arr[i] = _arr[i + 1];
      }
    }

    public void Remove(T item)
    {
      uint i;
      if (IndexOf(item).IsSome(out i))
      {
        RemoveAt(i);
      }
    }

    public void Insert(uint idx, T item)
    {
      if (idx > _length)
      {
        throw new IndexOutOfRangeException($"Index was {idx} while length was {_length}");
      }
      else if (idx == _length)
      {
        Add(item);
        return;
      }

      // initial _length = 0, so first insert at 0 is handled by prev branch
      Add(_arr[_length - 1]);
      for (uint i = _length - 1; i > idx; --i)
      {
        _arr[i] = _arr[i - 1];
      }
      _arr[idx] = item;
    }

    public void Clear()
    {
      _length = 0;
      _capacity = DefaultCapacity;
      _arr = new T[DefaultCapacity];
    }

    public override string ToString()
    {
      var res = new String("[");
      for (uint i = 0; i < _length - 1; ++i)
      {
        res += _arr[i] + ", ";
      }
      if (_length > 0)
      {
        res += _arr[_length - 1];
      }
      return res + "]";
    }

    public Option<uint> IndexOf(T item)
    {
      for (uint i = 0; i <= _length; ++i)
      {
        if (EqualityComparer<T>.Default.Equals(_arr[i], item))
        {
          return Option<uint>.Some(i);
        }
      }
      return Option<uint>.None;
    }

    public void SortBy(Compare<T> compare)
    {
      Sorter.Sort(this, compare);
    }

    public void ForEach(Action<T> action)
    {
      for (uint i = 0; i < _length; ++i)
      {
        action(_arr[i]);
      }
    }

    public Option<T> Find(Func<T, bool> predicate)
    {
      for (uint i = 0; i < _length; ++i)
      {
        if (predicate(_arr[i])) return Option<T>.Some(_arr[i]);
      }
      return Option<T>.None;
    }
  }
}