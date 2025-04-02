# ChemStore
Chem Store is a chemical tracking website that allows easy navigation and organization of inventory for chemicals on the MTU campus.

# Setup for new developers:
## IDE Setup
Any IDE that supports Python and Django can be used. PyCharm is recommended, and comes free with an MTU email.

## Python Setup
1. Install the latest version of [Python 3.9](https://www.python.org/downloads/release/python-3913/)  
The easiest way to install an manage python versions is with [uv](https://github.com/astral-sh/uv).
Once uv is installed, you can simply clone the repository and run:
```bash
uv sync
```
This will install python 3.9.21 and create a virtual environment in the project directory, as well as install all the requirements.

### Setup without uv
If you do not want to use uv, you can set up the project manually:
1. Install Python 3.9 from the [Python website](https://www.python.org/downloads/release/python-3913/).

2. After cloning the repository, create a virtual environment in the project directory:
```bash
python -m venv .venv
```
3. Activate the virtual environment:
```bash
# On Windows
.venv\Scripts\activate

# On Linux or MacOS
.venv/bin/activate
```

4. Install the required packages:
```bash
pip install .
```

## Database Setup
The development database is SQLite, which is created automatically upon running:
```bash
python manage.py migrate
```
The production database will be MySQL, and will be set up by the server administrator.

## Running the server
There are two versions of the server: a synchronous (WSGI) server and an asynchronous (ASGI) server. The ASGI server allows asynchronous requests, which is useful for long-running tasks.  
This project has the ability to use either server, but the ASGI server is recommended for production. The WSGI server gives better debug logs and information.
### With uv installed
If you have uv installed, you can run the WSGI server with:
```bash
uv run python manage.py runserver
```
or the ASGI server with:
```bash
uv run uvicorn chemstore.asgi:application
```

### Without uv
1. Make sure the virtual environment is activated.
```bash
# On Windows
.venv\Scripts\activate

# On Linux or MacOS
.venv/bin/activate
```

2. Run the WSGI server:
```bash
python manage.py runserver
```
or the ASGI server:
```bash
python -m uvicorn chemstore.asgi:application
```

## Running in deployment
When running in deployment, a few changes need to be made. It's assumed uv is installed.  
Setup a .env file in the root directory with the following variables:
```dotenv
DJANGO_KEY=<secret>  # See https://docs.djangoproject.com/en/4.2/ref/settings/#std-setting-SECRET_KEY
DB_NAME=<database_name>  # The name of the database to connect to
DB_USER=<database_user>  # The user to connect to the database
DB_PASS=<database_password>  # The password for the database user
DB_HOST=<database_host>  # The host of the database
DB_PORT=<database_port>  # The port of the database
DEBUG=False
```

### On each run/reload of the server:

1. Make sure the packages are installed:
```bash
uv sync --extra deploy
```
2. Run the migrations:
```bash
uv run python manage.py migrate
```
3. Collect static files with:
```bash
uv run python manage.py collectstatic
```
*It's important that there are no references to nonexistant static files in the templates, as this will cause an error when trying to initialize static files.*
4. Run the server with:
```bash
uv run gunicorn chemstore.asgi:application
```

The server will be running on port 8000 by default. This can be changed through modifying the `gunicorn.conf.py` file.