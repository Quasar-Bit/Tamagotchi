# Project name: Tamagotchi
Subject: Give meal to real dogs with Tamagotchi App.
Description: The application allows users to give meal to real pets and grow theirs account states or whatever. The application can handle data with 200k and get responses in a tick. The approach is a Sql Database + DataTables Queries + Parametrized request body and finally we have the fastest possible way to get some data from the table filled by 200k lines.

# Data
The Database has over 200k Animals and was implemented with posibillity to handle it fast. For example, fetching the animal page and searching take almost no time.

# Internal projects:
- Tamagotchi.Data 
Database project with EntityFramework approach.
- Tamagotchi.Application
The main business logic of the project, implemented with CQRS design including Mediatr.
- Tamagotchi.Api
The APIs of Tamagotchi project. The majority of business logic can be managed by these ones. 
- Tamagotchi.Web
Web Application when we can see the data in the convinient view.
- Tamagotchi.ApisMonitoring
HealthChecks sample project for monitoring services validity.

Start Date working: March 4 2022
Task File: https://curaciov.com/Backend_Developer_Assignment.pdf
GutHub project: https://github.com/IaamRich/Tamagotchi
The project gets list of pets via Petfinder.com APIs: https://www.petfinder.com/developers/v2/docs/
Used DBMS: MSSQL
Platform: .Net Core 6

Vladimir Curaciov © 2023