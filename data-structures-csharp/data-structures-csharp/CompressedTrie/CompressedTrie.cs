﻿using System;
using System.Diagnostics.Contracts;


namespace DataStructures.CompressedTrieSpace
{
    /// <summary>
    /// Compressed trie which saves node space by compressing non branching
    /// nodes into one node
    /// </summary>
    [Serializable]
    public class CompressedTrie
    {
        public Node Root { get; private set; }


        public CompressedTrie()
        {
            Root = new NullNode("");
        }


        /// <summary>
        /// Check if an word exists
        /// </summary>
        /// <param name="word">word to search for</param>
        /// <returns>True if that word exists</returns>
        public bool Exists(string word)
        {
            Contract.Requires(string.IsNullOrEmpty(word), "Trie doesn't include empty string or null values");

            Node current = Root;
            int count = 0;
            for (int i = 0; i < word.Length; i++, count++)
            {
                if (current.MoveToChildren(word.Substring(i)))
                {
                    current = current.GetChild(word.Substring(i));
                }
                else
                {
                    break;
                }
            }
            return (count == word.Length);
        }

        /// <summary>
        /// Adds a word if it doesn't exist
        /// </summary>
        /// <param name="word"></param>
        public void Add(string word)
        {
            Contract.Requires(string.IsNullOrEmpty(word), "Trie doesn't include empty string or null values");

            Node current = Root;
            int count = 0;
            for (int i = 0; i < word.Length; i++, count++)
            {
                if (current.MoveToChildren(word.Substring(i)))
                {
                    current = current.GetChild(word.Substring(i));
                }
                else
                {
                    current.AddChild(word.Substring(i));
                }
            }

            if (count == word.Length)
            {
                current.AddNullNode();
            }
        }
    }
}
