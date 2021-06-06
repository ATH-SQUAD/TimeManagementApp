# Specyfikacja wymagań
## Dla projektu *Time management application*

Wersja 0.1  

Autorzy: 

* Adrian Raszka
* Filip Niedziela
* Jakub Bogacz  

26-05-2021

Spis treści
=================

[TOC]

## 1. Wstęp
### 1.1 Cel dokumentu
Celem specyfikacji jest przedstawienie podstawowych założeń i informacji na temat tworzonego projektu.

### 1.2 Zakres projektu

Aplikacja pozwalająca na zarządzanie czasem pracy pracowników w kontekście jej harmonogramowania. Ma ona ułatwić jej użytkownikom kontrolę nad ich czasem pracy, dniami wolnymi oraz przechowywać te dane przez bliżej nieokreślony czas.

Podstawowe funkcjonalności (wyjaśnione później):

* Podział na użytkowników,
* Rejestracja czasu pracy,
* Kalendarz (urlopy, zaplanowane zadania),
* Zestawienia.

### 1.3 Definicje, akronimy, skróty

Dla ujednolicenia nazwa ***projektu*** będzie podawana w języku angielskim - ***Time management application***.

### 1.4 Bibliografia

<!-- TODO: Uzupełnić o linki do kluczowych dokumentacji @rvshu13 -->

### 1.5 Przegląd dokumentu
Specyfikacja zawierać będzie opis założeń, funkcjonalności i wykorzystywanych technologii.

## 2. Przegląd projektu
### 2.1 Perspektywa
Projekt ten jest samodzielną aplikacją umożliwiającą kontrolę nad czasem pracy poprzez stronę internetową. Użytkownik logując się ma dostęp do swoich danych na temat jego obecności, zaplanowanych zadań i przerw. 

```mermaid
graph LR
A(Użytkownik) --- B(Przeglądarka)
    B --- C(Aplikacja)
    C --- D(Baza danych)
```

### 2.2 Funkcjonalności

* Podział na użytkowników
* Kontrolowanie czasu pracy (rozpoczęcie, zakończenie)
* Planowanie zadań, urlopów
* Proste zestawienia przechowywanych informacji

### 2.3 Ograniczenia

* Nie jest brane pod uwagę prawne ujęcie urlopu (zakładamy twardy, miesięczny limit)
* *Ograniczony czas* i umiejętności zespołu
* Brak domeny - działanie będzie wyłącznie lokalne (PoC)

### 2.4 Charakterystyka użytkownika

* Administrator
  * Wykonywanie wszystkich, dostępnych dla użytkownika, operacji
  * Dodatkowo możliwość usuwania efektów operacji użytkowników
  * Zarządzanie dostępem użytkowników i ich rolami
* Szef
  * Wykonywanie wszystkich, dostępnych dla użytkownika, operacji
  * Dodatkowo możliwość usuwania efektów operacji użytkowników
  * Zarządzanie dostępem użytkowników
* Użytkownik
  * Kontrolowanie czasu pracy (rozpoczęcie, zakończenie)
  * Planowanie zadań na konkretny dzień
  * Wybieranie dni wolnych
  * Dostęp do informacji

### 2.5 Założenia i zależności

Założenia procesu tworzenia:

* Wykonanie aplikacji w okresie **nie przekraczającym** **dwóch miesięcy**
* Spotkania dwa razy w tygodniu
* Mały nacisk na systemy zarządzania projektem
* Znaczny nacisk na systemy kontroli wersji

Zależności:

* Szablon aplikacji został wykorzystany z jednego z poprzednich projektów
* Wykorzystanie bazy relacyjnej z powodu łatwiejszej konfiguracji

### 2.6 Podział technologii

Poniżej przedstawia się podział wymagań technologii zależnie od działu:

| Dział                  | Technologia                 |
| ---------------------- | --------------------------- |
| Strona internetowa     | Razor Pages                 |
| Baza danych            | SQL Server Express LocalDB  |
| Architektura           | MVVM (Model-view-viewmodel) |
| Kod                    | ASP.NET Core                |
| System kontroli wersji | Git                         |

## 3. Wymagania
### 3.1 Interfejsy zewnętrzne
#### 3.1.1 Interfejs użytkownika
<!-- TODO: Wykonać wireframe w Figmie @rvshu13 -->

#### 3.1.2 Interfejs sprzętowy
Komputer, telefon.

#### 3.1.3 Interfejs programowy

Od strony użytkownika:

* Aktualna przeglądarka Google Chrome, Mozilla Firefox, Microsoft Edge, Opera bądź Safari

Od strony serwera:

* Baza danych
  * SQL Server Express LocalDB

### 3.2 Funkcjonalności
* Podział na użytkowników
* Kontrolowanie czasu pracy (rozpoczęcie, zakończenie)
* Planowanie zadań, urlopów
* Proste zestawienia przechowywanych informacji

### 3.3 Jakość usług
#### 3.3.1 Bezpieczeństwo
* Brak rejestracji - tworzenie użytkowników, ich dostępem do aplikacji i rolami będzie zarządzał Administrator
* Logowanie

#### 3.3.2 Niezawodność

* Wersja projektu przeznaczona do pracy będzie zawierać minimum funkcjonalności, aby zapewnić jej stabilność.
* Podczas tworzenia będzie wykonanych co najmniej 2 testy jednostkowe kluczowych elementów

### 3.4 Projektowanie i implementacja

#### 3.4.1 Instalacja
Nie planowany jest kreator instalacyjny - projekt będzie możliwy do skompilowania według instrukcji przedstawionych w *README.md*.

#### 3.4.7 Termin końcowy
Przewidywany termin oddania nie przekracza 04-07-2021.

#### 3.4.8 Proof of Concept
Zależnie od poziomu zaawansowania ta sekcja będzie uzupełniona wersją, która zostanie przyjęta jako PoC projektu.

## 4. Załączniki

<!-- TODO: Zmienić odnośniki po uaktualnieniu struktury repozytorium @rvshu13 -->

* Repozytorium --> https://github.com/ATH-SQUAD/TimeManagementApp/tree/master/TimeManagementApp

* README.md --> https://github.com/ATH-SQUAD/TimeManagementApp/blob/master/TimeManagementApp/README.md
