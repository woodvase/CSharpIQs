/*
 * Created by SharpDevelop.
 * User: yoli
 * Date: 4/19/2015
 * Time: 3:17 PM
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewQs
{
    class Program
    {
        public static void Main(string[] args)
        {
            // TODO: Implement Functionality Here
            FactorCombination(12);
            Console.WriteLine(FibonacciSequenceII(16));
            CombinationSum3(3, 9);
            Console.WriteLine(FindOrder(2, new int[,] { { 1, 0 }, { 0, 1 } }));
            Console.WriteLine(CanFinish(2, new int[,] { { 1, 0 }, { 0, 1 } }));
            Console.WriteLine(FindKthLargest(new int[] { 7, 6, 5, 4, 3, 2, 1 }, 5));

            Trie trie = new Trie();
            trie.Insert("dogg");
            trie.Insert("deot");
            trie.Insert("eat");
            trie.Insert("de");
            Console.WriteLine(trie.Search("dog"));
            Console.WriteLine(trie.StartsWith("dog"));

            string[] strs = new string[] {
                "Don't",
                "go",
                "around",
                "saying",
                "the",
                "world",
                "owes",
                "you",
                "a",
                "living;",
                "the"
            };
            List<string> words = new List<string>(strs);
            FullJustify(words, 30);
            MinWindow("ADOBECODEBANC", "ABC");
            PrintSet(4);
            //ContainsNearbyAlmostDuplicate(new int[]{7,1,3}, 2, 3);
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }

        // https://leetcode.com/problems/maximum-subarray/
        public int MaxSubArray(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return Int32.MinValue;
            int tmp = nums[0];
            int max = tmp;
            for (int i = 1; i < nums.Length; i++)
            {
                int curMax = Math.Max(nums[i], nums[i] + tmp);
                if (curMax > max)
                    max = curMax;
                tmp = curMax;
            }

            return max;
        }

        //Print all unique combination of factors(except 1) of a given number.
        //For example:
        //Input: 12
        //Output: [[2, 2, 3], [2, 6], [3, 4]]
        //Input: 15
        //Output: [[3, 5]]
        //Input: 28
        //Output: [[2, 2, 7], [2, 14], [4, 7]]
        static public List<List<int>> FactorCombination(int num)
        {
            List<List<int>> ret = new List<List<int>>();
            if (num < 2)
                return ret;
            List<int> cur = new List<int>();
            FactorCombinationHelper(num, ret, cur);
            return ret;
        }

        static public void FactorCombinationHelper(int num, List<List<int>> combinations, List<int> combination)
        {
            for (int i = combination.Count == 0 ? 2 : combination[combination.Count - 1]; i < num; i++)
            {
                if (num % i == 0)
                {
                    combination.Add(i);
                    FactorCombinationHelper(num / i, combinations, combination);
                    combination.RemoveAt(combination.Count - 1);
                }
            }
            if (combination.Count > 0 && num > combination[combination.Count - 1])
            {
                combination.Add(num);
                combinations.Add(new List<int>(combination));
                combination.RemoveAt(combination.Count - 1);
            }
        }
        public static int FibonacciSequenceII(int n)
        {
            if (n == 0 || n == 1) return n;
            return FibonacciSequenceII(n - 1) + FibonacciSequenceII(n - 2);
        }

        static public int FibonacciSequenceI(int n)
        {
            if (n == 0 || n == 1) return 0;
            int pre2 = 0;
            int pre1 = 1;
            int i = 2;
            while (i <= n)
            {
                int fi = pre1 + pre2;
                pre2 = pre1;
                pre1 = fi;
                i++;
            }

            return pre1;
        }

        // https://leetcode.com/problems/combination-sum-iii/
        static public IList<IList<int>> CombinationSum3(int k, int n)
        {
            List<IList<int>> ret = new List<IList<int>>();
            if (k <= 0 || k > 9)
                return ret;
            IList<int> cur = new List<int>();
            CombinationSum3Helper(ret, cur, 1, k, n);
            return ret;
        }

        static public void CombinationSum3Helper(IList<IList<int>> lists, IList<int> current, int start, int k, int n)
        {
            if (k == 1)
            {
                if (n >= start && n <= 9)
                {
                    current.Add(n);
                    lists.Add(new List<int>(current));
                    current.RemoveAt(current.Count - 1);
                }
            }
            else
            {
                for (int i = start; i <= 9 - k + 1; i++)
                {
                    current.Add(i);
                    CombinationSum3Helper(lists, current, i + 1, k - 1, n - i);
                    current.RemoveAt(current.Count - 1);
                }
            }
        }

        // https://leetcode.com/problems/search-a-2d-matrix-ii/
        public bool SearchMatrixII(int[,] matrix, int target)
        {
            if (matrix == null)
                return false;
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);
            if (target < matrix[0, 0] || target > matrix[m - 1, n - 1])
                return false;
            int row = 0;
            int col = n - 1;
            while (row < m && col >= 0)
            {
                if (target == matrix[row, col])
                    return true;
                if (target > matrix[row, col])
                {
                    row++;
                }
                else
                {
                    col--;
                }
            }

            return false;
        }

        // https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree/
        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null || root == p || root == q)
                return root;
            TreeNode left = LowestCommonAncestor(root.left, p, q);
            TreeNode right = LowestCommonAncestor(root.right, p, q);
            if (left != null && right != null)
                return root;
            if (left == null)
                return right;
            return left;
        }

        // https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-search-tree/
        public TreeNode LowestCommonAncestorBST(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null)
                return null;
            if (root.val == p.val || root.val == q.val)
                return root;
            if (root.val > Math.Min(p.val, q.val) && root.val < Math.Max(p.val, q.val))
            {
                return root;
            }
            if (root.val > Math.Max(p.val, q.val))
            {
                return LowestCommonAncestor(root.left, p, q);
            }
            else
            {
                return LowestCommonAncestor(root.right, p, q);
            }
        }

        // https://leetcode.com/problems/palindrome-linked-list/
        public bool IsPalindrome(ListNode head)
        {
            if (head == null)
                return true;
            ListNode node = head.next;
            ListNode reversedHead = new ListNode(head.val);
            reversedHead.next = null;

            while (node != null)
            {
                ListNode newNode = new ListNode(node.val);
                newNode.next = reversedHead;
                reversedHead = newNode;
                node = node.next;
            }
            node = head;
            while (reversedHead != null)
            {
                if (reversedHead.val != node.val)
                {
                    return false;
                }
                reversedHead = reversedHead.next;
                node = node.next;
            }
            return true;
        }

        // https://leetcode.com/problems/kth-smallest-element-in-a-bst/
        public int KthSmallest_Solution2(TreeNode root, int k)
        {
            Stack<TreeNode> s = new Stack<TreeNode>();
            TreeNode n = root;
            while (n != null)
            {
                s.Push(n);
                n = n.left;
            }
            while (k > 0)
            {
                TreeNode tmp = s.Pop();
                k--;
                if (k == 0)
                    return tmp.val;
                tmp = tmp.right;
                while (tmp != null)
                {
                    s.Push(tmp);
                    tmp = tmp.left;
                }
            }

            return -1;
        }
        public int KthSmallest_Solution1(TreeNode root, int k)
        {
            int l = this.CountChild(root.left);
            if (l == k - 1)
                return root.val;
            if (l + 1 > k)
            {
                return KthSmallest_Solution1(root.left, k);
            }
            else
            {
                return KthSmallest_Solution1(root.right, k - l - 1);
            }
        }

        private int CountChild(TreeNode node)
        {
            if (node == null)
                return 0;
            return 1 + CountChild(node.left) + CountChild(node.right);
        }

        // https://leetcode.com/problems/course-schedule-ii/
        public static int[] FindOrder(int numCourses, int[,] prerequisites)
        {
            List<int> ret = new List<int>();
            Course[] courses = new Course[numCourses];
            for (int i = 0; i < numCourses; i++)
            {

                courses[i] = new Course(i);
            }
            for (int j = 0; j < prerequisites.GetLength(0); j++)
            {

                courses[prerequisites[j, 0]].Prerequisites.Add(courses[prerequisites[j, 1]]);
            }
            for (int k = 0; k < numCourses; k++)
            {
                if (!courses[k].GetOrdered(ret))
                    return new int[0];
            }
            return ret.ToArray();
        }

        // https://leetcode.com/problems/summary-ranges/
        // Given a sorted integer array without duplicates, return the summary of its ranges.
        // For example, given [0,1,2,4,5,7], return ["0->2","4->5","7"].
        public IList<string> SummaryRanges(int[] nums)
        {
            List<string> ret = new List<string>();
            if (nums == null || nums.Length == 0)
                return ret;
            int start = 0;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] - 1 > nums[i - 1])
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(nums[start].ToString());
                    if (i - 1 > start)
                    {
                        sb.Append("->");
                        sb.Append(nums[i - 1].ToString());
                    }
                    ret.Add(sb.ToString());
                    start = i;
                }
            }
            StringBuilder sb2 = new StringBuilder();
            sb2.Append(nums[start].ToString());
            if (start != nums.Length - 1)
            {
                sb2.Append("->");
                sb2.Append(nums[nums.Length - 1].ToString());
            }
            ret.Add(sb2.ToString());
            return ret;
        }

        // https://leetcode.com/problems/course-schedule/
        public static bool CanFinish(int numCourses, int[,] prerequisites)
        {
            Course[] courses = new Course[numCourses];
            for (int i = 0; i < numCourses; i++)
            {

                courses[i] = new Course(i);
            }
            for (int j = 0; j < prerequisites.GetLength(0); j++)
            {

                courses[prerequisites[j, 0]].Prerequisites.Add(courses[prerequisites[j, 1]]);
            }
            for (int k = 0; k < numCourses; k++)
            {

                if (!courses[k].canBeDone())
                    return false;
            }

            return true;
        }

        // https://leetcode.com/problems/validate-binary-search-tree/
        public bool IsValidBST(TreeNode root)
        {
            return IsValidBSTHelper(root, Int64.MinValue, Int64.MaxValue);
        }

        private bool IsValidBSTHelper(TreeNode node, long min, long max)
        {
            if (node == null)
                return true;
            if (node.val <= min || node.val >= max)
                return false;
            return IsValidBSTHelper(node.left, min, node.val) && IsValidBSTHelper(node.right, node.val, max);
        }

        // not verified by oj
        public bool IsValidBST2(TreeNode root)
        {
            return IsValidBSTHelper2(root, root.left);
        }

        private bool IsValidBSTHelper2(TreeNode node, TreeNode prev)
        {
            if (node == null) return true;
            if (!IsValidBSTHelper2(node.left, node.left == null ? null : node.left.left)) return false;
            if (prev != null && prev.val >= node.val)
                return false;
            prev = node;
            return IsValidBSTHelper2(node.right, prev);
        }

        bool IsValidBSTHelper(TreeNode node, bool isRightChild, int target)
        {
            if (node == null)
                return true;
            if (isRightChild && node.val < target)
                return false;
            if (!isRightChild && node.val > target)
                return false;
            return IsValidBSTHelper(node.left, false, node.val) && IsValidBSTHelper(node.right, true, node.val);
        }

        // https://leetcode.com/problems/find-minimum-in-rotated-sorted-array/
        public int FindMinInRotatedSortedArray(int[] nums)
        {
            int ret = -1;
            int l = 0;
            int r = nums.Length - 1;
            while (l < r)
            {
                if (nums[l] < nums[r])
                    return nums[l];
                int mid = l + (r - l) / 2;
                if (nums[l] > nums[mid])
                {
                    r = mid;
                }
                else
                {
                    l = mid + 1;
                }
            }
            ret = nums[l];

            return ret;
        }

        //https://leetcode.com/problems/best-time-to-buy-and-sell-stock-ii/
        public int MaxProfit2(int[] prices)
        {
            if (prices == null || prices.Length < 2)
            {
                return 0;
            }
            int allGain = 0;
            int oneSell = 0;
            int min = prices[0];
            int max = prices[0];
            for (int i = 1; i < prices.Length; i++)
            {
                if (prices[i] < min)
                {
                    allGain += oneSell;
                    oneSell = 0;
                    min = prices[i];
                    max = prices[i];
                }
                else
                {
                    if (prices[i] > max)
                    {
                        max = prices[i];
                        oneSell = max - min;
                    }
                    else
                    {
                        allGain += oneSell;
                        min = prices[i];
                        max = prices[i];
                        oneSell = 0;
                    }
                }
            }

            return allGain + oneSell;
        }

        // https://leetcode.com/problems/best-time-to-buy-and-sell-stock/
        public int MaxProfit1(int[] prices)
        {
            if (prices == null || prices.Length < 2)
                return 0;
            int min = prices[0];
            int max = prices[0];
            int ret = 0;
            for (int i = 1; i < prices.Length; i++)
            {
                if (prices[i] < min)
                {
                    min = prices[i];
                    max = prices[i];
                }
                else if (prices[i] > max)
                {
                    max = prices[i];
                    if (max - min > ret)
                    {
                        ret = max - min;
                    }
                }

            }
            return ret;
        }

        // https://leetcode.com/problems/search-in-rotated-sorted-array/
        public int Search(int[] nums, int target)
        {
            int ret = -1;
            if (nums == null || nums.Length == 0)
                return ret;
            int left = 0;
            int right = nums.Length - 1;
            int minp = -1;
            while (left < right)
            {
                if (nums[left] < nums[right])
                {
                    break;
                }
                int mid = left + (right - left) / 2;
                if (nums[left] > nums[mid])
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }

            minp = left;

            if (target < nums[minp])
                return -1;
            ret = Bsearch(nums, 0, minp - 1, target);
            if (ret >= 0)
            {
                return ret;
            }

            return Bsearch(nums, minp, nums.Length - 1, target);
        }

        private int Bsearch(int[] array, int start, int end, int target)
        {
            int l = start;
            int r = end;
            while (l <= r)
            {
                int mid = l + (r - l) / 2;
                if (target == array[mid])
                    return mid;
                if (target > array[mid])
                {
                    l = mid + 1;
                }
                else
                {
                    r = mid - 1;
                }
            }

            return -1;
        }

        // https://leetcode.com/problems/merge-intervals/
        public IList<Interval> Merge(IList<Interval> intervals)
        {
            IList<Interval> ret = new List<Interval>();
            if (intervals == null || intervals.Count == 0)
            {
                return ret;
            }
            Interval[] array = new Interval[intervals.Count];
            intervals.CopyTo(array, 0);
            Array.Sort(array, this.CompareByStart);

            ret.Add(array[0]);
            for (int i = 1; i < array.Length; i++)
            {
                Interval tmp = ret[ret.Count - 1];
                if (tmp.end < array[i].start)
                {
                    ret.Add(array[i]);
                    continue;
                }

                if (tmp.end >= array[i].start && tmp.end < array[i].end)
                {
                    tmp.end = array[i].end;
                }
            }

            return ret;
        }

        private int CompareByStart(Interval a, Interval b)
        {
            if (a.start < b.start)
                return -1;
            if (a.start > b.start)
                return 1;
            return 0;
        }

        // https://leetcode.com/problems/invert-binary-tree/
        public TreeNode InvertTree(TreeNode root)
        {
            if (root == null)
                return null;
            TreeNode tmpl = InvertTree(root.left);
            TreeNode tmpr = InvertTree(root.right);
            root.left = tmpr;
            root.right = tmpl;
            return root;
        }

        // https://leetcode.com/problems/kth-largest-element-in-an-array/
        public static int FindKthLargest(int[] nums, int k)
        {
            int start = 0;
            int end = nums.Length - 1;
            while (true)
            {
                int i = ArrayPartition(nums, start, end);
                if (i + 1 == k)
                    return nums[i];
                if (i + 1 < k)
                {
                    start = i + 1;
                }
                else
                {
                    end = i - 1;
                }
            }
        }

        private static int ArrayPartition(int[] nums, int start, int end)
        {
            int tmp = nums[start];
            int left = start + 1;
            int right = end;
            while (left <= right)
            {
                while (left < end && nums[left] >= tmp)
                {
                    left++;
                }
                while (right > start && nums[right] <= tmp)
                {
                    right--;
                }

                if (left < right)
                {
                    int a = nums[left];
                    nums[left] = nums[right];
                    nums[right] = a;
                }
            }
            nums[start] = nums[right];
            nums[right] = tmp;

            return right;
        }

        // https://leetcode.com/submissions/detail/29662115/
        public int ComputeArea(int A, int B, int C, int D, int E, int F, int G, int H)
        {
            int areaA = (C - A) * (D - B);
            int areaB = (G - E) * (H - F);

            int allArea = areaA + areaB;
            int overlap = 0;
            int ol = Math.Min(G, C) - Math.Max(A, E);
            int oh = Math.Min(H, D) - Math.Max(B, F);
            if (ol > 0 && oh > 0)
            {
                overlap = ol * oh;
            }
            return allArea - overlap;
        }

        // https://leetcode.com/problems/count-complete-tree-nodes/
        public int CountNodesRecursive(TreeNode root)
        {
            if (root == null)
                return 0;
            int l = 1;
            int r = 1;
            TreeNode node = root.left;
            while (node != null)
            {
                l++;
                node = node.left;
            }

            node = root.right;
            while (node != null)
            {
                r++;
                node = node.right;
            }
            if (l == r)
                return (int)Math.Pow(2, l) - 1;
            return 1 + CountNodesRecursive(root.left) + CountNodesRecursive(root.right);
        }

        // https://leetcode.com/problems/contains-duplicate-ii/
        public bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            if (nums.Length < 2)
                return false;
            for (int i = 0; i < nums.Length; i++)
            {
                if (dict.ContainsKey(nums[i]))
                {
                    if (i - dict[nums[i]] <= k)
                    {
                        return true;
                    }
                    else
                    {
                        dict[nums[i]] = i;
                    }
                }
                else
                {
                    dict.Add(nums[i], i);
                }
            }
            return false;
        }

        // https://leetcode.com/problems/contains-duplicate/
        public bool ContainsDuplicate(int[] nums)
        {

            HashSet<int> dict = new HashSet<int>();
            if (nums.Length < 2)
                return false;
            foreach (int num in nums)
            {
                if (dict.Contains(num))
                    return true;
                dict.Add(num);
            }
            return false;
        }


        // https://leetcode.com/problems/minimum-size-subarray-sum/
        // O(n2)
        public int MinSubArrayLen(int s, int[] nums)
        {
            if (nums.Length == 0)
                return 0;
            if (s == 0)
                return 1;
            int start = 0;
            int end = 0;
            int tmpSum = 0;
            int minLen = nums.Length;
            while (end < nums.Length && tmpSum < s)
            {
                tmpSum += nums[end++];
            }

            if (end == nums.Length && tmpSum < s)
            {
                return 0;
            }

            minLen = end - start < minLen ? end - start : minLen;
            while (start < nums.Length)
            {
                if (tmpSum >= s)
                {
                    tmpSum -= nums[start++];
                }
                else
                {
                    if (end < nums.Length)
                    {
                        tmpSum += nums[end++];
                    }
                    else
                    {
                        return minLen;
                    }
                }

                if (tmpSum >= s && end - start < minLen)
                {
                    minLen = end - start;
                }
            }

            return minLen;
        }


        // https://leetcode.com/problems/reverse-linked-list/
        ListNode reverseList(ListNode head)
        {
            if (head == null)
                return null;
            ListNode newHead = head;
            ListNode nextNode = head.next;

            newHead.next = null;
            while (nextNode != null)
            {
                ListNode tmp = nextNode;
                nextNode = nextNode.next;
                tmp.next = newHead;
                newHead = tmp;
            }

            return newHead;
        }

        public static void PrintSet(int i)
        {
            if (i == 0)
                return;
            for (int k = 0; k < Math.Pow(2, i); k++)
            {
                StringBuilder sb = new StringBuilder();
                for (int m = i; m > 0; m--)
                {
                    if ((k & (1 << (m - 1))) > 0)
                    {
                        sb.Append('T');
                    }
                    else
                    {
                        sb.Append('F');
                    }
                }
                Console.WriteLine(sb.ToString());
            }
        }

        // https://leetcode.com/problems/remove-linked-list-elements/
        public ListNode RemoveElements(ListNode head, int val)
        {
            ListNode dummyNode = new ListNode(0);
            dummyNode.next = head;
            ListNode cur = dummyNode;
            while (cur != null)
            {
                if (cur.next != null && cur.next.val == val)
                {
                    cur.next = cur.next.next;
                }
                else
                {
                    cur = cur.next;
                }
            }
            return dummyNode.next;
        }

        // https://leetcode.com/problems/minimum-window-substring/
        // For example,
        // S = "ADOBECODEBANC"
        // T = "ABC"
        // Minimum window is "BANC".
        public static string MinWindow(string S, string T)
        {
            if (S.Length == 0 || T.Length == 0 || T.Length > S.Length)
            {
                return string.Empty;
            }

            Dictionary<char, int> expectedDict = new Dictionary<char, int>();
            Dictionary<char, int> appearance = new Dictionary<char, int>();
            foreach (char c in T)
            {
                if (!appearance.ContainsKey(c))
                {
                    appearance.Add(c, 0);
                }
                if (expectedDict.ContainsKey(c))
                {
                    expectedDict[c]++;
                }
                else
                {
                    expectedDict.Add(c, 1);
                }
            }

            int start = 0;
            int minStart = -1;
            int minEnd = S.Length;
            int count = 0;
            for (int i = 0; i < S.Length; i++)
            {
                if (expectedDict.ContainsKey(S[i]))
                {
                    appearance[S[i]]++;

                    if (appearance[S[i]] <= expectedDict[S[i]])
                    {
                        count++;
                    }

                    if (count == T.Length)
                    {
                        while (!expectedDict.ContainsKey(S[start]) || appearance[S[start]] > expectedDict[S[start]])
                        {

                            if (appearance.ContainsKey(S[start]))
                            {
                                appearance[S[start]]--;
                            }
                            start++;
                        }
                        if (i - start < minEnd - minStart)
                        {
                            minStart = start;
                            minEnd = i;
                        }
                    }
                }
            }

            if (minStart == -1)
                return string.Empty;

            return S.Substring(minStart, minEnd - minStart + 1);
        }

        // https://leetcode.com/problems/longest-consecutive-sequence/
        public static int LongestConsecutive(int[] num)
        {
            if (num.Length == 0)
                return 0;
            HashSet<int> dict = new HashSet<int>(num);
            int maxLength = 1;
            foreach (int k in num)
            {
                int count = 1;
                int m = k;
                while (dict.Contains(m + 1))
                {
                    count++;
                    dict.Remove(m);
                    m = m + 1;
                }
                m = k;
                while (dict.Contains(m - 1))
                {
                    count++;
                    dict.Remove(m);
                    m = m - 1;
                }
                if (count > maxLength)
                {
                    maxLength = count;
                }
            }
            return maxLength;
        }

        public static IList<int> PreorderTraversal(TreeNode root)
        {
            IList<int> ret = new List<int>();
            if (root == null)
            {
                return ret;
            }

            Stack<TreeNode> stk = new Stack<TreeNode>();

            while (root != null)
            {
                ret.Add(root.val);
                if (root.right != null)
                {
                    stk.Push(root.right);
                }

                if (root.left != null)
                {
                    root = root.left;
                }
                else
                {
                    root = stk.Count != 0 ? stk.Pop() : null;
                }
            }
            return ret;
        }

        // https://leetcode.com/problems/text-justification/
        public static IList<string> FullJustify(IList<string> words, int L)
        {
            if (words.Count == 0)
                return words;
            var ret = new List<string>();
            int listIndex = 0;
            Queue<int> spacePos = new Queue<int>();
            StringBuilder currentLine = new StringBuilder();
            while (listIndex < words.Count)
            {
                if (currentLine.Length != 0)
                {
                    currentLine.Append(" ");
                }
                string lastWord = words[listIndex];
                currentLine.Append(lastWord);
                if (currentLine.Length == L)
                {
                    ret.Add(currentLine.ToString());
                    currentLine.Clear();
                    spacePos.Clear();
                }
                else if (currentLine.Length > L)
                {
                    currentLine.Remove(currentLine.Length - lastWord.Length - 1, lastWord.Length + 1);
                    int spaceNeeded = L - currentLine.Length;
                    int spaceAdded = 0;
                    while (spaceNeeded > 0)
                    {
                        int spaceNum = spaceNeeded;
                        if (spacePos.Count > 1)
                        {
                            spaceNum = spaceNeeded % (spacePos.Count - 1) == 0 ? spaceNeeded / (spacePos.Count - 1) : spaceNeeded / (spacePos.Count - 1) + 1;
                        }
                        currentLine.Insert(spacePos.Dequeue() + spaceAdded, " ", spaceNum);
                        spaceAdded += spaceNum;
                        spaceNeeded -= spaceNum;
                    }
                    ret.Add(currentLine.ToString());
                    listIndex -= 1;
                    currentLine.Clear();
                    spacePos.Clear();
                }
                else if (currentLine.Length < L)
                {
                    if (listIndex == words.Count - 1)
                    {
                        while (currentLine.Length < L)
                        {
                            currentLine.Append(" ");
                        }
                        ret.Add(currentLine.ToString());
                    }
                    else
                    {
                        spacePos.Enqueue(currentLine.Length);
                    }
                }

                listIndex++;
            }

            return ret;
        }
    }
}