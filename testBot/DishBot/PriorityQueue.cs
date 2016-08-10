using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Web;
using testBot.Bean;

namespace testBot.DishBot
{
    public class PriorityQueue<T> : ISerializable where T : IComparable
    {

        T[] heap;

        public int Count { get; private set; }

        public PriorityQueue(SerializationInfo info, StreamingContext context)
        {

            Count = (int)info.GetValue("Count", typeof(int));
            heap = (T[])info.GetValue("heap", typeof(T[]));
            //t2 = (Question)info.GetValue("test", typeof(Question));
        }
        public PriorityQueue() : this(16) { }



        public PriorityQueue(int capacity)
        {
            this.heap = new T[capacity];
        }

        public void Push(T v)
        {
            if (Count >= heap.Length) Array.Resize(ref heap, Count * 2);
            heap[Count] = v;
            SiftUp(Count++);
        }

        public T Pop()
        {
            var v = Top();
            heap[0] = heap[--Count];
            if (Count > 0) SiftDown(0);
            return v;
        }

        public T Top()
        {
            if (Count > 0) return heap[0];
            return default(T);
        }

        void SiftUp(int n)
        {
            var v = heap[n];
            for (var n2 = n / 2; n > 0 && v.CompareTo(heap[n2]) < 0; n = n2, n2 /= 2) heap[n] = heap[n2];
            heap[n] = v;
        }


        void SiftDown(int n)
        {
            var v = heap[n];
            for (var n2 = n * 2; n2 < Count; n = n2, n2 *= 2)
            {
                if (n2 + 1 < Count && heap[n2 + 1].CompareTo(heap[n2]) < 0) n2++;
                if (v.CompareTo(heap[n2]) <= 0) break;
                heap[n] = heap[n2];
            }
            heap[n] = v;
        }


        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Count", Count, typeof(int));
            info.AddValue("heap", heap, typeof(T[]));
            //info.AddValue("test", t2, typeof(Question));
        }

    }

}