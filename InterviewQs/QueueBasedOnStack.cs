/*
 * Created by SharpDevelop.
 * User: yoli
 * Date: 7/7/2015
 * Time: 11:06 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace InterviewQs
{
	/// <summary>
	/// Description of QueueBasedOnStack.
	/// https://leetcode.com/problems/implement-queue-using-stacks/
	/// </summary>
	public class QueueBasedOnStack
	{
		private Stack<int> pushs = new Stack<int>();
		private Stack<int> pops = new Stack<int>();
		
		// Push element x to the back of queue.
		public void Push(int x)
		{
			pushs.Push(x);
		}

		// Removes the element from front of queue.
		public void Pop()
		{
			if (pops.Count == 0)
			{
				while (pushs.Count > 0)
				{
					pops.Push(pushs.Pop());
				}
			}
			pops.Pop();
		}

		// Get the front element.
		public int Peek()
		{
			if (pops.Count == 0)
			{
				while (pushs.Count > 0)
				{
					pops.Push(pushs.Pop());
				}
			}
			return pops.Peek();
		}

		// Return whether the queue is empty.
		public bool Empty()
		{
			return pushs.Count == 0 && pops.Count == 0;
		}
	}
}
