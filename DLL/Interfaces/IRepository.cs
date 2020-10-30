// ****************************************************************************
// File:       IRepository.cs
// Solution:   Aspects
// Project:    Aspects
// Date:       10/23/2020
// Author:     Latency McLaughlin
// Copywrite:  Bio-Hazard Industries - 1998-2020
// *****************************************************************************

using System.Collections.Generic;

namespace Aspects.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}