# ubisoft - Feedback API

> _NOTE_:
> This is just a Test project

## Table of Content

- [ubisoft](#ubisoft)
  - [Table of Content](#table-of-content)
  - [Getting Started](#getting-started)
  - [Repository](#repository)
  - [Domains Services](#domains-services)
  - [Authentication](#standards)
  - [Swagger](#swagger)
  - [Logging](#logging)

## Getting Started

This repo uses CQRS and Onion Architecture. The repo is divided into:
    - Repository
        Defines the access to database through the repository pattern design
    - Domain Services
        - Defines `Commands` or `Queries` used in the domain
        - Defines the `Handlers` and `QueryHandlers`
        - Defines the `CommandBus` in charge of routing commands to the correct handler
        - Defines the `Dispatcher` in charge of routing queries to the correct query handler
    - Infrastructure
        Contains infrastructure specific implementations like the Context of the ORM and implementaion that depends on the specific DI container used.
    - Gateway/UI
        Contains the API that exposes the required endpoints:
            - An HTTP endpoint for players to post a new feedback for a session.
            - An HTTP endpoint to get the last 15 feedbacks left by players and allow
            filtering by rating.
    - Tests
        This folder contains the unit tests.

To build and run this service locally, you need to have Visual Studio 2019 or a higher version.

## Repository

To access data this repos uses the repository pattern which is a design pattern that mediates data from and to the Domain and Data Access Layers. Repositories are classes that hide the logics required to store or retreive data. Thus, our application will not care about what kind of ORM we are using, as everything related to the ORM is handled within a repository layer. This allows you to have a cleaner seperation of concerns. Unit of Work is referred to as a single transaction that involves multiple operations of insert/update/delete and so on kinds.

## Domains Services

In more traditional architectures the same data model is used to both query and update the database. In simple scenarios like CRUD operations, it works very well but in more complex cases this approach brings several disadvantages. The result can be very complex and a big model that does way too much and objects mapping can become very complicated.
This repo is using CQRS design pattern. Command Query Responsibility Segregation (CQRS) is an architectural pattern that separates reading and writing into two different models. Unlike many patterns that answer to general business or engineering problems, the CQRS pattern is an alternative to another pattern, CRUD.

In a more complex scenario this folder would contain the Domain logic and Services that might interact with other concepts and serves as bridge for our domain.


## Authentication

The API uses a `CustomAuthenticationHandler` that looks for the header `Ubi-UserId` (`UbiUserId`) to authenticate calls. 
To consume the query endpoints the caller needs to be in the Admin role. This is hardcoded right now in the class IRoleManager,
The only user with Admin role is `BAF003C2-8CB4-4AE8-8A55-198660830E80`

## Swagger

This repo uses swagger to describe the exposed endpoints. To authenticate the call just click on the Authorize button in the top right.

## Logging

This repo uses Serilog to log unhandled errors.
