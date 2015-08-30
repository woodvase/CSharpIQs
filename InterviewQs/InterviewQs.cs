/*
 * Created by SharpDevelop.
 * User: yoli
 * Date: 4/19/2015
 * Time: 3:17 PM
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace InterviewQs
{
    class Program
    {
        public static void Main(string[] args)
        {
            // TODO: Implement Functionality Here
            Console.WriteLine(Multiply("124", "0"));
            Console.WriteLine(CalculateSum("1*1+1-1*3"));
            List<int> ab = MergeSortedListsWithoutDup(new List<int> { 8, 8, 8 }, new List<int> { 3, 3 });
            Console.WriteLine("Merged list start.");
            foreach (int i in ab)
            {
                Console.Write(i);
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.WriteLine("Merged list end.");
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

        // https://leetcode.com/problems/sort-list/
        public ListNode SortList(ListNode head)
        {
            if (head == null || head.next == null) return head;
            ListNode prev = head;
            ListNode fast = head;
            ListNode slow = head;
            while (fast != null && fast.next != null)
            {
                prev = slow;
                slow = slow.next;
                fast = fast.next.next;
            }

            prev.next = null;

            ListNode firstHalf = SortList(head);
            ListNode secondHarf = SortList(slow);
            return MergeSortedList(firstHalf, secondHarf);
        }

        ListNode MergeSortedList(ListNode l1, ListNode l2)
        {
            ListNode dummy = new ListNode(0);
            ListNode p1 = l1;
            ListNode p2 = l2;
            ListNode p = dummy;
            while (p1 != null && p2 != null)
            {
                if (p1.val > p2.val)
                {
                    p.next = p2;
                    p2 = p2.next;
                }
                else
                {
                    p.next = p1;
                    p1 = p1.next;
                }
                p = p.next;
            }

            if (p1 != null)
            {
                p.next = p1;
            }
            if (p2 != null)
            {
                p.next = p2;
            }

            return dummy.next;
        }

        // https://leetcode.com/problems/happy-number/
        public bool IsHappyHelper(int n, HashSet<int> set)
        {
            if (n <= 0)
                return false;
            if (n == 1)
                return true;
            if (set.Contains(n))
                return false;
            set.Add(n);
            int tmp = 0;
            while (n > 0)
            {
                int x = n % 10;
                tmp += x * x;
                n = n / 10;
            }
            return IsHappyHelper(tmp, set);
        }
        public bool IsHappy(int n)
        {
            HashSet<int> visitedNumber = new HashSet<int>();
            return IsHappyHelper(n, visitedNumber);
        }

        public int MissingNumber(int[] nums)
        {
            BitArray ba = new BitArray(nums.Length + 1);
            foreach (int n in nums)
                ba.Set(n, true);
            int i = 0;
            foreach (bool bit in ba)
            {
                if (!bit)
                {
                    break;
                }
                i++;
            }

            return i;
        }

        // https://leetcode.com/problems/ugly-number/
        public bool IsUgly(int num)
        {
            if (num <= 0) return false;
            if (num == 1) return true;
            if (num % 2 == 0)
                return IsUgly(num / 2);
            if (num % 3 == 0)
                return IsUgly(num / 3);
            if (num % 5 == 0)
                return IsUgly(num / 5);
            return false;
        }

        // https://leetcode.com/problems/add-digits/
        public int AddDigits(int num)
        {
            if (num >= 0 && num <= 9)
                return num;
            int tmp = 0;
            while (num > 0)
            {
                tmp += num % 10;
                num = num / 10;
            }
            return AddDigits(tmp);
        }

        // https://leetcode.com/problems/binary-tree-paths/
        public IList<string> BinaryTreePaths(TreeNode root)
        {
            List<string> ret = new List<string>();
            if (root == null)
                return ret;
            StringBuilder sb = new StringBuilder();
            BinaryTreePathsHelper(root, ret, sb);
            return ret;
        }

        private void BinaryTreePathsHelper(TreeNode node, List<string> paths, StringBuilder path)
        {
            if (path.Length > 0)
            {
                path.Append("->");
            }
            path.Append(node.val.ToString());
            if (node.left == null && node.right == null)
            {
                paths.Add(path.ToString());
            }
            else
            {
                StringBuilder tmp = new StringBuilder(path.ToString());
                if (node.left != null)
                {
                    BinaryTreePathsHelper(node.left, paths, path);
                }
                if (node.right != null)
                {
                    BinaryTreePathsHelper(node.right, paths, tmp);
                }
            }
        }

        // https://leetcode.com/problems/reverse-nodes-in-k-group/
        // Given this linked list: dummy->1->2
        // For k = 2, you should return: 2->1->4->3->5
        // For k = 3, you should return: 3->2->1->4->5
        public ListNode ReverseKGroup(ListNode head, int k)
        {
            if (head == null || k == 1) return head;
            ListNode dummy = new ListNode(-1);
            dummy.next = head;
            ListNode khead = dummy;
            ListNode kend = head;
            int i = 1;
            while (kend != null)
            {
                kend = kend.next;
                i++;
                if (i % k == 0 && kend != null)
                {
                    ListNode knext = khead.next;
                    khead.next = kend;
                    khead = knext;
                    while (khead != kend)
                    {
                        knext = khead.next;
                        khead.next = kend.next;
                        kend.next = khead;
                        khead = knext;
                    }
                    // dummy -> 2(kend, khead)->1->3->4->5
                    int m = k;
                    while (m > 1)
                    {
                        kend = kend.next;
                        m--;
                    }
                    khead = kend;
                    // dummy -> 2 ->1 (khead, kend) -> 3 -> 4 -> 5
                }
            }

            return dummy.next;
        }

    // https://leetcode.com/problems/multiply-strings/
    // // "13" * "11"
    public static string Multiply(string num1, string num2)
        {
            string snum = num1.Length > num2.Length ? num2 : num1;
            string lnum = num1.Length > num2.Length ? num1 : num2;
            string ret = string.Empty;
            for (int i = 0; i < snum.Length; i ++)
            {
                string cur = string.Empty;
                int m = 0;
                while (m < (snum[i] - '0'))
                {
                    cur = AddString(cur, lnum);
                    m++;
                }
                ret = AddString(new StringBuilder(ret).Append('0').ToString(), cur);
            }

            return ret;
        }

        public static string AddString(string num1, string num2)
        {
            int l1 = num1.Length - 1;
            int l2 = num2.Length - 1;
            if (num1.Length == 0 && num2.Length == 0)
                return string.Empty;
            if (num1.Length == 0)
                return num2;
            if (num2.Length == 0)
                return num1;
            int carryBit = 0;
            StringBuilder sb = new StringBuilder();
            while (l1 >= 0 && l2 >= 0)
            {
                int i1 = num1[l1--] - '0';
                int i2 = num2[l2--] - '0';
                int bitSum = i1 + i2 + carryBit;
                if (bitSum >= 10)
                {
                    carryBit = 1;
                    sb.Insert(0, (char)(bitSum - 10 + '0'));
                }
                else
                {
                    carryBit = 0;
                    sb.Insert(0, (char)(bitSum + '0'));
                }
            }

            string num;
            int l;
            if (l1 >= 0)
            {
                num = num1;
                l = l1;
            }
            else
            {
                num = num2;
                l = l2;
            }

            while (l >= 0)
            {
                int i = num[l --] - '0' + carryBit;
                if (i >= 10)
                {
                    sb.Insert(0, (char)(i - 10 + '0'));
                    carryBit = 1;
                }
                else
                {
                    sb.Insert(0, (char)(i + '0'));
                    carryBit = 0;
                }
            }

            if (carryBit == 1)
            {
                sb.Insert(0, '1');
            }

            return sb.ToString();
        }

        static public int CalculateSum(string s)
        {
            Stack<string> stk = new Stack<string>();
            StringBuilder currentNum = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] >= '0' && s[i] <= '9')
                {
                    currentNum.Append(s[i]);
                }
                else if (s[i] == '+' || s[i] == '-')
                {
                    stk.Push(currentNum.ToString());
                    stk.Push(s[i].ToString());
                    currentNum.Clear();
                }
                else if (s[i] == '*' || s[i] == '\\')
                {
                    char csign = s[i];
                    int j = int.Parse(currentNum.ToString());
                    i++;
                    int num = 0;
                    while (i < s.Length && s[i] >= '0' && s[i] <= '9')
                    {
                        num = num * 10 + s[i ++] - '0';
                    }

                    // i is not the end and s[i] is one of + - * /
                    if (i < s.Length)
                    {
                        i--;
                    }
                    if (csign == '*')
                    {
                        currentNum = new StringBuilder((j * num).ToString());
                    }
                    else
                    {
                        currentNum = new StringBuilder((j / num).ToString());
                    }
                }
            }

            stk.Push(currentNum.ToString());
            while (stk.Count > 1)
            {
                int m = int.Parse(stk.Pop());
                string sign = stk.Pop();
                int n = int.Parse(stk.Pop());
                if (sign.Equals("+"))
                {
                    stk.Push((m + n).ToString());
                }
                else
                {
                    stk.Push((n-m).ToString());
                }
            }

            return int.Parse(stk.Pop());
        }

        // https://leetcode.com/problems/anagrams/
        public IList<string> Anagrams(string[] strs)
        {
            List<string> ret = new List<string>();
            if (strs == null || strs.Length == 0)
                return ret;
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            foreach (string s in strs)
            {
                char[] a = s.ToCharArray();
                Array.Sort(a);
                string sorted = new string(a);
                if (dict.ContainsKey(sorted))
                {
                    dict[sorted].Add(s);
                }
                else
                {
                    List<string> list = new List<string>();
                    list.Add(s);
                    dict.Add(sorted, list);
                }
            }

            foreach (KeyValuePair<string, List<string>> kvp in dict)
            {
                if (kvp.Value.Count > 1)
                {
                    ret.AddRange(kvp.Value);
                }
            }

            return ret;
        }

        // https://leetcode.com/problems/valid-anagram/
        public bool IsAnagram(string s, string t)
        {
            if (s == null || t == null) return false;
            if (s.Length != t.Length) return false;
            int[] chars = new int[26];
            foreach (char c in s)
            {
                chars[c - 'a'] += 1;
            }

            foreach (char c in t)
            {
                chars[c - 'a'] -= 1;
            }

            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] != 0)
                    return false;
            }

            return true;
        }

        static public List<int> MergeSortedListsWithoutDup(List<int> a, List<int> b)
        {
            // Merge
            int la = 0;
            int lb = 0;
            List<int> newList = new List<int>();
            while (la < a.Count && lb < b.Count)
            {
                if (a[la] < b[lb])
                {
                    newList.Add(a[la]);
                    la++;
                }
                else
                {
                    newList.Add(b[lb]);
                    lb++;
                }
            }

            if (la < a.Count)
            {
                List<int> restA= a.GetRange(la, a.Count - la);
                newList.AddRange(restA);
            }

            if (lb < b.Count)
            {
                List<int> restB = b.GetRange(lb, b.Count - lb);
                newList.AddRange(restB);
            }

            // Dedup
            return DeDup(newList);          
        }

        static public List<int> DeDup(List<int> list)
        {
            if (list.Count == 0)
                return new List<int>();
            int[] tmp = list.ToArray();

            int pre = tmp[0];
            int index = 1;
            for (int i = 1; i < tmp.Length; i++)
            {
                if (tmp[i] != pre)
                {
                    tmp[index] = tmp[i];
                    pre = tmp[i];
                    index++;
                }
            }

            List<int> ret = new List<int>();
            for (int i = 0; i < index; i++)
            {
                ret.Add(tmp[i]);
            }

            return ret;
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