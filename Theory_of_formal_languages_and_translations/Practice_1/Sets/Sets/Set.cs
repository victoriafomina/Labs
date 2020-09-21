using System;
using System.Collections;
using System.Collections.Generic;

namespace Sets
{
    /// <summary>
    /// Set is an abstract data type that can store unique values.
    /// </summary>
    public class Set<T> : ISet<T> where T : IComparable<T>
    {
        private Node root;
        private int count;

        private class Node
        {
            public Node(T item)
            {
                Item = item;
                Parent = null;
                LeftChild = null;
                RightChild = null;
            }

            public T Item { get; set; }

            public Node Parent { get; set; }

            public Node LeftChild { get; set; }

            public Node RightChild { get; set; }
        }

        /// <summary>
        /// Initializes object of the Set class.
        /// </summary>
        public Set()
        {
            root = null;
            count = 0;
        }

        /// <summary>
        /// Gets number of the elements in the Set.
        /// </summary>
        public int Count => count;

        /// <summary>
        /// Determines whether the Set containes a specific value.
        /// </summary>
        public bool Contains(T item)
        {
            if (root == null)
            {
                return false;
            }

            if (count == 0)
            {
                return false;
            }

            var currentNode = root;

            while (true)
            {
                if (currentNode.Item.CompareTo(item) > 0)
                {
                    if (currentNode.LeftChild == null)
                    {
                        return false;
                    }
                    currentNode = currentNode.LeftChild;
                }
                else if (currentNode.Item.CompareTo(item) < 0)
                {
                    if (currentNode.RightChild == null)
                    {
                        return false;
                    }
                    currentNode = currentNode.RightChild;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Adds an element to the current set and returns a value to indicate if the element was successfully added.
        /// </summary>
        /// <param name="item">Element to add.</param>
        /// <returns>True if the element was successfully added.</returns>
        public bool Add(T item)
        {
            if (root == null)
            {
                root = new Node(item);
            }
            else if (Contains(item))
            {
                return false;
            }
            else
            {
                AddRecursion(root, item);
            }

            ++count;

            return true;
        }

        /// <summary>
        /// Adds an element to the current set and returns a value to indicate if the element was successfully added.
        /// </summary>
        /// <param name="item">Element to add.</param>
        void ICollection<T>.Add(T item) => Add(item);

        private void AddRecursion(Node currentNode, T item)
        {
            if (currentNode.Item.CompareTo(item) > 0 && currentNode.LeftChild != null)
            {
                AddRecursion(currentNode.LeftChild, item);
            }
            else if (currentNode.Item.CompareTo(item) < 0 && currentNode.RightChild != null)
            {
                AddRecursion(currentNode.RightChild, item);
            }
            else
            {
                if (currentNode.Item.CompareTo(item) > 0)
                {
                    currentNode.LeftChild = new Node(item);
                    currentNode.LeftChild.Parent = currentNode;
                }
                else
                {
                    currentNode.RightChild = new Node(item);
                    currentNode.RightChild.Parent = currentNode;
                }
            }
        }

        /// <summary>
        /// Removes an element from the Set (if the element is in the set).
        /// </summary>
        public bool Remove(T item)
        {
            if (!Contains(item))
            {
                return false;
            }

            if (root.Item.Equals(item))
            {
                RemoveHead();
            }
            else
            {
                RemoveRecursion(root, item);
            }

            --count;

            return true;
        }

        private void RemoveHead()
        {
            if (count == 1)
            {
                root = null;
            }
            else if (root.LeftChild == null || root.RightChild == null)
            {
                if (root.LeftChild == null)
                {
                    root = root.RightChild;
                    root.Parent = null;
                }
                else
                {
                    root = root.LeftChild;
                    root.Parent = null;
                }
            }
            else
            {
                root.Item = MaximumInSubtree(root.LeftChild);
                RemoveRecursion(root.LeftChild, root.Item);
            }

        }

        private void RemoveRecursion(Node currentNode, T item)
        {
            if (currentNode.Item.CompareTo(item) > 0)
            {
                RemoveRecursion(currentNode.LeftChild, item);
            }
            else if (currentNode.Item.CompareTo(item) < 0)
            {
                RemoveRecursion(currentNode.RightChild, item);
            }
            else
            {
                if (currentNode.LeftChild == null || currentNode.RightChild == null)
                {
                    RemoveWhenChildIsNull(currentNode);
                }
                else
                {
                    currentNode.Item = MaximumInSubtree(currentNode.LeftChild);
                    RemoveRecursion(currentNode.LeftChild, currentNode.Item);
                }
            }
        }

        private T MaximumInSubtree(Node current)
        {
            if (current.RightChild != null)
            {
                return MaximumInSubtree(current.RightChild);
            }
            else
            {
                return current.Item;
            }
        }

        private void RemoveWhenChildIsNull(Node NodeToRemove)
        {
            if (NodeToRemove.LeftChild == null && NodeToRemove.RightChild == null)
            {
                if (NodeToRemove == NodeToRemove.Parent.LeftChild)
                {
                    NodeToRemove.Parent.LeftChild = null;
                }
                else
                {
                    NodeToRemove.Parent.RightChild = null;
                }
            }
            else if (NodeToRemove.LeftChild == null)
            {
                if (NodeToRemove == NodeToRemove.Parent.LeftChild)
                {
                    NodeToRemove.RightChild.Parent = NodeToRemove.Parent;
                    NodeToRemove.Parent.LeftChild = NodeToRemove.RightChild;
                }
                else
                {
                    NodeToRemove.RightChild.Parent = NodeToRemove.Parent;
                    NodeToRemove.Parent.RightChild = NodeToRemove.RightChild;
                }
            }
            else
            {
                if (NodeToRemove == NodeToRemove.Parent.LeftChild)
                {
                    NodeToRemove.LeftChild.Parent = NodeToRemove.Parent;
                    NodeToRemove.Parent.LeftChild = NodeToRemove.LeftChild;
                }
                else
                {
                    NodeToRemove.LeftChild.Parent = NodeToRemove.Parent;
                    NodeToRemove.Parent.RightChild = NodeToRemove.LeftChild;
                }
            }
        }

        /// <summary>
        /// Removes all items from the Set.
        /// </summary>
        public void Clear()
        {
            root = null;
            count = 0;
        }

        /// <summary>
        /// Getting a set item by index.
        /// </summary>
        public T this[int index]
        {
            get
            {
                if (index >= count || count == 0)
                {
                    throw new IndexOutOfRangeException("Invalid index!\n");
                }
                return TreeTraversal(index);
            }
        }

        private T TreeTraversal(int index)
        {
            var stackOfElements = new Stack<Node>();

            var currentNode = root;

            int currentIndex = -1;

            while (stackOfElements.Count > 0 || currentNode != null)
            {
                if (currentNode == null)
                {
                    currentNode = stackOfElements.Pop();
                    ++currentIndex;
                    if (currentIndex == index)
                    {
                        return currentNode.Item;
                    }
                    currentNode = currentNode.RightChild;
                }
                else
                {
                    stackOfElements.Push(currentNode);
                    currentNode = currentNode.LeftChild;
                }
            }

            return currentNode.Item;
        }

        /// <summary>
        /// Copies the elements of the Set to an Array, starting at a particular Array index.
        /// </summary>
        /// <param name="destination">The one-dimensional Array that is the destination of the elements copied from Set. 
        /// The Array must have zero-based indexing.</param>
        /// <param name="copyingStartsFrom">The zero-based index in array at which copying begins.</param>
        /// <exception cref="EmptySetException">Thrown when the Set is empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the index at which copying starts is 
        /// invalid.</exception>
        /// <exception cref="ArgumentException">Thrown when the number of the elements in the list is greater than the
        /// available space in the array.</exception>
        public void CopyTo(T[] destination, int copyingStartsFrom)
        {
            if (copyingStartsFrom < 0 || copyingStartsFrom >= count)
            {
                throw new ArgumentOutOfRangeException($"Invalid index \"copyingStartsFrom\" {copyingStartsFrom}!!!\n");
            }

            if (root == null)
            {
                throw new EmptySetException("The set is empty!!! Invalid operation.\n");
            }

            if (destination.Length < count - copyingStartsFrom)
            {
                throw new ArgumentException("The number of elements in the Set is greater than the available space " +
                        "from the index to the end of destination array\n");
            }

            int index = copyingStartsFrom;

            foreach (var element in this)
            {
                destination[index] = element;
                ++index;
            }
        }

        /// <returns>an enumerator that iterates through a collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <returns>an enumerator that iterates through a collection.</returns>
        public IEnumerator<T> GetEnumerator() => new Iterator(this);

        /// <summary>
        /// Supports a simple iteration over a generic collection.
        /// </summary>
        private class Iterator : IEnumerator<T>
        {
            private Set<T> tree;
            private int currentIndex;
            private List<T> elements;

            /// <summary>
            /// Initializes an object of the class Iterator.
            /// </summary>
            public Iterator(Set<T> tree)
            {
                this.tree = tree;
                currentIndex = -1;
                elements = new List<T>();
                FillListWithTheElements();
            }

            private void FillListWithTheElements()
            {
                if (tree.Count > 0)
                {
                    FillQueueWithTheElementsRecursion(tree.root);
                }
            }

            private void FillQueueWithTheElementsRecursion(Node current)
            {
                elements.Add(current.Item);

                if (current.LeftChild != null)
                {
                    FillQueueWithTheElementsRecursion(current.LeftChild);
                }

                if (current.RightChild != null)
                {
                    FillQueueWithTheElementsRecursion(current.RightChild);
                }
            }
            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            public T Current
            {
                get
                {
                    if (currentIndex < 0 || currentIndex > tree.Count)
                    {
                        throw new IndexOutOfRangeException("Index is out of range!\n");
                    }

                    return elements[currentIndex];
                }
            }

            object IEnumerator.Current => Current;

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            public bool MoveNext()
            {
                if (currentIndex < tree.Count)
                {
                    ++currentIndex;
                }

                return currentIndex < tree.Count;
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset() => currentIndex = -1;

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose() { }
        }

        /// <summary>
        /// Removes all elements in the specified collection from the current set.
        /// </summary>
        /// <param name="other">The collection of items to remove from the set.</param>
        public void ExceptWith(IEnumerable<T> other)
        {
            if (this == other)
            {
                throw new ArgumentException("The argument is the current object!\n");
            }

            foreach (var element in other)
            {
                Remove(element);
            }
        }

        /// <summary>
        /// Modifies the current set so that it contains only elements that are also in a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        public void IntersectWith(IEnumerable<T> other)
        {
            var intersectionSet = new Set<T>();

            foreach (var element in other)
            {
                if (Contains(element))
                {
                    intersectionSet.Add(element);
                }
            }

            Clear();

            foreach (var element in intersectionSet)
            {
                Add(element);
            }
        }

        /// <summary>
        /// Determines whether the current set is a proper (strict) subset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>true if the current set is a proper subset of other; otherwise, false.</returns>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (!IsSubsetOf(other))
            {
                return false;
            }

            return NumberOfElementsInCollection(other) != count;
        }

        /// <summary>
        /// Determines whether the current set is a superset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>true if the current set is a superset of other; otherwise, false.</returns>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (!IsSupersetOf(other))
            {
                return false;
            }

            return NumberOfElementsInCollection(other) != count;
        }

        private int NumberOfElementsInCollection(IEnumerable<T> other)
        {
            int countElementsInOther = 0;

            foreach (var element in other)
            {
                ++countElementsInOther;
            }

            return countElementsInOther;
        }

        /// <summary>
        /// Determines whether the current set is a subset of a specified other.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>true if the current set is a subset of other; otherwise, false.</returns>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            int countEqualElements = 0;

            foreach (var element in other)
            {
                if (Contains(element))
                {
                    ++countEqualElements;
                }
            }

            return count == countEqualElements;
        }

        /// <summary>
        /// Determines whether the current set is a superset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>true if the current set is a superset of other; otherwise, false.</returns>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            int countEqualElementsInOther = 0;
            int numberOfElementsInCollection = 0;

            foreach (var element in other)
            {
                if (Contains(element))
                {
                    ++countEqualElementsInOther;
                }
                ++numberOfElementsInCollection;
            }

            return countEqualElementsInOther == numberOfElementsInCollection;
        }

