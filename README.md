# Calculator Desktop App

## Overview

This desktop application is a versatile calculator developed in .NET using C#. It provides a user-friendly interface for performing various arithmetic operations, including addition, subtraction, multiplication, and division. The project follows a modular and extensible design, allowing easy addition of new operators and functionalities.

## Table of Contents

- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Running the Application](#running-the-application)
- [Features](#features)
- [Configuration](#configuration)
- [Contributing](#contributing)
- [License](#license)

## Project Structure

### CalculatorApp (Main Project)
- **Entry Point:** `Program.cs`
- Initializes and runs the calculator application.

### OperationsLibrary
- **Interfaces:**
  - `IOperations.cs`: Common methods and properties for arithmetic operations.
- **Abstract Classes:**
  - `BinaryOperation.cs`: Implements `IOperations` for binary operations.
- **Concrete Operation Classes:**
  - `Add.cs`, `Subtract.cs`, `Multiply.cs`, etc.: Inherit from `BinaryOperation` and implement specific logic.
- **Utility Classes:**
  - `PredefinedOperator.cs`: Manages a dictionary of operators and provides methods for adding new operators and retrieving operator classes.
  - `Parser.cs`: Handles tokenization and expression parsing.
  - `Token.cs`: Represents individual tokens in expressions.
- **Configuration File:**
  - `OperatorDatabase.json`: Contains operator configurations.

### CalculatorApp.Tests (Optional)
- `UnitTest1.cs`: Unit tests for core functionalities.

### Resources (Optional)
- `error_messages.resx`: Localized error messages.

## Getting Started

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/meeran2021/desktop-calculator-application.git
   ```

2. **Open the Solution in Visual Studio:**
   - Navigate to the `CalculatorApp` directory and open the solution file (`CalculatorApp.sln`) in Visual Studio.

3. **Build and Run:**
   - Build the solution and run the `CalculatorApp` project.

## Running the Application

Follow these steps to run the Calculator Desktop App:

1. **Clone the Repository:**
Open a terminal or command prompt and execute the following command to clone the repository to your local machine:
 ```bash
   git clone https://github.com/meeran2021/desktop-calculator-application.git
   ```

2. **Navigate to the Project Directory:**
Change into the project directory:
```bash
  cd desktop-calculator-application\CalculatorApp
  ```

3. **Build and Run:**
Build the solution and run the application using the following commands:
```bash
  dotnet build
  dotnet run --project CalculatorApp
  ```
This will build the application and start it, allowing you to interact with the calculator.

4. **Use the Calculator:**
Once the application is running, you will get access to the calculator interface. Perform various arithmetic operations and explore the features provided by the calculator.

Now you have the Calculator Desktop App up and running without opening the code in an editor. Enjoy using the calculator!

## Features

- Input expressions using the calculator interface.
- Supports basic arithmetic operations, parentheses, and more.
- Modular design for easy addition of new operators and functionalities.

## Configuration

- Modify the `OperatorDatabase.json` file to add or update operator configurations.
- Customize error messages in the `error_messages.resx` file.

## Contributing

Feel free to contribute by opening issues, providing feedback, or submitting pull requests. Follow the [contribution guidelines](CONTRIBUTING.md) for details.

## License

This project is licensed under the [MIT License](LICENSE).
