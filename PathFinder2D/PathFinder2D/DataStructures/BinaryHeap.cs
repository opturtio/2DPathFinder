namespace PathFinder2D.DataStructures
{
    /// <summary>
    /// Represents a binary heap data structure.
    /// </summary>
    /// <typeparam name="T">The type of elements in the heap, which must implement IComparable&lt;T&gt;.</typeparam>
    public class BinaryHeap<T> where T : IComparable<T>
    {
        private List<T> heap;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryHeap{T}"/> class.
        /// </summary>
        public BinaryHeap()
        {
            this.heap = new List<T>();
        }

        /// <summary>
        /// Inserts an item into the heap.
        /// </summary>
        /// <param name="item">The item to insert.</param>
        public void Insert(T item)
        {
            this.heap.Add(item);
            int childIndex = this.heap.Count - 1;

            while (childIndex > 0)
            {
                int parentIndex = (childIndex - 1) / 2;
                if (this.heap[childIndex].CompareTo(this.heap[parentIndex]) >= 0)
                {
                    break;
                }

                T temp = this.heap[childIndex];
                this.heap[childIndex] = this.heap[parentIndex];
                this.heap[parentIndex] = temp;
                childIndex = parentIndex;
            }
        }

        /// <summary>
        /// Extracts the minimum item from the heap.
        /// </summary>
        /// <returns>The minimum item.</returns>
        public T ExtractMin()
        {
            int lastIndex = this.heap.Count - 1;
            T frontItem = this.heap[0];
            this.heap[0] = this.heap[lastIndex];
            this.heap.RemoveAt(lastIndex);

            --lastIndex;
            int parentIndex = 0;

            while (true)
            {
                int leftChildIndex = parentIndex * 2 + 1;
                if (leftChildIndex > lastIndex)
                {
                    break;
                }

                int rightChildIndex = leftChildIndex + 1;
                if (rightChildIndex <= lastIndex && this.heap[rightChildIndex].CompareTo(this.heap[leftChildIndex]) < 0)
                {
                    leftChildIndex = rightChildIndex;
                }

                if (this.heap[parentIndex].CompareTo(this.heap[leftChildIndex]) <= 0)
                {
                    break;
                }

                T temp = this.heap[parentIndex];
                this.heap[parentIndex] = this.heap[leftChildIndex];
                this.heap[leftChildIndex] = temp;
                parentIndex = leftChildIndex;
            }

            return frontItem;
        }

        /// <summary>
        /// Gets the number of items in the heap.
        /// </summary>
        public int Count => this.heap.Count;

        public bool Contains(T item)
        {
            return this.heap.Contains(item);
        }
    }
}