# MusicExplorer API

## Overview
MusicExplorer is a Web API built on .NET 7, which provides information about artists and releases. The atists details are saved in a SQL Server database and the releases are available by querying MusicBrainz API. MusicExplorer exposes 2 endpoints to make artists and releases available to a user. 

## Prerequisites
The prerequisites to run this solution is to have the following:
1) .NET 7 SDK
2) SQL Server 2018
3) Postman or a web browser
4) IIS (as the application will be installed on IIS)

## Project Structure
The MusicExplorer solution consists of 6 projects:
### 1. ExcelFileToSqlLoader
This project was developed to load the artists present in the Excel file to SQL Server database. The project reads the Excel file (from `docs` folder), sanitize the data and loads the data to SQL Server in a table named `Artist` using EF Core.

This project also consists of the SQL scripts, in folder `Scripts` to create the database and table used by the solution. The scripts are:
1. create_script.sql
2. seed_artists.sql

### 2. MusicExplorer


### 3. MusicExplorer.Common


### 4. MusicExplorer.Infrastructure


### 5. MusicExplorer.IntegrationTests


### 6. MusicExplorer.UnitTests


## Architecture Diagram
The architecture diagram below illustrates the components and their relationships within the application:
![MusicExplorer Architecture Diagram](https://github.com/MohammadFawwaaz/ArtistMusicExplorer/assets/62407416/8c15c8ee-2c10-47d9-8c4f-127c4e8fc1dc)

First, the Console Application is used to load the artists from the excel file to the database.
Second flow, the user access the Artist Search endpoint where the Web API queries the SQL Server database and returns a paginated result.
Third flow, the user access the Artist Release endpoint where the Web API queries the MusicBrainz API and returns a paginated result.

## Configuration


## Testing
There are 2 test suits in this project being:
1. Unit Tests
Unit tests tests individual components of the application such as: mappings, validators and paginations.

2. Integration Tests
Integration tests tests the integration of the different components of the application such as: endpoints, database call, 3rd party API calls and the services.

Note: To run the Integration tests, most specifically the `MusicBrainzClientTests`, the device must be connected to internet as the test cases connects to the MusicBrainz API.

## Running the Application


## Usage
postman collection
swagger ui (ss)

## Bonus
1. Added a validation layer to the Handlers using FluentValidation.
2. Added Swagger to the project to easily query the endpoints exposed using a web browser.

## Future Work
1. Add a layer in the application where Artists are queried from MusicBrainz API when not available in the SQL Server database.
2. Dockerize the application and add tests coverage.
3. Add more tests

## References
MusicBrainz API documentation: https://musicbrainz.org/doc/MusicBrainz_API
The documentation was used to build the URL to query MusicBrainz API to get Releases by Artist MBID.
