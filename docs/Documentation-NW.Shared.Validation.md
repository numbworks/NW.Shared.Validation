# NW.Shared.Validation
Contact: numbworks@gmail.com

## Revision History

| Date | Author | Description |
|---|---|---|
| 2024-02-10 | numbworks | Created. |

## Introduction

`NW.Shared.Validation` is a library that provides a collection of general-purpose validation methods.

## Getting Started

In order to use the library:

1. download and install the library in your project via NuGet - for ex. by using the Visual Studio UI or the .NET CLI:

```
dotnet add package NW.Shared.Validation --version 1.0.0
```

2. reference and start using the library:

```csharp
using System;
using NW.Shared.Validation;

/* ... */

public SettingBag(double forecastingDenominator, string folderPath, uint roundingDigits)
{

    Validator.ThrowIfLessThan(forecastingDenominator, DefaultForecastingDenominator, nameof(forecastingDenominator));
    Validator.ValidateStringNullOrWhiteSpace(folderPath, nameof(folderPath));
    Validator.ThrowIfFirstIsGreater((int)roundingDigits, nameof(roundingDigits), (int)DefaultRoundingDigits, nameof(DefaultRoundingDigits));

    ForecastingDenominator = forecastingDenominator;
    FolderPath = folderPath;
    RoundingDigits = roundingDigits;

}

/* ... */
```

3. (optional) unit test it by using the `MessageCollection` class:

```csharp
using System;
using NW.Shared.Validation;

/* ... */

private static TestCaseData[] settingBagExceptionTestCases =
{

    new TestCaseData(
        new TestDelegate(
            () => new SettingBag(
                            forecastingDenominator: 0,
                            folderPath: SettingBag.DefaultFolderPath,
                            roundingDigits: SettingBag.DefaultRoundingDigits
                            )
        ),
        typeof(ArgumentException),
        Validation.MessageCollection.VariableCantBeLessThanDouble(
                "forecastingDenominator",
                SettingBag.DefaultForecastingDenominator)
        ).SetArgDisplayNames($"{nameof(settingBagExceptionTestCases)}_01"),

    /* ... */

    };

[TestCaseSource(nameof(settingBagExceptionTestCases))]
public void SettingBag_ShouldThrowACertainException_WhenUnproperArguments
    (TestDelegate del, Type expectedType, string expectedMessage)
    {

        // Arrange
        // Act
        // Assert
        Exception actual = Assert.Throws(expectedType, del);
        Assert.That(actual.Message, Is.EqualTo(expectedMessage));

    }

```

4. Done!

## Markdown Toolset

Suggested toolset to view and edit this Markdown file:

- [Visual Studio Code](https://code.visualstudio.com/)
- [Markdown Preview Enhanced](https://marketplace.visualstudio.com/items?itemName=shd101wyy.markdown-preview-enhanced)
- [Markdown PDF](https://marketplace.visualstudio.com/items?itemName=yzane.markdown-pdf)
