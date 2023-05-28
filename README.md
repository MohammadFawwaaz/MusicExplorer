# MusicExplorer API

## Overview
MusicExplorer is a Web API built on .NET 7, which provides information about artists and releases. The atists details are saved in a SQL Server database and the releases are available by querying MusicBrainz API. 

This solution consists of 2 main projects:
1. ExcelFileToSqlLoader [Console Application] - Loads the artists excel file to SQL Server database
2. MusicExplorer [ASP.NET Core Web API] - Exposes 2 endpoints to make artists and releases available. 

A high level description of the folder structure are as follows:

Postman - Contains the postman collection to query the MusicExplorer API

Scripts - Contains the SQL Server create database and table and artists seeding scripts

src - Contains the different projects in the solution

## Project Structure
The MusicExplorer solution consists of 6 projects:
### 1. ExcelFileToSqlLoader
This project was developed quickly to load the artists present in the Excel file to SQL Server database. The project reads the Excel file (from `docs/` folder), sanitize the data and loads the data to SQL Server in a table named `Artist` using EF Core.

### 2. MusicExplorer
The MusicExplorer API exposes 2 endpoints to query artists and releases. The request is sent to a Handler from the Controller where the request is validated. Then the respective Service is called to fetch the data requested and returned. Finally, in the Controller, the result is paginated and returned.

### 3. MusicExplorer.Common
This is a Class Library project used to share common components across the solution.

### 4. MusicExplorer.Infrastructure
This Class Library project is used to separate the Infrastructure layer from the API, such as, calls to database.

### 5. MusicExplorer.IntegrationTests
This xUnit project consists of integration tests which tests the different components of the application such as: endpoints, database call, 3rd party API call and the services.

### 6. MusicExplorer.UnitTests
This xUnit project consists of unit tests which tests the individual components of the application such as: mappings, validators and paginations.

## Architecture Diagram
The architecture diagram below illustrates the components and their relationships within the application:
![MusicExplorer Architecture Diagram](https://github.com/MohammadFawwaaz/ArtistMusicExplorer/assets/62407416/8c15c8ee-2c10-47d9-8c4f-127c4e8fc1dc)

First, the Console Application is used to load the artists from the excel file to the database. (Represented by number 1 in the architecture diagram)

Second flow, the user access the Artist Search endpoint where the Web API queries the SQL Server database and returns a paginated result. (Represented by number 2, 3, 4, 5)

Third flow, the user access the Artist Release endpoint where the Web API queries the MusicBrainz API and returns a paginated result. (Represented by number 6, 7, 8, 9)

## Testing
To test the MusicExplorer API, in Visual Studio, in the Test Explorer panel, the unit and integration tests are available to be executed.

**Note: To run the Integration tests, most specifically the `MusicBrainzClientTests`, the device must be connected to internet as this particular test connects to the MusicBrainz API.**

# Setup

## Prerequisites
The prerequisites to run this solution requires [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)

## Infrastructure
2) [Visual Studio Community](https://visualstudio.microsoft.com/vs/)
3) [SQL Server Management Studio 2018](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
4) [Postman](https://www.postman.com/downloads/) or a web browser

## Technologies
### Excel File Loader
[EPPlus](https://www.epplussoftware.com/en) (NonCommercial License)

### Music Explorer
EntityFramework Core 7.0.5

FluentValidation

Swagger

Logging (Microsoft default logging - configured to log in Windows Event Log when running on IIS)

Faker (Used in xUnit Tests to generate dummy data)

# Usage

## Database Creation & Seeding
The scripts to create the database and the artist table and the insert statements to seed the artist table are present in the Scripts folder namely: 

`create_database.sql` - create the database if not already exists

`create_table.sql` - create the Artist table

`seed_artists.sql` - seed the Artist table with data from the excel file

## Web API Endpoints
The API endpoints are exposed as follows on:

HTTP - port: 5218

HTTPS - port: 7117

SSL - port: 44387 (IIS Settings)

> {apiUrl} : http:localhost/5218

> {apiUrl} : https:localhost/7117

> {apiUrl} : https:localhost/44387

### Health Check Endpoint
The health check endpoint can be pinged on:

`{apiUrl}/health`

### Get Artists Endpoint
The artists search endpoint can be pinged on:

`{apiUrl}/artist/search/{searchCriteria}/{pageNumber}/{pageSize}`

### Get Releases Endpoint
The releases endpoint can be pinged on:

`{apiUrl}/artist/{artistId}/releases`

In this endpoint, the page number and page size are passed into the request header as follows:

`X-PageNumber`

`X-PageSize`

## Swagger API Documentation
The Swagger UI is exposed on path `/swagger/index.html` when running the API. Both endpoints (artists, releases) exposed are well documented with the ability to try out the request from the UI, including the second endpoint (releases) configured to accept pagination parameters in the request header. Additionally, All the responses returned by the endpoints are documented under each endpoint on the Swagger UI and finally, the sample JSON response is also added to get an idea of the sample response from each endpoint.

### Swagger UI Request
To search for artists:
![SearchEndpoint](https://github.com/MohammadFawwaaz/ArtistMusicExplorer/assets/62407416/9d7098a6-48f7-40ac-83a2-55b360f285ad)

To search for releases:
![ReleaseEndpoint](https://github.com/MohammadFawwaaz/ArtistMusicExplorer/assets/62407416/14aa25be-8ab1-460e-a5db-d08ad067ab40)

# Bonus
1. Added a validation layer to the Handlers using FluentValidation.
2. Added Swagger to the project to provide endpoints documentation and to easily query the endpoints exposed using a web browser.

# Future Work
1. Use PollyRetry to retry database call requests and external API's requests to make MusicExplorer API more resilient.
2. Add a layer in the application where Artists are queried from MusicBrainz API when not available in the SQL Server database.
3. Dockerize the application and add tests coverage.
4. Add more tests

## References
[MusicBrainz API documentation](https://musicbrainz.org/doc/MusicBrainz_API)

The documentation was used to build the URL to query MusicBrainz API to get Releases by Artist MBID.

The URL used to get the artist releases are: https://musicbrainz.org/ws/2/release?artist={artistId}&inc=recordings+labels+artist-credits
