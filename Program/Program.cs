using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        string[] sentences = { "Это первое предложение", "Это второе предложение", "Это третье предложение" };
        char searchChar = 'о';

        int totalCount = 0; // Общее количество заданной буквы в предложениях

        Thread[] threads = new Thread[sentences.Length]; // Массив потоков

        for (int i = 0; i < sentences.Length; i++)
        {
            int index = i;

            threads[i] = new Thread(() =>
            {
                int countInSentence = GetLetterCount(sentences[index], searchChar);
                Interlocked.Add(ref totalCount, countInSentence); 
            });

            threads[i].Start();
        }

        for (int i = 0; i < sentences.Length; i++)
        {
            threads[i].Join(); 
        }

        Console.WriteLine("Общее количество букв '{0}': {1}", searchChar, totalCount);
    }

    static int GetLetterCount(string sentence, char searchChar)
    {
        int count = 0;

        foreach (char c in sentence)
        {
            if (c == searchChar)
            {
                count++;
            }
        }

        return count;
    }
}
