# ChemStore
Chem Store is a chemical tracking website that allows easy navigation and organization of inventory for chemicals on the MTU campus.

# Setup for new developers:
## IDE Setup
Any IDE that supports Python and Django can be used. PyCharm is recommended, and comes free with an MTU email.

## Python Setup
1. Install the latest version of [Python 3.9](https://www.python.org/downloads/release/python-3913/)  
On Windows, I would recommend using [scoop](https://scoop.sh/) to install Python. It is a package manager for Windows that makes it easy to install and manage Python versions.
With scoop installed, you can install Python with the following command:  
```bash
scoop bucket add versions
scoop install versions/python39
```
2. After cloning the repository, create a virtual environment in the project directory:
```bash
python -m venv .venv
```
3. Activate the virtual environment:
```bash
# On Windows
.venv\Scripts\activate
```

4. Install the required packages:
```bash
pip install -r requirements.txt
```

## Database Setup
The development database is SQLite, which is created automatically upon running:
```bash
python manage.py migrate
```
The production database will be MySQL, and will be set up by the server administrator.