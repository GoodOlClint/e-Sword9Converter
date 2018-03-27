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

        public void FromArray(T[] array)
        {
            foreach (T item in array)
<<<<<<< HEAD
            {
                this.Add(item);
            }
=======
            { this.Add(item); }
>>>>>>> Test
        }
    }
}