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
            Inaccuracy INTEGER,
            Magazine INTEGER,
            FireRate FLOAT,
            PRIMARY KEY(id))""")
conn.commit()
