# AS91892
## Github repository for AS91892 internal
![GitHub](https://img.shields.io/github/license/ac111897/AS91892)
![GitHub last commit](https://img.shields.io/github/last-commit/ac111897/AS91892)
![Website](https://img.shields.io/website?down_color=red&label=website-docs&up_message=online%21&url=https%3A%2F%2FAS91892-docs.ac111897.repl.co)
![GitHub repo size](https://img.shields.io/github/repo-size/ac111897/AS91892)

## Running the project
1. clone from repo
2. to apply migrations and use the dotnet-ef commands change "Enable-Test-Data" to false or else it will use the in memory database provider where you cannot make migrations/use ef commands.
3. Build and run the web project.

Some notes:
- Note that running in debug mode will have the reset db button on the home page to delete all the records in the database, preferably use this when "Enable-Test-Data".
