# Assessment

## Description

Assessment completed by Jovan

## Prerequisites

Before you begin, ensure you have met the following requirements:

- [.NET 8](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) with [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)

## Installation Instructions

To install and run this project on your local machine, follow these steps:

1. Clone the repository to your local machine:

   ```bash
   git clone https://github.com/doctorgucci/Spinnaker.Assessment.git

2. Open the solution file in Visual Studio 2022.

3. Build the solution to restore NuGet packages.

4. After succesfull build, follow the next steps

## Database Installation

To set up the database for this project, follow these steps:

1. Open SQL Server Management Studio.

2. Connect to your SQL Server instance.

3. In the solution - locate the JovanAssessment.bacpac file under the Database Solution folder.

4. In SQL Server Management Studio - Right Click Database - Select Import Data-Tier application - Select the "JovanAsessment.bacpac" file and complete the import.

5. The database should now be imported will all country data populated - I did not feel like seeding all those countries.

##Usage

To use this project:

1. Run the application. Both the API and ASP.NET Core applications will launch. I recommend using IIS Express but you are more than welcome to use whatever you prefer.
2. Ensure that the connection string in appsettings.json (Transport> Spinnaker.Assessment.WebAPI) matches your SQL Server instance. This the Entity Framework connection string.
    ```json "ConnectionStrings": {
    "DatabaseConnection": "Data Source=localhost; Initial Catalog=JovanAssessment;Integrated Security=true;TrustServerCertificate=True" } 
  If your data source is not localhost - please change it to the correct name as per your machine/setup.

3. Make sure that the URL for the API is the same as the ApiSettings.BaseUrl in the ASP.NET Core appSettings file. (Spinnaker.Assessment.UI)
   ```json
   "ApiSettings": {
    "BaseUrl": "https://localhost:44376/api/"} 
  If the launch url for the API which contains swagger endpoints is different - please replace the /localhost:port/ section.





