using System.Reflection.Metadata;

namespace Tuto
{
  public class Vector<T>
  {
    private const uint DefaultCapacity = 8;
    T[] _arr;
    uint _length = 0;
    public uint Length { get { return _length; } }
    uint _capacity = DefaultCapacity;
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
      var i = IndexOf(item);
      if (i != null)
      {
        RemoveAt(i.Value);
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

    public uint? IndexOf(T item)
    {
      for (uint i = 0; i <= _length; ++i)
      {
        if (EqualityComparer<T>.Default.Equals(_arr[i], item))
        {
          return i;
        }
      }
      return null;
    }
  }
}