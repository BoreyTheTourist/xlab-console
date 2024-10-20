using System.Reflection.Metadata;

namespace Tuto
{
  public class Vector<T>
  {
    uint _d = 10;
    T[] _arr = new T[] { };
    uint _length = 0;
    public uint Length { get { return _length; } }

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

    public void Add(T item)
    {
      if (_length == (uint)_arr.Length)
      {
        var narr = new T[_length + _d];
        for (uint i = 0; i < _length; ++i)
        {
          narr[i] = _arr[i];
        }
        _arr = narr;
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
      for (uint i = 0; i < _length; ++i)
      {
        if (EqualityComparer<T>.Default.Equals(_arr[i], item))
        {
          RemoveAt(i);
          return;
        }
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
  }
}