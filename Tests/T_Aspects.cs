// ****************************************************************************
// File:       T_Aspects.cs
// Solution:   Aspects
// Project:    Aspects.UnitTests
// Date:       10/23/2020
// Author:     Latency McLaughlin
// Copywrite:  Bio-Hazard Industries - 1998-2020
// *****************************************************************************

using System;
using System.Security.Principal;
using System.Threading;
using Aspects.DTO;
using NUnit.Framework;

namespace Aspects.UnitTests
{
    [TestFixture]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    // ReSharper disable once InconsistentNaming
    internal class T_Aspects
    {
        /// <summary>
        ///     SetUp
        /// </summary>
        [SetUp]
        public void Setup()
        {
        }


        /// <summary>
        ///     TearDown
        /// </summary>
        [TearDown]
        public void TearDown()
        {
        }


        /// <summary>
        ///     Test
        /// </summary>
        [Test]
        public void Aspect_Test()
        {
            try {
                TestContext.Progress.WriteLine(string.Format("***{0}Begin program - logging and authentication{0}{0}Running as admin", Environment.NewLine));
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("Administrator"), new[] {"ADMIN"});
                var customerRepository = RepositoryFactory.Create<Customer>();
                var customer = new Customer
                {
                    Id = 1,
                    Name = "Customer 1",
                    Address = "Address 1"
                };
                customerRepository.Add(customer);
                customerRepository.Update(customer);
                customerRepository.Delete(customer);
                TestContext.Progress.WriteLine($"{Environment.NewLine}Running as user");
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("NormalUser"), Array.Empty<string>());
                customerRepository.Add(customer);
                customerRepository.Update(customer);
                customerRepository.Delete(customer);
                TestContext.Progress.WriteLine(string.Format("{0}End program - logging and authentication{0}***", Environment.NewLine));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}