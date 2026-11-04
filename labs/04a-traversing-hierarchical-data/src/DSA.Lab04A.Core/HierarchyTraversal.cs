using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA.Lab04A
{
    /// <summary>
    /// Recursive algorithms over a rooted hierarchy.
    ///
    /// The key idea is structural recursion:
    /// solve the current node, then apply the same operation to each child subtree.
    /// </summary>
    public static class HierarchyTraversal
    {
        public static int CountNodes<T>(HierarchyNode<T> root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            int childrenCount = root.Children.Sum(child => CountNodes(child));

            // TODO: count this node, then count every child subtree.
            return 1 + childrenCount;
        }

        public static bool Contains<T>(HierarchyNode<T> root, T target)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }
            if (root.Value.Equals(target))
            {
                return true;
            }

            foreach (var child in root.Children)
            {
                if (Contains(child, target))
                {
                    return true;
                }
            }
            return false;
        }

        public static IReadOnlyList<T> DepthFirstPreOrder<T>(HierarchyNode<T> root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }
            List<T> order = new List<T>();
            order.Add(root.Value);// added first node
            foreach (var child in root.Children)
            {
                IReadOnlyList<T> childValues = DepthFirstPreOrder(child);
                for (int i = 0; i < childValues.Count; i++)
                {
                    order.Add(childValues[i]); // added each of the children's values
                }
            }
            return order;

            // TODO: return values in pre-order:
            // current node first, then each child subtree from left to right.
        }

        public static int MaxDepth<T>(HierarchyNode<T> root)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            int maxDepth = 0;
            if (root.Children.Count > 0)
            {
                foreach (var child in root.Children)
                {
                    int depth = MaxDepth(child);
                    if (depth > maxDepth)
                    {
                        maxDepth = depth;
                    }
                }
                maxDepth = maxDepth + 1;
            }
            return maxDepth;

            // Depth is measured in edges from the root.
            // A single-node tree therefore has max depth 0.
        }
    }
}