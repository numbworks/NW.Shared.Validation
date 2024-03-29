﻿using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NW.Shared.Validation.UnitTests
{
    [TestFixture]
    public class ValidatorTests
    {

        #region Fields

        private static TestCaseData[] validateLengthExceptionTestCases =
        {

            // ValidateLength<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateLength<Exception>(0)
                    ),
                typeof(Exception),
                new Exception(
                        MessageCollection.VariableCantBeLessThan(ObjectMother.VariableName_Length, 1)).Message
                ).SetArgDisplayNames($"{nameof(validateLengthExceptionTestCases)}_01"),

            // ValidateLength
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateLength(0)
                    ),
                typeof(ArgumentException),
                new ArgumentException(
                        MessageCollection.VariableCantBeLessThan(ObjectMother.VariableName_Length, 1)).Message
                ).SetArgDisplayNames($"{nameof(validateLengthExceptionTestCases)}_02")

        };
        private static TestCaseData[] validateObjectExceptionTestCases =
        {

            // ValidateObject<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateObject<ArgumentException>(null, ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentException),
                new ArgumentException(ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateObjectExceptionTestCases)}_01"),

            // ValidateObject
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateObject(null, ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateObjectExceptionTestCases)}_02")

        };
        private static TestCaseData[] validateArrayExceptionTestCases =
        {

            // ValidateArrayNull
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateArray<string>(
                                null,
                                ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateArrayExceptionTestCases)}_01"),

            // ValidateArrayEmpty
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateArray(
                                Array.Empty<string>(),
                                ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentException),
                MessageCollection.VariableContainsZeroItems(ObjectMother.VariableName_Variable)
                ).SetArgDisplayNames($"{nameof(validateArrayExceptionTestCases)}_02")

        };
        private static TestCaseData[] validateListExceptionTestCases =
        {

            // ValidateListNull
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateList(
                                (List<string>)null,
                                ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateListExceptionTestCases)}_01"),

            // ValidateListEmpty
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateList(
                                new List<string>() { },
                                ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentException),
                MessageCollection.VariableContainsZeroItems(ObjectMother.VariableName_Variable)
                ).SetArgDisplayNames($"{nameof(validateListExceptionTestCases)}_02"),

        };
        private static TestCaseData[] validateStringNullOrWhiteSpaceExceptionTestCases =
        {

            // ValidateStringNullOrWhiteSpace<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrWhiteSpace<Exception>(
                                null,
                                ObjectMother.VariableName_Variable)
                    ),
                typeof(Exception),
                new Exception(ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrWhiteSpaceExceptionTestCases)}_01"),

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrWhiteSpace(
                                null,
                                ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrWhiteSpaceExceptionTestCases)}_02"),

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrWhiteSpace(
                                string.Empty,
                                ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrWhiteSpaceExceptionTestCases)}_03"),

            // ValidateStringNullOrWhiteSpace
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrWhiteSpace(
                                ObjectMother.StringOnlyWhiteSpaces,
                                ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrWhiteSpaceExceptionTestCases)}_04")

        };
        private static TestCaseData[] validateStringNullOrEmptyExceptionTestCases =
        {

            // ValidateStringNullOrEmpty<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrEmpty<Exception>(
                                null,
                                ObjectMother.VariableName_Variable)
                    ),
                typeof(Exception),
                new Exception(ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrEmptyExceptionTestCases)}_01"),

            // ValidateStringNullOrEmpty
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrEmpty(
                                null,
                                ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrEmptyExceptionTestCases)}_02"),

            // ValidateStringNullOrEmpty
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ValidateStringNullOrEmpty(
                                string.Empty,
                                ObjectMother.VariableName_Variable)
                    ),
                typeof(ArgumentNullException),
                new ArgumentNullException(ObjectMother.VariableName_Variable).Message
                ).SetArgDisplayNames($"{nameof(validateStringNullOrEmptyExceptionTestCases)}_03")

        };
        private static TestCaseData[] validateStringNullOrEmptyTestCases =
        {

            new TestCaseData(
                    ObjectMother.String01
                ).SetArgDisplayNames($"{nameof(validateStringNullOrEmptyTestCases)}_01"),

            new TestCaseData(
                    ObjectMother.StringOnlyWhiteSpaces
                ).SetArgDisplayNames($"{nameof(validateStringNullOrEmptyTestCases)}_02")

        };
        private static TestCaseData[] throwIfLessThanOneExceptionTestCases =
        {

            // ThrowIfLessThanOne<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfLessThanOne<Exception>(0, ObjectMother.VariableName_N1)
                    ),
                typeof(Exception),
                MessageCollection.VariableCantBeLessThan(ObjectMother.VariableName_N1, 1)
                ).SetArgDisplayNames($"{nameof(throwIfLessThanOneExceptionTestCases)}_01"),

            // ThrowIfLessThanOne
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfLessThanOne(0, ObjectMother.VariableName_N1)
                    ),
                typeof(ArgumentException),
                MessageCollection.VariableCantBeLessThan(ObjectMother.VariableName_N1, 1)
                ).SetArgDisplayNames($"{nameof(throwIfLessThanOneExceptionTestCases)}_02")

        };
        private static TestCaseData[] throwIfLessThanExceptionTestCases =
        {

            // ThrowIfLessThan<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfLessThan<Exception>((int)0, (int)1, ObjectMother.VariableName_N1)
                    ),
                typeof(Exception),
                MessageCollection.VariableCantBeLessThan(ObjectMother.VariableName_N1, 1)
                ).SetArgDisplayNames($"{nameof(throwIfLessThanExceptionTestCases)}_01"),

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfLessThan<Exception>((double)0, (double)1, ObjectMother.VariableName_N1)
                    ),
                typeof(Exception),
                MessageCollection.VariableCantBeLessThan(ObjectMother.VariableName_N1, 1)
                ).SetArgDisplayNames($"{nameof(throwIfLessThanExceptionTestCases)}_02"),

            // ThrowIfLessThan
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfLessThan((int)0, (int)1, ObjectMother.VariableName_N1)
                    ),
                typeof(ArgumentException),
                MessageCollection.VariableCantBeLessThan(ObjectMother.VariableName_N1, 1)
                ).SetArgDisplayNames($"{nameof(throwIfLessThanExceptionTestCases)}_03"),

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfLessThan((double)0, (double)1, ObjectMother.VariableName_N1)
                    ),
                typeof(ArgumentException),
                MessageCollection.VariableCantBeLessThan(ObjectMother.VariableName_N1, 1)
                ).SetArgDisplayNames($"{nameof(throwIfLessThanExceptionTestCases)}_04")

        };
        private static TestCaseData[] throwIfFirstIsGreaterExceptionTestCases =
        {

            // ThrowIfFirstIsGreater<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfFirstIsGreater<Exception>(
                                4,
                                ObjectMother.VariableName_N1,
                                1,
                                ObjectMother.VariableName_N2)
                    ),
                typeof(Exception),
                MessageCollection.FirstValueIsGreaterThanSecondValue(
                                        ObjectMother.VariableName_N1,
                                        ObjectMother.VariableName_N2
                                    )
                ).SetArgDisplayNames($"{nameof(throwIfFirstIsGreaterExceptionTestCases)}_01"),

            // ThrowIfFirstIsGreater
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfFirstIsGreater(
                                4,
                                ObjectMother.VariableName_N1,
                                1,
                                ObjectMother.VariableName_N2)
                    ),
                typeof(ArgumentException),
                MessageCollection.FirstValueIsGreaterThanSecondValue(
                                        ObjectMother.VariableName_N1,
                                        ObjectMother.VariableName_N2
                                    )
                ).SetArgDisplayNames($"{nameof(throwIfFirstIsGreaterExceptionTestCases)}_02")

        };
        private static TestCaseData[] throwIfFirstIsGreaterOrEqualExceptionTestCases =
                {

            // ThrowIfFirstIsGreater<T>
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfFirstIsGreaterOrEqual<Exception>(
                                4,
                                ObjectMother.VariableName_N1,
                                1,
                                ObjectMother.VariableName_N2)
                    ),
                typeof(Exception),
                MessageCollection.FirstValueIsGreaterOrEqualThanSecondValue(
                                        ObjectMother.VariableName_N1,
                                        ObjectMother.VariableName_N2
                                    )
                ).SetArgDisplayNames($"{nameof(throwIfFirstIsGreaterOrEqualExceptionTestCases)}_01"),

            // ThrowIfFirstIsGreater
            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfFirstIsGreaterOrEqual(
                                4,
                                ObjectMother.VariableName_N1,
                                1,
                                ObjectMother.VariableName_N2)
                    ),
                typeof(ArgumentException),
                MessageCollection.FirstValueIsGreaterOrEqualThanSecondValue(
                                        ObjectMother.VariableName_N1,
                                        ObjectMother.VariableName_N2
                                    )
                ).SetArgDisplayNames($"{nameof(throwIfFirstIsGreaterOrEqualExceptionTestCases)}_02")

        };
        private static TestCaseData[] throwIfModuloIsNotZeroExceptionTestCases =
        {

            new TestCaseData(
                new TestDelegate(
                        () => Validator.ThrowIfModuloIsNotZero<Exception>(
                                value1: 5,
                                variableName1: ObjectMother.VariableName_N1,
                                value2: 2,
                                variableName2: ObjectMother.VariableName_N2)
                    ),
                typeof(Exception),
                MessageCollection.DividingMustReturnWholeNumber(ObjectMother.VariableName_N1, ObjectMother.VariableName_N2)
                ).SetArgDisplayNames($"{nameof(throwIfModuloIsNotZeroExceptionTestCases)}_01")

        };

        #endregion

        #region SetUp
        #endregion

        #region Tests

        [TestCaseSource(nameof(validateLengthExceptionTestCases))]
        public void ValidateLength_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(validateObjectExceptionTestCases))]
        public void ValidateObject_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(validateArrayExceptionTestCases))]
        public void ValidateArray_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(validateListExceptionTestCases))]
        public void ValidateList_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(validateStringNullOrWhiteSpaceExceptionTestCases))]
        public void ValidateStringNullOrWhiteSpace_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(validateStringNullOrEmptyExceptionTestCases))]
        public void ValidateStringNullOrEmpty_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(throwIfLessThanOneExceptionTestCases))]
        public void ThrowIfLessThanOne_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(throwIfLessThanExceptionTestCases))]
        public void ThrowIfLessThan_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(throwIfFirstIsGreaterExceptionTestCases))]
        public void ThrowIfFirstIsGreater_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(throwIfFirstIsGreaterOrEqualExceptionTestCases))]
        public void ThrowIfFirstIsGreaterOrEqual_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);

        [TestCaseSource(nameof(throwIfModuloIsNotZeroExceptionTestCases))]
        public void ThrowIfModuloIsNotZero_ShouldThrowACertainException_WhenUnproperArguments
            (TestDelegate del, Type expectedType, string expectedMessage)
                => ObjectMother.Method_ShouldThrowACertainException_WhenUnproperArguments(del, expectedType, expectedMessage);


        [Test]
        public void ValidateLength_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ValidateLength(ObjectMother.Length01),
                        () => Validator.ValidateLength<ArgumentException>(ObjectMother.Length01)
                    });

        [Test]
        public void ValidateObject_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ValidateObject(ObjectMother.Object01, ObjectMother.VariableName_Variable),
                        () => Validator.ValidateObject<ArgumentException>(ObjectMother.Object01, ObjectMother.VariableName_Variable)
                    });

        [Test]
        public void ValidateArray_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ValidateArray(ObjectMother.Array01, ObjectMother.VariableName_Variable)
                    });

        [Test]
        public void ValidateList_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ValidateList(ObjectMother.List01, ObjectMother.VariableName_Variable)
                    });

        [Test]
        public void ValidateStringNullOrWhiteSpace_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ValidateStringNullOrWhiteSpace(ObjectMother.String01, ObjectMother.VariableName_Variable)
                    });

        [TestCaseSource(nameof(validateStringNullOrEmptyTestCases))]
        public void ValidateStringNullOrEmpty_ShouldDoNothing_WhenProperArgument(string str)
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ValidateStringNullOrEmpty(str, ObjectMother.VariableName_Variable)
                    });

        [Test]
        public void ThrowIfFirstIsGreaterOrEqual_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ThrowIfFirstIsGreaterOrEqual(4, "n1", 5, "n2")
                    });

        [Test]
        public void ThrowIfFirstIsGreater_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ThrowIfFirstIsGreater(3, "n1", 4, "n2")
                    });

        [Test]
        public void ThrowIfLessThanOne_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ThrowIfLessThanOne(ObjectMother.Value, nameof(ObjectMother.Value))
                    });

        [Test]
        public void ThrowIfLessThan_ShouldDoNothing_WhenProperArgumentAndInteger()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ThrowIfLessThan(
                                (int)ObjectMother.Value, 
                                (int)ObjectMother.Value, 
                                nameof(ObjectMother.Value))
                    });

        [Test]
        public void ThrowIfLessThan_ShouldDoNothing_WhenProperArgumentAndDouble()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ThrowIfLessThan(
                                (double)ObjectMother.Value,
                                (double)ObjectMother.Value,
                                nameof(ObjectMother.Value))
                    });

        [Test]
        public void ThrowIfModuloIsNotZero_ShouldDoNothing_WhenProperArgument()
            => Method_ShouldDoNothing_WhenProperArgument(
                    new Action[] {
                        () => Validator.ThrowIfModuloIsNotZero(
                                value1: 4,
                                variableName1: ObjectMother.VariableName_N1,
                                value2: 1,
                                variableName2: ObjectMother.VariableName_N2)
                    });

        #endregion

        #region TearDown
        #endregion

        #region Support_methods

        public void Method_ShouldDoNothing_WhenProperArgument(Action[] actions)
        {

            try
            {

                // Arrange
                // Act
                foreach (Action action in actions)
                    action();

            }
            catch (Exception ex)
            {

                // Assert
                Assert.Fail(ex.Message);

            }

        }

        #endregion

    }
}

/*
    Author: numbworks@gmail.com
    Last Update: 10.02.2024
*/
