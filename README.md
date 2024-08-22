# DotNetMVC_GithubRepos

It's a basic .Net MVC application to fetch data from Github API and allow user to search based on project names.

1. The backend is confdigured in .NET Core 8.0
2. For DB connection, Entity Framework (CFA) is used.
3. To fetch the data, a recurring job is created using Hangfire which runs Minuetly.
4. For UI, Razor views have been created.
