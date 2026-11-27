# Placement Dashboard - .NET Experiment 9

ASP.NET Core MVC app: a Placement Dashboard using Data Controls.

## Features
- Data table (GridView equivalent) with server-side sorting (click column headers), paging (5 per page) and search by name/company
- DropDownList filters by Company and by Year
- DetailsView-style details page per student
- Summary stat cards: Total Placed, Average Package, Highest Package, Companies Count

## Data
12 placed students across 5 companies (TCS, Infosys, Wipro, Accenture, Capgemini), packages 3.5-12 LPA. In-memory list (no database needed for the lab).

## Run
```
dotnet run --urls http://localhost:5019
```
Then open http://localhost:5019/Placement

## Student
Hamid Hussain (92400103509), B.Tech Computer Engineering, Sem V, Marwadi University - .NET Technologies (01CE1523), Experiment 9.
