# Geek Hunters

You are working in IT-recruiting agency "Geek Hunters". Your employer asked you to implement Geek Registration System
(GRS). 

Using GRS a recruitment agent should be able to:
  - register a new candidate:
     - first name / last name
     - select technologies candidate has experience in from the predefined list 
  - view all candidates
  - filter candidates by technology

This single-page application built with:

- ASP.NET Core 2 and C# for cross-platform server-side code
- React for client-side code
- Bootstrap for layout and styling (I didn't focus much on it)
- localdb for database (you need to run migrations)
- Entity Framework Core as ORM
- XUnit for unit tests
The ClientApp subdirectory is a standard React application based on the create-react-app template.

Need to have Visual Studio 2017 (version 15.7 or higher) with ASP.NET Core 2.1. To install database is have to do: Update-Database in DAL project
