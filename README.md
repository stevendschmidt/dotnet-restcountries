
# dotnet-restcountries

A web API wrapper for the REST Countries API, written in .NET

## Description

This application provides an OData 4 REST API using data from the [REST Countries API](https://restcountries.com/). The project utilizes .NET 8 with an OpenAPI/Swagger interface.

## Getting Started

### Dependencies

* .NET 8
* Network access to the REST Countries API

### Installing

* Download the repository to your computer:
```
git clone https://github.com/stevendschmidt/dotnet-restcountries.git
```

### Executing program

* Navigate to the folder containing DotNetRestCountries.csproj and start the application:
```
dotnet run
```

## Authors

[Steven D. Schmidt](https://github.com/stevendschmidt)

## Version History

* 1.0
    * Initial Release

## Developer Notes

The application uses ASP&#46;NET Core controllers for front-end access. The controllers call a backend service that retrieves data from the Rest Countries API, processes it into new formats, and returns it to the user.

Data is queried using typical GET requests, and OData system query options are supported (__filter__, __select__, __orderby__, __top__, __skip__, etc.)

An interesting caching approach I used was to take the request path and use that as the memory cache key. This way, any request is cached by default, no matter how the user formats the query string.

I was pleased with the ease of use of Microsoft's OData and JSON libraries. Data from the REST Countries API is automatically deserialized field by field, as long as the data matches the model given. Implementing filtering, sorting, and other options was trivial since the OData system handles it for you.

One improvement to make would be to pass the OData __select__ operation through to the underlying REST Countries API in order to select certain fields directly. The REST Countries API doesn't support most operations, but field selection is supported. Doing it this way would offload the field selection to the REST Countries API server side. OData's field selection would no longer be required, increasing performance and reducing bandwidth.

Another improvement would be to include the OData system query options within the Swagger UI for convenience.