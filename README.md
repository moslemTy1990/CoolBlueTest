# CoolBlue

Welcome to CoolBlue Tesing Project.

This project was created with [Rider]([https://visualstudio.microsoft.com/vs/](https://www.jetbrains.com/rider/)).

This API will contain all relevant entry points which is mentioned in the proposal document except Task 5.

## TOC
- [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Installing & Local Development](#installing--local-development)
- [Files/Folder Structure](#filesfolders-structure)
- [Code Structure and Conventions](#code-structure-and-conventions)
- [Tests](#Tests)
- [Built With](#built-with)
- [Changelog](#changelog)
- [Authors](#authors)


## Getting Started
In order to run **Insurance API** on your local machine all what you need to do is
to have the prerequisites stated below installed on your machine and follow the installation steps down below.


#### Prerequisites
- .NET Core 6

#### Installing & Local Development
Start by typing the following commands in your terminal in order to get **API** up and running.

```
> dotnet build
> dotnet run
```


## Files/Folders Structure
Here is a brief explanation of the template folder structure and some of its main files usage:

```
CoolBlueTest                      # Contains all sourse and test folder projectrs
└──Insurance API                  # Contains source folder for the insurance API.
│   └── Controllers               # Contains the controllers and routs for the insurance API
│   │   
│   │── Extensions                # Contains the Extension methods for DI     
|   |
│   │── Interfaces                # Interfaces for ISegregation
│   │── Logic                     # The main strategies
│   └── Models                    # class models
│   └── Properties                # the ENV variables
|
└──Insurance API  Tests           # Contains source folder for testing the insurance API.
│   └── *.cs                      # Class test files
└── .gitignore                    # Ignored files in Git.
└── README.md                     # Manual file.

```

# Code Structure and Conventions
For the most part we will be using the standard naming conventions as Microsoft (specific documentation can be found at https://docs.microsoft.com/en-us/dotnet/standard/ and at https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines ).
If there is a specific reason it needs to be added to the code documentation.

Strategy pattern is used for dealing with differency Insurance policies.

Basic naming and explanation for specific structures:
- Extension(s) -> Extension methods used for specific coupling of functionality to existing objects. Example: StartupHelperExtension.


## Tests
XUnit test is used for unit testing

## Built With
- [ASP.NET Core 6 ](https://dotnet.microsoft.com/en-us/apps/aspnet/)
- Strategy Pattern

## Changelog
#### V 1.0.0
Initial Release

## Authors
Cool Blue
Moslem Teymoori
