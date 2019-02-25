import sqlite3
import os
os.remove("WeaponsTable.db")
conn = sqlite3.connect("WeaponsTable.db")
cursor = conn.cursor()

cursor.execute("""CREATE TABLE tblWeapon
            (id INTEGER,
            Name STRING,
            Category STRING,
            Damage INTEGER,
            Inaccuracy FLOAT,
            Magazine INTEGER,
            FireRate INTEGER,
            PRIMARY KEY(id))""")
conn.commit()
