using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

/*

--- Day 4: The Ideal Stocking Stuffer ---

Santa needs help mining some AdventCoins (very similar to bitcoins) to use as gifts for all the economically forward-thinking little girls and boys.

To do this, he needs to find MD5 hashes which, in hexadecimal, start with at least five zeroes. The input to the MD5 hash is some secret key (your puzzle input, given below) followed by a number in decimal. To mine AdventCoins, you must find Santa the lowest positive number (no leading zeroes: 1, 2, 3, ...) that produces such a hash.

For example:

    If your secret key is abcdef, the answer is 609043, because the MD5 hash of abcdef609043 starts with five zeroes (000001dbbfa...), and it is the lowest such number to do so.
    If your secret key is pqrstuv, the lowest number it combines with to make an MD5 hash starting with five zeroes is 1048970; that is, the MD5 hash of pqrstuv1048970 looks like 000006136ef....

*/

namespace Day4_Challenge1
{
	class Program
	{
		static string m_Input = "iwrupvqb";

		static void Main(string[] args)
		{
			int t_Guess = 0;
			bool t_Guessing = true;

			using (MD5 hash = MD5.Create())
			{
				while (t_Guessing)
				{
					byte[] output = hash.ComputeHash(Encoding.UTF8.GetBytes(m_Input + t_Guess));

					StringBuilder guess = new StringBuilder();

					foreach (byte character in output) { guess.Append(character.ToString("x2")); }

					if (guess.ToString().Substring(0, 5) == "00000")
					{
						Console.WriteLine("=================================================");
						Console.WriteLine("Found Answer:");
						Console.WriteLine(m_Input + t_Guess + " - " + guess.ToString());
						t_Guessing = false;
					}
					else
					{
						Console.WriteLine(t_Guess + " - " + guess.ToString());
					}

					t_Guess++;
				}
			}

			// Halt Exit
			Console.ReadKey();
		}
	}
}
