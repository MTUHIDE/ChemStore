# ChemStore
Chem Store is a chemical tracking website that allows easy navigation and organization of inventory for chemicals on the MTU campus.

# Setup for new developers:
## IDE Setup
1. Download Visual Studio 2022 Community Edition https://visualstudio.microsoft.com/downloads/
	1. At the first menu, choose the workloads
  ASP.NET and web development

2. Clone this repository, https://github.com/MTUHIDE/ChemStore through visual studio
	1. Open Visual Studio, and hit "Clone a repository"
	2. Use the URL above, or click Github


## Local Database Setup
3. Install SQL Server Express from https://www.microsoft.com/en-us/sql-server/sql-server-downloads


4. Install SQL Server Management Studio (SSMS) (there is a button at the bottom of the window after you install SQL Server Express)

5. In SSMS, connect to your local SQL Express server
	1. Right click on the Databases folder and click "New Database..."
	2. In the window that pops up, type "chemstore" in the database name field and click "OK"

6. In the solution explorer, (list of files and folders on side of screen) double click ChemstoreWebApp.sln to build the project
	1. Tools -> NuGet Package Manager -> Package Manager Console
	2. Ensure you have an up to date appsettings.json file (get this from the team lead for now)
	3. Run `Update-Database` to create all tables locally (If this fails, try running `EntityFrameworkCore\Update-Database`)

7. Return to SSMS, right click on the chemstore database and click Refresh. There should now be tables in the Table folder that match the classes in the Models folder of the project.
	1. In order for the website to work, there must be an entry in the account table with your MTU email, and a role of 0.
	2. The list of items in container is what will show on the home page, as long as its chemical and location are in their respective tables.

## Errors and Fixes
* Nuget.org not installed as default package
	* Tools > Nuget Package Manager > Package Manager Settings
	* Go to package sources and add new package
		* Name: nuget.org
		* Source: https://api.nuget.org/v3/index.json
	* Go to general, click “Clear All NuGet Cache(s)”
	* Go back to Package Sources, click Update

* appsettings.json doesn’t show up in the solution explorer, but Visual Studio says it already exists
	* Right click on ChemStoreWebApp and Open folder in file explorer, open the file from there

