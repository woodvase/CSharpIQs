/*
 * Created by SharpDevelop.
 * User: yoli
 * Date: 5/18/2015
 * Time: 9:54 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
namespace Solution {
    class Solution {
        static void Main1(string[] args) {
            /* Enter your code here. Read input from STDIN. Print output to STDOUT */
            int n = Convert.ToInt32(Console.ReadLine());
            string klm = Console.ReadLine();
            int k = Convert.ToInt32(klm.Split(' ')[0]);
            int l = Convert.ToInt32(klm.Split(' ')[1]);
            int m = Convert.ToInt32(klm.Split(' ')[2]);
            // Debug.Assert() for the constraints
            string str = Console.ReadLine();
            int ret = 0;
			Dictionary<string, int> dict = new Dictionary<string, int>();
            for(int i = k; i <= l; i ++)
            {
                for(int a = 0; a + i <= n; a ++)
                {
                    string substr = str.Substring(a, i);
                    if(!NumOfCharsExceeds(substr, m))
                    {
						if (!dict.ContainsKey(substr))
						{
							dict.Add(substr, 1);
						}
						else
						{
							dict[substr] += 1;
						}
						
						if (dict[substr] > ret)
						{
							ret = dict[substr];
						}
                    }
                }
            }
            Console.WriteLine(ret);
	    }
        private static bool NumOfCharsExceeds(string str, int m)
        {
              if(str.Length <= m)
                  return false;
             
              int[] chars = new int[26];
              for(int i = 0; i < str.Length; i ++)
              {
                chars[str[i] - 'a'] = 1;    
              }
                
              int count = 0;
              for(int i = 0; i < 26; i ++)
              {
                  if(chars[i] == 1)
                  {
                    count ++;    
                  }
              }
              return count > m;
        }
    }
}