// *****************************************************************************
// File:       Method.cs
// Solution:   Aspects
// Project:    Aspects
// Date:       10/29/2020
// Author:     Latency McLaughlin
// Copywrite:  Bio-Hazard Industries - 1998-2020
// *****************************************************************************

using System;
using System.Reflection;
using System.Threading;
using Aspects.Delegates;

namespace Aspects.Aspects {
    public sealed class Method<T> {
        private Predicate<MethodInfo> _filter = f => true;
        private static Method<T> _parent;
        // ReSharper disable once StaticMemberInGenericType
        private static object _decorated;

        public Action<MethodInfo, object[]> OnBefore { private get; set; }
        private event ProxyEventHandler EvtBefore;
        public Action<MethodInfo, object[]> OnAfter { private get; set; }
        private event ProxyEventHandler EvtAfter;
        public Action<MethodInfo, object[]> OnError { private get; set; }
        private event ProxyEventHandler EvtError;

        public Predicate<MethodInfo> Filter
        {
            get => _filter;
            set => _filter = value ?? (m => true);
        }

        public TInterface Create<TInterface, TClass>(TClass target) where TClass : class
        {
            _parent = this;
            _decorated = target;
            return DispatchProxy.Create<TInterface, AuthenticationProxy>();
        }



        /// <summary>
        ///     DynamicProxy
        /// </summary>
        public class DynamicProxy : DispatchProxy
        {
            public DynamicProxy() {
                _parent.EvtBefore += OnBeforeExecute;
                _parent.EvtAfter  += OnAfterExecute;
                _parent.EvtError  += OnErrorExecuting;
            }

            private static void OnBeforeExecute(MethodInfo targetMethod, object[] args) {
                if (_parent.OnBefore == null) return;
                if (_parent._filter(targetMethod))
                    _parent.OnBefore(targetMethod, args);
            }

            private static void OnAfterExecute(MethodInfo targetMethod, object[] args)
            {
                if (_parent.OnAfter == null) return;
                if (_parent._filter(targetMethod))
                    _parent.OnAfter(targetMethod, args);
            }

            private static void OnErrorExecuting(MethodInfo targetMethod, object[] args)
            {
                if (_parent.OnError == null) return;
                if (_parent._filter(targetMethod))
                    _parent.OnError(targetMethod, args);
            }

            protected override object Invoke(MethodInfo targetMethod, object[] args)
            {
                if (targetMethod == null)
                    throw new ArgumentException(nameof(targetMethod));

                try {
                    _parent.EvtBefore?.Invoke(targetMethod, args);
                    var result = targetMethod.Invoke(_decorated, args);
                    _parent.EvtAfter?.Invoke(targetMethod, args);
                    return result;
                } catch (Exception) {
                    _parent.EvtError?.Invoke(targetMethod, args);
                    return default;
                }
            }
        }


        /// <summary>
        ///     AuthenticationProxy
        /// </summary>
        public class AuthenticationProxy : DynamicProxy
        {
            private static void Log(string msg, object arg = null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(msg, arg);
                Console.ResetColor();
            }

            protected override object Invoke(MethodInfo targetMethod, object[] args)
            {
                 if (targetMethod == null)
                    throw new ArgumentException(nameof(targetMethod));

                 if (Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.IsInRole("ADMIN")) {
                     try {
                         Log($"User authenticated - You can execute '{targetMethod.Name}'");
                         return base.Invoke(targetMethod, args);
                     } catch (Exception e) {
                         Log($"User authenticated - Exception {e} executing '{targetMethod.Name}'");
                         return default;
                     }
                 }

                 Log($"User not authenticated - You can't execute '{targetMethod.Name}'");
                 return default;
            }
        }
    }
}