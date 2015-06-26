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
			Trie trie = new Trie();
			trie.Insert("dogg");
			trie.Insert("deot");
			trie.Insert("eat");
			trie.Insert("de");
			Console.WriteLine(trie.Search("dog"));
			Console.WriteLine(trie.StartsWith("dog"));
			
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
			PrintSet(4);
			//ContainsNearbyAlmostDuplicate(new int[]{7,1,3}, 2, 3);
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		public int MaxProfit(int[] prices) 
		{
			if(prices == null || prices.Length < 2)
				return 0;
			int min = prices[0];
			int max = prices[0];
			int ret = max - min;
			for(int i = 1; i< prices.Length; i ++)
			{
				if(prices[i] < min)
				{
					min = prices[i];
				}
				else
				{
					if(prices[i] > max)
					{
						max = prices[i];
						if(max - min > ret)
						{
							ret = max - min;
						}
					}
				}
			}
			return ret;
		}
		
		// https://leetcode.com/problems/word-search-ii/
		public static IList<string> FindWords(char[,] board, string[] words)
		{
			List<string> ret = new List<string>();
			if(words == null || words.Length == 0) 
				return ret;
			foreach(string str in words)
			{
				bool[,] flags = new bool[board.GetLength(0),board.GetLength(1)];
				for(int i = 0; i < board.GetLength(0); i ++)
					for(int j = 0; j < board.GetLength(1); j ++)
				{
					FildWordsHelper(board, i, j, str, 0, flags, ret);
				}
			}
			return ret;
		}
        
		private static bool FildWordsHelper(char[,] board, int row, int col, string word, int index, bool[,] flags, List<string> foundStrings)
		{
			if(index == word.Length)
			{
				foundStrings.Add(word);
				return true;
			}
			
			if(row < 0 || row > board.GetLength(1))
				return false;
			if(col < 0 || col > board.GetLength(0))
				return false;
			
			if(!flags[i, j] && board[i,j] == word[index])
			{
				flags[i,j] = true;
				bool up = FildWordsHelper(board, row - 1, col,  word, index + 1, flags, foundStrings);
				if(up)
					return true;
				bool down = FildWordsHelper(board, row + 1, col,  word, index + 1, flags, foundStrings);
				if(down)
					return true;
				bool left = FildWordsHelper(board, row, col - 1,  word, index + 1, flags, foundStrings);
				if(left)
					return true;
				bool right = FildWordsHelper(board, row, col + 1,  word, index + 1, flags, foundStrings);
				if(right)
					return true;
				flags[i,j] = false;
			}
			
			return false;
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
			if(ol > 0 && oh > 0)
			{
				overlap = ol * oh;
			}
			return allArea - overlap;
		}
		
		// https://leetcode.com/problems/count-complete-tree-nodes/
		public int CountNodesRecursive(TreeNode root)
		{
			if(root == null)
				return 0;
			int l = 1;
			int r = 1;
			TreeNode node = root.left;
			while(node != null)
			{
				l ++;
				node = node.left;
			}
			
			node = root.right;
			while(node != null)
			{
				r ++;
				node = node.right;
			}
			if(l == r) return (int)Math.Pow(2, l) - 1;
			return 1 + CountNodesRecursive(root.left) + CountNodesRecursive(root.right);
		}
		
		// https://leetcode.com/problems/contains-duplicate-ii/
		public bool ContainsNearbyDuplicate(int[] nums, int k) 
		{
			Dictionary<int, int> dict = new Dictionary<int, int>();
			if(nums.Length < 2) return false;
			for(int i = 0; i < nums.Length; i ++)
			{
				if(dict.ContainsKey(nums[i]))
				{
					if(i - dict[nums[i]] <= k)
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
			if(nums.Length < 2) return false;
			foreach(int num in nums)
			{
				if(dict.Contains(num))
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
					tmpSum -= nums[start ++];
				}
				else
				{
					if (end < nums.Length)
					{
						tmpSum += nums[end ++];
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
			if(head == null)
				return null;
			ListNode newHead = head;
			ListNode nextNode = head.next;
			
			newHead.next = null;
			while(nextNode != null)
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
			if(i == 0) return;
			for(int k = 0; k < Math.Pow(2,i); k ++)
			{
				StringBuilder sb = new StringBuilder();
				for(int m = i; m > 0; m --)
				{
					if((k & (1 << (m - 1))) > 0)
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