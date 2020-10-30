// ****************************************************************************
// File:       Repository.cs
// Solution:   Aspects
// Project:    Aspects
// Date:       10/23/2020
// Author:     Latency McLaughlin
// Copywrite:  Bio-Hazard Industries - 1998-2020
// *****************************************************************************

using System;
using System.Collections.Generic;
using Aspects.Interfaces;

namespace Aspects.DTO
{
    public class Repository<T> : IRepository<T>
    {
        public void Add(T entity)
        {
            Console.WriteLine("Adding {0}", entity);
        }

        public void Delete(T entity)
        {
            Console.WriteLine("Deleting {0}", entity);
        }

        public void Update(T entity)
        {
            Console.WriteLine("Updating {0}", entity);
        }

        public IEnumerable<T> GetAll()
        {
            Console.WriteLine("Getting entities");
            return null;
        }

        public T GetById(int id)
        {
            Console.WriteLine("Getting entity {0}", id);
            return default;
        }
    }
}