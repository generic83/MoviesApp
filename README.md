# Movies App built in .NET 5 with Angular
###### A simple SPA for viewing data about movies, offering features such as sorting, searching, and filtering
<br/>

## Table of contents
* [About the project](#about-the-project) 
  * [Built with](#built-with)
* [Getting started](#getting-started) 
  * [Prerequisites](#prerequisites)
  * [Setup](#setup)
  * [Testing](#testing)

* [Usage](#usage)
* [Project status](#project-status)

<br/>

## About the project
The project allows users to view movies-related data, as well as sort, search, and filter data based on movie title, language and location.  

The UI naviation menu consists of two items, Home and Movies. The Movies page displays a grid with features for sorting, searching, and filtering.     
In addition, the grid supports server-side pagination, which is a more scalable solution, where only a subset of the data, requested by the client, is returned.

The project uses a JSON file, movies.json, as its data source, and  for the sake of simplicity, the solution exposes an API to import data from the file into memory.  An in-memory database is used to improve performance by reducing the overhead required in generating content. The existing data model supports other database providers such as SQL Server.
The backend also exposes another API to allow the frondend to query movies-related data.  
	
### Built with
The Project is created with:
* [Angular](https://angular.io/) for the responsive UI framework
* [Angular Materials](https://material.angular.io/) for UI components
* [Bootstrap](https://getbootstrap.com) for layout and styling
* [.NET 5](https://docs.microsoft.com/en-us/dotnet/core/dotnet-five) for the backend
* EntityFramewok Core for data access

<br/>

## Getting started

### Prerequisites 
* Node js 10 or later
* Visual Studio 2019 or Visual Studio Code
* .NET 5 SDK

### Setup
- Open the MoviesApp solution in Visual Studio 2019, build the solution, and set the MoviesApp project as startup project
- Press Ctrl+F5 and Visual Studio will launch the app using IIS Express on http://localhost:41295. The port is specified in launchSettings.json
- Data import: Open another tab in the brower, and type in this url: http://localhost:41295/api/seed/import, and press Enter. The app will then read the movies.json file, deserialize its data, and store it in memory. The number of imoprted movies will be displayed once the import is complete. Triggering the import API again would clear the existing data and do the import again.

### Testing
* Backend tests: 
  * Open test explorer in Visual Studio to run tests using NUnit as the unit-testing framework
  * EF Core In-memory database is used to test controllers and repositories
* Frontend tests:
  * Run "ng test" from command line at the ClientApp directory level to execute the unit tests, [Jasmine](https://jasmine.github.io/) specs, via [Karma](https://karma-runner.github.io).

<br/>

## Usage
- The Movies page displays a table listing general data about movies, as well as a paginator whose page size can be configured
- Hvovering over a column header in the table shows the sort icon. Toggling the sort icon sorts the data in asc and desc order
- Search field and filters: An input text field  where the user can type title or part of it to search for a movie. In addition, two drop-down boxes allowing the user to filter by a specific language and/or location
- Clicking a row in the table navigates to the details page where more information about the selected movie is displayed incl. stills

<br/>

## Project status
Login functionality to be implemented: Using ASP.NET Core Identity and IdentityServer, middleware designed to add OIDC and OAuth 2.0, to add login and
registration functionalities. Angular authorization API, set of functionalities that rely upon the oidc-client library to allow an Angular app to interact with the URI endpoints provided by the ASP.NET Core Identity system.




