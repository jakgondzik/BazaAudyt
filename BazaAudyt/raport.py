import os
import datetime
import pandas as pd
import matplotlib.pyplot as plt
import pyodbc
from reportlab.lib.pagesizes import A4
from reportlab.pdfgen import canvas
from reportlab.lib.utils import ImageReader
from io import BytesIO

dzisiaj = datetime.datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
desktop = os.path.join(os.path.expanduser("~"), "Desktop")
folder = os.path.join(desktop, f"RaportAudyt_{dzisiaj}")
os.makedirs(folder, exist_ok=True)
pdf_path = os.path.join(folder, "RaportAudyt.pdf")

conn_str = (
    "Driver={ODBC Driver 17 for SQL Server};"
    "Server=KUBA-KOMPUTER;"
    "Database=Audyty;"
    "UID=audytor;"
    "PWD=audytor;"
    "TrustServerCertificate=Yes;"
    "Connect Timeout=30;"
)

query_count = """
SELECT COUNT(*) 
FROM LPA_PlanAudytow
WHERE DataPlanowana IS NOT NULL 
AND DataPlanowana < DATEADD(DAY, -30, GETDATE())
"""

query_dane = """
SELECT Id, DataPlanowana
FROM LPA_PlanAudytow
WHERE DataPlanowana IS NOT NULL
"""

conn = pyodbc.connect(conn_str)
cursor = conn.cursor()
cursor.execute(query_count)
liczba_spoznionych = cursor.fetchone()[0]
df = pd.read_sql_query(query_dane, conn)
conn.close()

df["DataPlanowana"] = pd.to_datetime(df["DataPlanowana"])
df["DniOdPlanowanej"] = (datetime.datetime.now() - df["DataPlanowana"]).dt.days
df["TygodnieSpoznienia"] = (df["DniOdPlanowanej"] // 7).clip(upper=3)
grupy = df[df["DniOdPlanowanej"] > 7]["TygodnieSpoznienia"].value_counts().sort_index()

buffer = BytesIO()
if not grupy.empty:
    plt.figure(figsize=(6, 4))
    grupy.plot(kind="bar", color="orange")
    plt.xlabel("Opóźnienie względem daty planowanej (tygodnie)")
    plt.ylabel("Liczba audytów")
    plt.title("Audyt opóźniony o 1, 2, 3+ tygodnie")
    plt.xticks(ticks=[0, 1, 2, 3], labels=["1 tydz", "2 tyg", "3 tyg", "3+ tyg"])
    plt.tight_layout()
    plt.savefig(buffer, format='png')
    plt.close()
    buffer.seek(0)

c = canvas.Canvas(pdf_path, pagesize=A4)
width, height = A4
text_y = height - 50

c.setFont("Helvetica", 12)
c.drawString(50, text_y, f"Raport audytów - {dzisiaj}")
text_y -= 30
c.drawString(50, text_y, f"Liczba audytów planowanych ponad 30 dni temu: {liczba_spoznionych}")
text_y -= 50

if not grupy.empty:
    image = ImageReader(buffer)
    c.drawImage(image, 50, 200, width=500, preserveAspectRatio=True, mask='auto')
else:
    c.drawString(50, text_y, "Brak wykresu - brak audytów opóźnionych o więcej niż 7 dni.")

c.save()

print(f"Raport PDF zapisany na pulpicie w folderze: {folder}")
