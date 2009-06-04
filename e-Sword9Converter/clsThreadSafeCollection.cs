using System;
using System.Collections.Generic;
using System.Text;

namespace eSword9Converter
{
    public class ThreadSafeCollection<T> : ICollection<T>, IDisposable, ICloneable
    {
        private static object threadLock = new object();
        private IList<T> List = new List<T>();
        private bool disposed = false;

        #region Indexer
        public T this[int index]
        {
            get
            {
                if (disposed)
                { throw new ObjectDisposedException(this.GetType().ToString()); }
                lock (threadLock)
                { return this.List[index]; }
            }
            set
            {
                if (disposed)
                { throw new ObjectDisposedException(this.GetType().ToString()); }
                lock (threadLock)
                { this.List[index] = value; }
            }
        }
        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            if (disposed)
            { throw new ObjectDisposedException(this.GetType().ToString()); }
            lock (threadLock)
            { this.List.Add(item); }

        }

        public void Clear()
        {
            if (disposed)
            { throw new ObjectDisposedException(this.GetType().ToString()); }
            lock (threadLock)
            { this.List.Clear(); }

        }

        public bool Contains(T item)
        {
            if (disposed)
            { throw new ObjectDisposedException(this.GetType().ToString()); }
            lock (threadLock)
            { return this.List.Contains(item); }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (!disposed)
            {
                lock (threadLock)
                {
                    this.List.CopyTo(array, arrayIndex);
                }
            }
            else
            {
                throw new ObjectDisposedException(this.GetType().ToString());
            }
        }

        public int Count
        {
            get
            {
                if (!disposed)
                {
                    lock (threadLock)
                    {
                        return this.List.Count;
                    }
                }
                else
                {
                    throw new ObjectDisposedException(this.GetType().ToString());
                }
            }
        }

        public bool IsReadOnly
        {
            get
            {
                if (!disposed)
                {
                    return this.List.IsReadOnly;
                }
                else
                {
                    throw new ObjectDisposedException(this.GetType().ToString());
                }
            }
        }

        public bool Remove(T item)
        {
            if (!disposed)
            {
                lock (threadLock)
                {
                    return this.List.Remove(item);
                }
            }
            else
            {
                throw new ObjectDisposedException(this.GetType().ToString());
            }
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            if (!disposed)
            {
                return this.List.GetEnumerator();
            }
            else
            {
                throw new ObjectDisposedException(this.GetType().ToString());
            }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            if (!disposed)
            {
                return this.List.GetEnumerator();
            }
            else
            {
                throw new ObjectDisposedException(this.GetType().ToString());
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (disposed)
            { throw new ObjectDisposedException(this.GetType().ToString()); }
            foreach (T item in this.List)
            {
                if (item.GetType() == typeof(IDisposable))
                { ((IDisposable)item).Dispose(); }
            }
            this.Clear();
            this.disposed = true;
        }

        #endregion

        #region ICloneable Members

        public object Clone() { return this.Clone(false); }

        public object Clone(bool clear)
        {
            ThreadSafeCollection<T> col = new ThreadSafeCollection<T>();
            lock (threadLock)
            {
                foreach (T item in this.List)
                {
                    if (item.GetType() == typeof(ICloneable))
                    { col.Add((T)((ICloneable)item).Clone()); }
                    else { if ((item != null)) col.Add(item); }
                }
                if (clear) this.List.Clear();
                return (object)col;
            }
        }

        public object Clone(int Offset, int length)
        {
            ThreadSafeCollection<T> col = new ThreadSafeCollection<T>();
            lock (threadLock)
            {
                for (int i = 0; i <= length; i++)
                {
                    col.Add(this[i + Offset]);
                }
            }
            return col;
        }

        #endregion
    }
}