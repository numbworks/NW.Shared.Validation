﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Reflection;
using NW.Shared.Validation.UnitTests.Utilities;

namespace NW.Shared.Validation.UnitTests
{
    public static class ObjectMother
    {

        #region Properties

        public static string[] Array01 = new[] { "Dodge", "Datsun", "Jaguar", "DeLorean" };
        public static Car Object01 = new Car()
        {
            Brand = "Dodge",
            Model = "Charger",
            Year = 1966,
            Price = 13500,
            Currency = "USD"
        };
        public static uint Length01 = 3;
        public static string VariableName_Variable = "variable";
        public static string VariableName_Length = "length";
        public static string VariableName_N1 = "n1";
        public static string VariableName_N2 = "n2";
        public static List<string> List01 = Array01.ToList();
        public static uint Value = Length01;
        public static string String01 = "Dodge";
        public static string StringOnlyWhiteSpaces = "   ";

        #endregion

        #region Methods

        public static void Method_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
        {

            // Arrange
            // Act
            // Assert
            Exception actual = Assert.Throws(expectedType, del);
            Assert.That(actual.Message, Is.EqualTo(expectedMessage));

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 10.02.2024
*/