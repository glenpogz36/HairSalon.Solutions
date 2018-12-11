using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Test
{
  [TestClass]
    public class ClientTests : IDisposable
    {
        public void Dispose()
        {
            Client.DeleteAll();
        }
        public ClientTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=glen_sale;";
        }

      [TestMethod]
      public void GetAll_DbStartsEmpty_0()
      {
        //Arrange
        //Act
        int result = Client.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
      }
  }
}
