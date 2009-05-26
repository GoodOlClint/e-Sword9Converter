using System;
using System.Collections.Generic;
using System.Text;

namespace eSword9Converter
{
    public class ThreadSafeDictionary<Key, Value> : IDictionary<Key, Value>, IDisposable, ICloneable
    {
        private static object threadLock = new object();
        private IDictionary<Key, Value> Dictionary = new Dictionary<Key, Value>();
        private bool disposed = false;

        #region IDictionary<Key,Value> Members

        public void Add(Key key, Value value)
        {
            if (!disposed)
            {
                lock (threadLock)
                {
                    KeyValuePair<Key, Value> keyValue = new KeyValuePair<Key, Value>(key, value);
                    this.Dictionary.Add(keyValue);
                }
            }
            else
            {
                throw new ObjectDisposedException(this.GetType().ToString());
            }


        }

        public bool ContainsKey(Key key)
        {
            if (!disposed)
            {
                lock (threadLock)
                {
                    return this.Dictionary.ContainsKey(key);
                }
            }
            else
            {
                throw new ObjectDisposedException(this.GetType().ToString());
            }

        }

        public ICollection<Key> Keys
        {
            get
            {
                if (!disposed)
                {
                    lock (threadLock)
                    {
                        return this.Dictionary.Keys;
                    }
                }
                else
                {
                    throw new ObjectDisposedException(this.GetType().ToString());
                }
            }
        }

        public bool Remove(Key key)
        {
            if (!disposed)
            {
                lock (threadLock)
                {
                    return this.Dictionary.Remove(key);
                }
            }
            else
            {
                throw new ObjectDisposedException(this.GetType().ToString());
            }
        }

        public bool TryGetValue(Key key, out Value value)
        {
            if (!disposed)
            {
                lock (threadLock)
                {
                    return this.Dictionary.TryGetValue(key, out value);
                }
            }
            else
            {
                throw new ObjectDisposedException(this.GetType().ToString());
            }
        }

        public ICollection<Value> Values
        {
            get
            {
                if (!disposed)
                {
                    lock (threadLock)
                    {
                        return this.Dictionary.Values;
                    }
                }
                else
                {
                    throw new ObjectDisposedException(this.GetType().ToString());
                }
            }
        }

        public Value this[Key key]
        {
            get
            {
                if (!disposed)
                {
                    lock (threadLock)
                    {
                        return this.Dictionary[key];
                    }
                }
                else
                {
                    throw new ObjectDisposedException(this.GetType().ToString());
                }
            }
            set
            {
                if (!disposed)
                {
                    lock (threadLock)
                    {
                        this.Dictionary[key] = value;
                    }
                }
                else
                {
                    throw new ObjectDisposedException(this.GetType().ToString());
                }
            }
        }

        #endregion

        #region ICollection<KeyValuePair<Key,Value>> Members

        public void Add(KeyValuePair<Key, Value> item)
        {
            if (!disposed)
            {
                lock (threadLock)
                {
                    this.Dictionary.Add(item);
                }
            }
            else
            {
                throw new ObjectDisposedException(this.GetType().ToString());
            }
        }

        public void Clear()
        {
            if (!disposed)
            {
                lock (threadLock)
                {
                    this.Dictionary.Clear();
                }
            }
            else
            {
                throw new ObjectDisposedException(this.GetType().ToString());
            }
        }

        public bool Contains(KeyValuePair<Key, Value> item)
        {
            if (!disposed)
            {
                lock (threadLock)
                {
                    return this.Dictionary.Contains(item);
                }
            }
            else
            {
                throw new ObjectDisposedException(this.GetType().ToString());
            }
        }

        public void CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex)
        {
            if (!disposed)
            {
                lock (threadLock)
                {
                    this.Dictionary.CopyTo(array, arrayIndex);
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
                        return this.Dictionary.Count;
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
                    return this.Dictionary.IsReadOnly;
                }
                else
                {
                    throw new ObjectDisposedException(this.GetType().ToString());
                }
            }
        }

        public bool Remove(KeyValuePair<Key, Value> item)
        {
            if (!disposed)
            {
                lock (threadLock)
                {
                    return this.Dictionary.Remove(item);
                }
            }
            else
            {
                throw new ObjectDisposedException(this.GetType().ToString());
            }
        }

        #endregion

        #region IEnumerable<KeyValuePair<Key,Value>> Members

        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            if (!disposed)
            {
                return this.Dictionary.GetEnumerator();
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
                return this.Dictionary.GetEnumerator();
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
            if (!disposed)
            {
                this.Clear();
                this.disposed = true;
            }
            else
            {
                throw new ObjectDisposedException(this.GetType().ToString());
            }
        }

        #endregion

        #region ICloneable Members

        public object Clone() { return this.Clone(false); }

        public object Clone(bool clear)
        {
            ThreadSafeDictionary<Key, Value> col = new ThreadSafeDictionary<Key, Value>();
            lock (threadLock)
            {
                foreach (KeyValuePair<Key, Value> Item in this.Dictionary)
                {
                    if ((Item.Value != null)) col.Add(Item);
                }
                if (clear) this.Dictionary.Clear();
                return (object)col;
            }
        }

        #endregion
    }
}