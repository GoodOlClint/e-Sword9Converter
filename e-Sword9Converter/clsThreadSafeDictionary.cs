/*
 * Copyright (c) 2009, GoodOlClint All rights reserved.
 * Redistribution and use in source and binary forms, with or without modification, are permitted
 * provided that the following conditions are met:
 * Redistributions of source code must retain the above copyright notice, this list of conditions
 * and the following disclaimer.
 * Redistributions in binary form must reproduce the above copyright notice, this list of conditions
 * and the following disclaimer in the documentation and/or other materials provided with the distribution.
 * Neither the name of the e-Sword Users nor the names of its contributors may be used to endorse
 * or promote products derived from this software without specific prior written permission.
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS
 * OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
 * AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
 * IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
 * OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

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