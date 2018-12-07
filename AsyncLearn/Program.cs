﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncLearn
{
    /// <summary>
    /// Calling async method in sync context.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Demonstrates calling async method in sync context.
        /// </summary>
        static void Main(string[] args)
        {
            try
            {
                // var text = GetContent().Result; // This wraps in System.AggregateException.
                var text = GetContent().GetAwaiter().GetResult(); // This does not wrap exceptions.
                Console.WriteLine($"Content: '{text}'");
            } catch (Exception e)
            {
                Console.WriteLine($"{e.GetType()}: {e.Message}");
            }

            Console.WriteLine("Press key...");
            Console.ReadKey();
        }

        /// <summary>
        /// Example of IO bound async method using await.
        /// </summary>
        private static async Task<string> GetContent()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("http://127.0.0.1:8080/foo");
            return response.ToUpper();
        }
    }
}
