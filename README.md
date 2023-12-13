# Calculator Desktop App

## Overview

This desktop application is a versatile calculator developed in .NET using C#. It provides a user-friendly interface for performing various arithmetic operations, including addition, subtraction, multiplication, and division. The project follows a modular and extensible design, allowing easy addition of new operators and functionalities.

## Table of Contents

- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
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
