# Dotnet Web API Project Skeleton Generator

Welcome to the Dotnet Web API Project Skeleton Generator! This tool helps you quickly set up a new Dotnet Web API project with a clean and organized structure. You can choose between generating a multilayer project or a single-layer project.

## Features

- **Global Exception Middleware**: Provides a global exception handling mechanism to ensure consistent error responses.
- **ResponseHelper**: Simplifies controller code and enhances readability by reducing repetitive code.
- **Serilog Integration**: Adds robust logging capabilities with Serilog, ensuring proper logging throughout your application.
- **Default DB Connection Configuration**: Pre-configured database connection settings in `appsettings.json`.
- **DapperContext**: Automatically creates a database connection using Dapper, eliminating the need for manual configuration.

## Steps to Generate a Project

1. **Run the Executable**: Start the tool by running the executable.

2. **Enter Project Name**: Provide a name for your new project.

    ![Enter Project Name](Images/ChooseProject1.png)

3. **Choose Project Type**:
    - **1** for Multilayer Project
    - **2** for Single Layer Project

    ![Choose Project Type](Images/ChooseType2.png)

4. **Generate the Solution**: The tool will generate the necessary files and structure for your chosen project type.

    ![Generate Solution](Images/GenerateSolution3.png)

