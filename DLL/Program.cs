// *****************************************************************************
// File:       Program.cs
// Solution:   Aspects
// Project:    Aspects
// Date:       10/29/2020
// Author:     Latency McLaughlin
// Copywrite:  Bio-Hazard Industries - 1998-2020
// *****************************************************************************

using System;
using System.Security.Principal;
using System.Threading;
using Aspects.DTO;

namespace Aspects {
    internal static class Program {
        [STAThread]
        public static void Main(string[] args) {
            try {
                var customerRepository = RepositoryFactory.Create<Customer>();
                var customer = new Customer
                {
                    Id = 1,
                    Name = "Customer 1",
                    Address = "Address 1"
                };
                Console.WriteLine("***{0}Begin program - logging and authentication{0}{0}Running as admin", Environment.NewLine);
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("Administrator"), new[] {"ADMIN"});
                customerRepository.Add(customer);
                customerRepository.Update(customer);
                customerRepository.Delete(customer);
                Console.WriteLine($"{Environment.NewLine}Running as user");
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("NormalUser"), Array.Empty<string>());
                customerRepository.Add(customer);
                customerRepository.Update(customer);
                customerRepository.Delete(customer);
                Console.WriteLine("{0}End program - logging and authentication{0}***", Environment.NewLine);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}