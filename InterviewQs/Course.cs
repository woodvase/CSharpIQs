/*
 * Created by SharpDevelop.
 * User: yoli
 * Date: 6/29/2015
 * Time: 1:40 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace InterviewQs
{
	/// <summary>
	/// Description of Course.
	/// </summary>
	public class Course
	{
		public int Id;
		public List<Course> Prerequisites;
		public bool isDone;
		public bool hasVisited;
		
		public Course(int id)
		{
			this.Id = id;
			this.Prerequisites = new List<Course>();
			this.isDone = false;
			this.hasVisited = false;
		}
		
		public bool canBeDone()
		{
			if (!this.isDone) {
				this.hasVisited = true;
				foreach (Course pre in Prerequisites) {
					if (!pre.isDone && pre.hasVisited == true)
						return false;
					if (!pre.canBeDone())
						return false;
				}
				this.isDone = true;
			}			
			return true;
		}
		
		public bool GetOrdered(List<int> order)
		{
			if (!this.isDone) {
				if (this.hasVisited)
					return false;
				this.hasVisited = true;
				foreach (Course pre in this.Prerequisites) {
					if (!pre.GetOrdered(order))
						return false;
				}
				order.Add(this.Id);
				this.isDone = true;
			}
			return true;
		}
	}
}
