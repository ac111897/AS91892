# AS91892
## Github repository for AS91892 internal
![GitHub](https://img.shields.io/github/license/ac111897/AS91892)
![GitHub last commit](https://img.shields.io/github/last-commit/ac111897/AS91892)
![Website](https://img.shields.io/website?down_color=red&label=website-docs&up_message=online%21&url=https%3A%2F%2FAS91892-docs.ac111897.repl.co)
![GitHub repo size](https://img.shields.io/github/repo-size/ac111897/AS91892)

![Lines of code](https://img.shields.io/tokei/lines/github/ac111897/AS91892)


## Running the project
1. clone from repo
2. to apply migrations and use the dotnet-ef commands change "Enable-Test-Data" to false or else it will use the in memory database provider where you cannot make migrations/use ef commands.
3. Build and run the web project.

Some notes:
- Note that running in debug mode will have the reset db button on the home page to delete all the records in the database, preferably use this when "Enable-Test-Data".

## Relevant Implications of the Project
- This project will not infringe on copyright by using mocked data or test data. This includes stuff like not using assets such as images or videos that are copyright protected and not free use in my project.
- The project is licensed under the MIT License and might use other code snippet solutions granted they are under legal constraints where I can use them free of infringement.
- Cultural and ethical and social implication are applied by not using any potentially offensive content on the website or contained within the database.
- Future proofing will be applied by making each component modular and applying SOLID principles to the application. This will be practiced by using interfaces and relying on abstractions instead of a concrete implementation of the class being instantiated and have our code become spaghetti.
- Accessibility of the product will be implemented by using stuff like navigation bars and home pages. This also includes having our web app fit to scale to different device resolutions and different platforms/browsers.
- Aesthetics will be designed in a way so the colour is not overly bright or too dark for the user to comfortability see as this would not create a good user experience.
- Documentation of the codebase will be provided of the whole project on https://as91892-docs.ac111897.repl.co/ which will be hosted by replit.com. This will build the xml comments of classes and class members as well as anything else that requires XML documentation. The result will produce a html website by using the free tool doxygen. This website will display class components and hierarchies to easier visualize how the components blend.
