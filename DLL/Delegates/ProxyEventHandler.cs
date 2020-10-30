// *****************************************************************************
// File:       ProxyEventHandler.cs
// Solution:   Aspects
// Project:    Aspects
// Date:       10/29/2020
// Author:     Latency McLaughlin
// Copywrite:  Bio-Hazard Industries - 1998-2020
// *****************************************************************************

using System.Reflection;

namespace Aspects.Delegates {
    public delegate void ProxyEventHandler(MethodInfo method, object[] args);
}