// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System;
// using System.Collections.Generic;
// using HairSalon.Models;
// using Microsoft.AspNetCore.Mvc;
//
// namespace HairSalon.Test
// {
//   [TestClass]
//     public class EmployeeTests : IDisposable
//     {
//         public void Dispose()
//         {
//             Employee.DeleteAll();
//         }
//
//         public EmployeeTests()
//         {
//             DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=mark_mangahas_tests;";
//         }
//
//       [TestMethod]
//       public void GetAll_DbStartsEmpty_0()
//       {
//         //Arrange
//         //Act
//         int result = Employee.GetAll().Count;
//
//       //Assert
//       Assert.AreEqual(0, result);
//       }
//     }
//   }
