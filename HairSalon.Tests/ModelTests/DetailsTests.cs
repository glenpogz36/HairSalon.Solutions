// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using System;
// using System.Collections.Generic;
// using HairSalon.Models;
// using Microsoft.AspNetCore.Mvc;
//
// namespace HairSalon.Test
// {
//   [TestClass]
//     public class SpecialtyTests : IDisposable
//     {
//         public void Dispose()
//         {
//             Specialty.DeleteAll();
//         }
//         public SpecialtyTests()
//         {
//             DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=mark_mangahas_tests;";
//         }
//
//       [TestMethod]
//       public void GetAll_DbStartsEmpty_0()
//       {
//         //Arrange
//         //Act
//         int result = Specialty.GetAll().Count;
//
//       //Assert
//       Assert.AreEqual(0, result);
//       }
//     }
//   }
