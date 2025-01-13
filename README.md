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

6. Install Runtime Installer
   	1. Install the Runtime Installer for your operating system from https://versionsof.net/core/6.0/6.0.33/

8. In the solution explorer, (list of files and folders on side of screen) double click ChemstoreWebApp.sln to build the project
	1. Tools -> NuGet Package Manager -> Package Manager Console
	2. Ensure you have an up to date appsettings.json file (get this from the team lead for now)
	3. Run `Update-Database` to create all tables locally (If this fails, try running `EntityFrameworkCore\Update-Database`)

9. Return to SSMS, right click on the chemstore database and click Refresh. There should now be tables in the Table folder that match the classes in the Models folder of the project.
	1. In order for the website to work, there must be an entry in the User table with your MTU email. Run the following SQL script, replacing `[YOUR MTU EMAIL]` with your MTU email, and `[YOUR NAME]` with your name.

```
INSERT INTO [chemstore].[dbo].[Role] VALUES('Developer')
INSERT INTO [chemstore].[dbo].[Department] VALUES('Debug')
INSERT INTO [chemstore].[dbo].[User] VALUES('[YOUR MTU EMAIL]', '[YOUR NAME]', 1, 1)
```

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

