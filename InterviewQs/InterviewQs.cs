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
			string[] strs = new string[]
			{
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
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
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