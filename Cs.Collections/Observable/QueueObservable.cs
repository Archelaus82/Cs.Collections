using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.Observable
{
    public class QueueObservable<T>
    {
        public event EventHandler EnqueuedEventHandler;
        public event EventHandler DequeuedEventHandler;

        private readonly Queue<T> _queue = new Queue<T>();
        private int _size = 1024;
        private bool _autoResize = false;
        private bool _closing = false;

        public QueueObservable() { }

        public QueueObservable(int maxSize, bool autoResize)
        {
            this._size = maxSize;
            this._autoResize = autoResize;
        }

        public bool IsEmpty { get { return _queue.Count > 0 ? false : true; } }
        public int Count { get { return _queue.Count; } }
        public int Size { get { return _size; } }

        private void OnEnqueue()
        {
            if (EnqueuedEventHandler != null)
                EnqueuedEventHandler(this, EventArgs.Empty);
        }

        private void OnDequeue()
        {
            if (DequeuedEventHandler != null)
                DequeuedEventHandler(this, EventArgs.Empty);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _queue.GetEnumerator();
        }

        public T[] ToArray() 
        { 
            return _queue.ToArray(); 
        }

        public bool Enqueue(T item)
        {
            lock (_queue)
            {
                if (_closing) return false;
                if (_queue.Count == _size)
                    if (_autoResize) _size = _size * 2;
                    else return false;
                _queue.Enqueue(item);
                OnEnqueue();
                return true;
            }
        }

        public bool TryDequeue(out T item)
        {
            item = default(T);
            lock (_queue)
            {
                if (_queue.Count == 0)
                    return false;
                item = _queue.Dequeue();
                OnDequeue();
                return true;
            }
        }

        public bool TryPeek(out T item)
        {
            item = default(T);
            lock (_queue)
            {
                if (_queue.Count == 0)
                    return false;
                item = _queue.Peek();
                return true;
            }
        }

        public void Close()
        {
            lock (_queue)
            {
                _closing = true;
            }
        }

        public void Open()
        {
            lock (_queue)
            {
                _closing = false;
            }
        }

        public void Clear()
        {
            lock (_queue)
            {
                _queue.Clear();
            }
        }
    }
}
