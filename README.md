# REST API

This project is a simple REST API built with ASP.NET Core Minimal API and Entity Framework Core.  

## Using
- ASP.NET Core (Minimal API)
- Entity Framework Core
- SQL Server

## Database
The database is created using Entity Framework.

Database name: RestApiDB

Testdata added using SQL-queries

## ER-Diagram
![Link to ER Diagram](https://postimg.cc/gLPF9nkh)

## API Endpoints

GET /  
Returns: REST API is running

GET /api/people  
Returns a list of all people.

GET /api/people/{personId}/interests  
Returns all interests connected to a specific person.

GET /api/people/{personId}/links  
Returns all links connected to a specific person.

POST /api/people/{personId}/interests/{interestId}  
Connects a person to an interest.

POST /api/people/{personId}/interests/{interestId}/links  
Adds a new link for a specific person and interest.

Request body example:
{
  "url": "https://en.wikipedia.org/wiki/Football",
  "note": "Description"
}

## OpenAPI
When running the project in development mode, the OpenAPI specification is available at:
/openapi/v1.json

## Notes
This project is intentionally kept simple and focuses on database relationships, basic REST API functionality, and Entity Framework usage.
