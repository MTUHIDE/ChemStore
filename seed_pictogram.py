# This file adds pictograms to the database.
# Only run this file after a database wipe and
# before inserting data for StatementPictogram table/model
# Also delete any images from the media directory

# To run this file use: python seed_pictogram.py
# from the root directory of the project

import os
import django

# Setup Django environment
os.environ.setdefault("DJANGO_SETTINGS_MODULE", "chemstore.settings")
django.setup()

from django.conf import settings
from store.models import HazardPictogram
from django.core.files import File

# Data to be inserted into HazardPictogram
pictograms = [
    ("ghCode1", "Explosive", "exploding-bomb-pictogram.jpg"),
    ("ghCode2", "Flammable", "flame-pictogram.jpg"),
    ("ghCode3", "Oxidizing", "flame-over-circle-pictogram.jpg"),
    ("ghCode4", "Compressed Gas", "gas-cylinder-pictogram.jpg"),
    ("ghCode5", "Corrosion", "corrosion-pictogram.jpg"),
    ("ghCode6", "Toxic", "skull-pictogram.jpg"),
    ("ghCode7", "Exclamation", "exclamation-mark-pictogram.jpg"),
    ("ghCode8", "Health", "health-hazard-pictogram.jpg"),
    ("ghCode9", "Environment", "environment-pictogram.jpg"),
]

# Loop through each element in the pictogram list
for gh_code, description, filename in pictograms:

    file_path = settings.BASE_DIR / "store" / "static" / "store" / "images" / "hazards" / filename

    if not file_path.exists():
        print(f"File not found: {filename}")
        continue

    with open(file_path, "rb") as f:
        HazardPictogram.objects.update_or_create(
            gh_code=gh_code,
            description=description,
            pictogram= File(f, name=filename)
        )