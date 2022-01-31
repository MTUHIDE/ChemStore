# ChemStore
Chem Store is a chemical tracking website that allows easy navigation and organization of inventory for chemicals on the MTU campus.

# Setup for new developers:
## IDE Setup
1. Download visual studio 2019 community https://visualstudio.microsoft.com/downloads/
	1. At the first menu, choose the workloads
  ASP.NET and web development

2. Clone this repository, https://github.com/MTUHIDE/ChemStore through visual studio
	1. Open Solution Explorer, and double click “Solution ‘ChemStoreWebApp’” to be able to edit the project.
	2. Since appsettings.json is in gitignore, manually create an “appsettings.json” file and copy/paste from the github
	3. Run “IIS Express” at the top of the screen
		1. Project should be working except the database (can’t filter)


## Local Database Setup
3. Install MySql from https://dev.mysql.com/downloads/installer/
	1. Make sure to include MySQL Server
	2. Choose the ‘Developer Default’ setup type to include MySql for Visual Studio
	3. Python is required, if you get an error just install it then press “check”
	4. For root password, request it from team.

4. In MySQL Workbench, create a new connection
	1. Name: chemstore
	2. All else default
	3. In the connection, in the query window, run “CREATE DATABASE chemstore;”, press Ctrl+Enter to execute

5. In Visual Studio, go to View -> Server Explorer -> Connect to Database and choose MySQL Database
	1. Server name: 127.0.0.1
	2. User name: root
	3. Password: <request from team>
	4. Database name: chemstore

6. In the solution explorer, (list of files and folders on side of screen) double click ChemstoreWebApp.sln to build the project
	1. Tools -> NuGet Package Manager -> Package Manager Console
	2. Run `Update-Database` to create all tables locally
7. Return to MySQL workbench, refreshing the list of schemas should show **chemstore**, and expanding that should show the tables.
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

* Python not installed, which is a requirement for MySql Connector
	* Install Python 3.9.4 https://www.python.org/downloads/

* If you are getting connection issues, sometimes using "localhost" instead of "127.0.0.1" fixes the issue

