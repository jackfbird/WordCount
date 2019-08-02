using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using WordCount.Models;
using WordCount.Dal.Interfaces;
using Microsoft.AspNetCore.Hosting.Internal;

namespace WordCount.Dal
{
    public class OutputDal : IOutputDal
    {
        int numberOfWords = 0;
        int numberOfSentences = 0;
        string paragraph = "";
        readonly string fileName = "App_data\\Paragraph.txt";
        string newWord;

        // Gets Count of each word
        public IDictionary<string, int> GetCountByWord()
        {
            IDictionary<string, int> WordOutput = new Dictionary<string, int>();

            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string editedLine = line.Replace("  ", " ");

                        if (line != "")
                        {
                            List<string> wordsInLine = new List<string>();
                            wordsInLine.AddRange(editedLine.Split(" ")); // lines are split by spaces to get each 'word'
                            paragraph += editedLine;

                            // Only counts words that have a letter in them
                            foreach (string word in wordsInLine)
                            {
                                newWord = word;
                                var charsToRemove = new string[] { "@", ",", ".", ";", "'", "?", "\"" };
                                foreach (var c in charsToRemove)
                                {
                                    newWord = newWord.Replace(c, string.Empty);
                                }

                                if (Regex.Matches(newWord, @"[a-zA-Z]").Count != 0)
                                {
                                    if (WordOutput.Keys.Contains(newWord))
                                    {
                                        WordOutput[newWord] = (WordOutput[newWord] + 1);
                                    }
                                    else
                                    {
                                        WordOutput.Add(newWord, 1);
                                    }
                                }
                            }
                        }
                        else
                        {
                            List<string> sentencesInLine = new List<string>();
                            sentencesInLine.AddRange(paragraph.Split(new Char[] { '.', '!', '?' }));
                            numberOfSentences += sentencesInLine.Count;
                            paragraph = "";
                        }
                    }
                }
            }
            catch (IOException e) //catch an IO Exception
            {
                string message = "Error(135): " + e.Message;
            }
            return WordOutput;
        }

        // Gets Total Word & Sentence Count
        public Output GetWordsAndSentences()
        {
            IDictionary<string, int> WordOutput = new Dictionary<string, int>();
            Output textoutput = new Output();

            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string editedLine = line.Replace("  ", " ");


                        if (line != "")
                        {
                            List<string> wordsInLine = new List<string>();
                            wordsInLine.AddRange(editedLine.Split(" ")); // lines are split by spaces to get each 'word'
                            paragraph += editedLine;

                            // Only counts words that have a letter in them
                            foreach (string word in wordsInLine)
                            {
                                newWord = word;
                                var charsToRemove = new string[] { "@", ",", ".", ";", "'", "?", "\"" };
                                foreach (var c in charsToRemove)
                                {
                                    newWord = newWord.Replace(c, string.Empty);
                                }

                                if (Regex.Matches(newWord, @"[a-zA-Z]").Count != 0)
                                {
                                    if (WordOutput.Keys.Contains(newWord))
                                    {
                                        WordOutput[newWord] = (WordOutput[newWord] + 1);
                                    }
                                    else
                                    {
                                        WordOutput.Add(newWord, 1);
                                    }
                                }
                                numberOfWords++;
                            }
                        }
                        else
                        {
                            List<string> sentencesInLine = new List<string>();
                            sentencesInLine.AddRange(paragraph.Split(new Char[] { '.', '!', '?' }));
                            numberOfSentences += sentencesInLine.Count;
                            paragraph = "";
                        }
                        textoutput.WordCount = numberOfWords;
                        textoutput.SentenceCount = numberOfSentences;
                    }
                }
            }
            catch (IOException e) //catch an IO Exception
            {
                string message = "Error(135): " + e.Message;
            }

            return textoutput;
        }


    }
}












