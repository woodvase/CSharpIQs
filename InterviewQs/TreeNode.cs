/*
 * Created by SharpDevelop.
 * User: yoli
 * Date: 4/19/2015
 * Time: 3:24 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace InterviewQs
{
	/// <summary>
	/// Description of TreeNode.
	/// </summary>
	public class TreeNode
	{
		public int val;
		public TreeNode left;
		public TreeNode right;
		public TreeNode(int val)
		{
			this.left = null;
			this.right = null;
			this.val = val;
		}
	}
}
