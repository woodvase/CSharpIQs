/*
 * Created by SharpDevelop.
 * User: yoli
 * Date: 5/11/2015
 * Time: 10:23 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace InterviewQs
{
	public class TrieNode
	{
		public char value;
		public Dictionary<char, TrieNode> children;
		public bool IsEndOfWord;
		public TrieNode(char key)
		{
			this.value = key;
			this.children = new Dictionary<char, TrieNode>();
			this.IsEndOfWord = false;
		}
	}
	
	/// <summary>
	/// Description of Trie.
	/// </summary>
	public class Trie
	{
		private TrieNode root;
		
		public Trie()
		{
			root = new TrieNode(' ');
		}

		// Inserts a word into the trie.
		public void Insert(String word)
		{
			this.InsertHelper(this.root, word, 0);
		}
		
		private void InsertHelper(TrieNode n, string word, int index)
		{
			if (index == word.Length)
			{	
				n.IsEndOfWord = true;
				return;
			}
			TrieNode node = null;
			char c = word[index];
			if (n.children.ContainsKey(c))
			{
				node = n.children[c];
			}
			else
			{
				node = new TrieNode(c);
				n.children.Add(c, node);
			}
			
			InsertHelper(node, word, index + 1);
		}

		// Returns if the word is in the trie.
		public bool Search(string word)
		{
			TrieNode node = this.root;
			if (node == null)
				return false;
			return this.SearchHelper(word, node, 0);
		}
		
		private bool SearchHelper(string word, TrieNode n, int index)
		{
			if (n == null)
				return false;
			if (index == word.Length)
			{
				return n.IsEndOfWord;
			}
			
			if (n.children.ContainsKey(word[index]))
			{
				return SearchHelper(word, n.children[word[index]], index + 1);
			}
			else
				return false;
		}

		// Returns if there is any word in the trie
		// that starts with the given prefix.
		public bool StartsWith(string word)
		{
			return this.StartsWithHelper(word, this.root, 0);
		}
		
		private bool StartsWithHelper(string word, TrieNode node, int index)
		{
			if (node == null)
				return false;
			if (index == word.Length)
			{
				return true;
			}
			
			if (node.children.ContainsKey(word[index]))
			{
				return StartsWithHelper(word, node.children[word[index]], index + 1);
			}
			else
				return false;
		}
	}
	
	// Your Trie object will be instantiated and called as such:
	// Trie trie = new Trie();
	// trie.Insert("somestring");
	// trie.Search("key");
}
