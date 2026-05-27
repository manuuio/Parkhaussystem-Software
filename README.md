# 🅿️ Parkhaus System

Eine Konsolenanwendung in C# (.NET) die eine Parkhausverwaltung simuliert.

---

## Beschreibung

Das Parkhaus System ermöglicht die Verwaltung von Ein- und Ausfahrten in einem simulierten Parkhaus. Beim Programmstart wird per Zufallsgenerator eine realistische Startbelegung erzeugt. Das System unterscheidet zwischen normalen Fahrzeugen und Motorrädern und simuliert eine vollständige Zahlungsabwicklung am Terminal.

---

## Funktionen

- Zufällige Startbelegung beim Programmstart
- Anzeige aller freien Parkplätze (übersichtlich in Spalten)
- Einfahrt mit Platznummerwahl und Fahrzeugtyp-Auswahl
- Unterscheidung zwischen Fahrzeug (2,00 €/30 Min) und Motorrad (1,00 €/30 Min)
- Automatische Kostenberechnung (angefangene 30-Minuten-Blöcke werden aufgerundet)
- Zahlungsabwicklung am Terminal mit Münzeingabe (1 Euro, 50 Cent, 20 Cent, 10 Cent)
- Wechselgeldberechnung bei Überzahlung
- Parkplatz wird nach Ausfahrt automatisch wieder freigegeben

---

## Voraussetzungen

- Windows
- Visual Studio 2022 (oder neuer)
- .NET SDK (6.0 oder neuer)

---

## Starten

1. Repository klonen:
```
git clone https://github.com/manuuio/Parkhaussystem-Software.git
```
2. Projekt in Visual Studio öffnen
3. Mit `F5` starten oder über `dotnet run` im Projektordner

---

## Klassenstruktur

| Klasse | Beschreibung |
|---|---|
| `Fahrzeug` | Basisklasse — speichert Platznummer, Parkdauer und Preis |
| `Motorrad` | Erbt von Fahrzeug — günstigerer Preis pro 30 Minuten |
| `Parkhaus` | Verwaltet freie und belegte Plätze, Ein- und Ausfahrt, Zahlungsabwicklung |
| `Program` | Einstiegspunkt — zeigt Hauptmenü und startet das Parkhaus |

---

## Technologien

- C# (.NET)
- Objektorientierte Programmierung (Vererbung, Polymorphismus)
- Konsolenanwendung

---

## Projekt

SE-Grundlagen Projekt — Umschulung Fachinformatiker Anwendungsentwicklung