        /// <summary>
        /// Determines whether the current set overlaps with the specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>true if the current set and other share at least one common element; otherwise, false.</returns>
        public bool Overlaps(IEnumerable<T> other)
        {
            if (count == 0)
            {
                return false;
            }

            foreach (var element in other)
            {
                if (Contains(element))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether the current set and the specified other contain the same elements.
        /// </summary>
        /// <param name="other">The other to compare to the current set.</param>
        /// <returns>true if the current set is equal to other; otherwise, false.</returns>
        public bool SetEquals(IEnumerable<T> other)
        {
            int numberOfElementsInCollection = 0;

            foreach (var element in other)
            {
                if (!Contains(element))
                {
                    return false;
                }
                ++numberOfElementsInCollection;
            }

            return count == numberOfElementsInCollection;
        }

        /// <summary>
        /// Modifies the current set so that it contains only elements that are present either in the current 
        /// set or in the specified collection, but not both.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            foreach (var element in other)
            {
                if (Contains(element))
                {
                    Remove(element);
                }
                else
                {
                    Add(element);
                }
            }
        }

        /// <summary>
        /// Modifies the current set so that it contains all elements that are present in the current set, 
        /// in the specified collection, or in both.
        /// </summary>
        /// <param name="other">The collection to union with.</param>
        public void UnionWith(IEnumerable<T> other)
        {
            foreach (var element in other)
            {
                Add(element);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the Set is read-only.
        /// </summary>
        public bool IsReadOnly => false;
    }
}
