using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Security.Cryptography;

/*

--- Part Two ---

Now find one that starts with six zeroes.

Your puzzle input is still iwrupvqb.

*/

namespace Day4_Challenge2
{
	class Program
	{
		public static string m_Input = "iwrupvqb";
		public static int m_ThreadCount = 8;
		public static bool m_IsGuessing = true;
		public static int m_Answer = 0;
		public static string m_AnswerHash = "";

		static void Main(string[] args)
		{
			List<Worker> m_Workers = new List<Worker>();
			List<Thread> m_WorkerThreads = new List<Thread>();

			// Create Workers
			for (int i = 0; i < m_ThreadCount; i++)
			{
				Worker worker = new Worker(i);
				Thread thread = new Thread(new ThreadStart(worker.ThreadProcess));

				thread.Start();

				m_Workers.Add(worker);
				m_WorkerThreads.Add(thread);
            }

			// Halt While Guessing
			while (m_IsGuessing) { }

			// Wait for All To Rejoin
			while (m_WorkerThreads.Count > 0)
			{
				if (!m_WorkerThreads[0].IsAlive) { m_WorkerThreads.RemoveAt(0); continue; }

				m_WorkerThreads[0].Join();
				m_WorkerThreads.RemoveAt(0);
			}

			// Output Answer
			Console.WriteLine("=================================================");
			Console.WriteLine("Found Answer:");
			Console.WriteLine(m_Input + m_Answer + " - " + m_AnswerHash);

			// Halt Exit
			Console.ReadKey();
		}
	}

	class Worker
	{
		int m_Guess;

		public Worker(int a_GuessOffset)
		{
			m_Guess = a_GuessOffset;
		}

		public void ThreadProcess()
		{
			using (MD5 hash = MD5.Create())
			{
				while (Program.m_IsGuessing)
				{
					byte[] output = hash.ComputeHash(Encoding.UTF8.GetBytes(Program.m_Input + m_Guess));

					StringBuilder guess = new StringBuilder();

					foreach (byte character in output) { guess.Append(character.ToString("x2")); }

					if (guess.ToString().Substring(0, 6) == "000000")
					{
						Program.m_AnswerHash = guess.ToString();
						Program.m_Answer = m_Guess;
						Program.m_IsGuessing = false;
					}

					m_Guess += Program.m_ThreadCount;
				}
			}
		}
	}
}
