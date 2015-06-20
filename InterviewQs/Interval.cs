/*
 * Created by SharpDevelop.
 * User: yoli
 * Date: 6/19/2015
 * Time: 10:05 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace InterviewQs
{
	/// <summary>
	/// Description of Interval.
	/// </summary>
	public class Interval
	{
		public int start;
		public int end;
		public Interval()
		{
			this.start = 0;
			this.end = 0;
		}
		public Interval(int start, int end)
		{
			this.start = start;
			this.end = end;
		}
	}
}
