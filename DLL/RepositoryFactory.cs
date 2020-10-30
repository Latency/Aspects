// ****************************************************************************
// File:       RepositoryFactory.cs
// Solution:   Aspects
// Project:    Aspects
// Date:       10/23/2020
// Author:     Latency McLaughlin
// Copywrite:  Bio-Hazard Industries - 1998-2020
// *****************************************************************************

using System;
using Aspects.Aspects;
using Aspects.DTO;
using Aspects.Interfaces;

namespace Aspects
{
    public static class RepositoryFactory
    {
        private static void Log(string msg, object arg = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg, arg);
            Console.ResetColor();
        }


        public static IRepository<T> Create<T>()
        {
            var aspect = new Method<T> {
                OnBefore = (s, e) => Log($"In {nameof(RepositoryFactory)} - Before executing '{s.Name}'"),
                OnAfter  = (s, e) => Log($"In {nameof(RepositoryFactory)} - After executing '{s.Name}'"),
                OnError  = (s, e) => Log($"In {nameof(RepositoryFactory)} - Error executing '{s.Name}'"),
                Filter   = m => !m.Name.StartsWith("Get")
            };
            return aspect.Create<IRepository<T>, Repository<T>>(new Repository<T>());
        }
    }
}