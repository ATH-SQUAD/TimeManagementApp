# Specyfikacja wymagań
## Dla projektu *Time management application*

Wersja 0.1  

Autorzy: 

* Adrian Raszka
* Filip Niedziela
* Jakub Bogacz  

21-05-2021

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

Brak.

### 1.5 Przegląd dokumentu
Specyfikacja zawierać będzie opis założeń, funkcjonalności i wykorzystywanych technologii.

## 2. Przegląd projektu
> This section should describe the general factors that affect the product and its requirements. This section does not state specific requirements. Instead, it provides a background for those requirements, which are defined in detail in Section 3, and makes them easier to understand.

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
TODO: Layout.

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

### 3.3 Quality of Service
> This section states additional, quality-related property requirements that the functional effects of the software should present.

#### 3.3.1 Performance
If there are performance requirements for the product under various circumstances, state them here and explain their rationale, to help the developers understand the intent and make suitable design choices. Specify the timing relationships for real time systems. Make such requirements as specific as possible. You may need to state performance requirements for individual functional requirements or features.

#### 3.3.2 Security
Specify any requirements regarding security or privacy issues surrounding use of the product or protection of the data used or created by the product. Define any user identity authentication requirements. Refer to any external policies or regulations containing security issues that affect the product. Define any security or privacy certifications that must be satisfied.

#### 3.3.3 Reliability
Specify the factors required to establish the required reliability of the software system at time of delivery.

#### 3.3.4 Availability
Specify the factors required to guarantee a defined availability level for the entire system such as checkpoint, recovery, and restart.

### 3.4 Compliance
Specify the requirements derived from existing standards or regulations, including:  
* Report format
* Data naming
* Accounting procedures
* Audit tracing

For example, this could specify the requirement for software to trace processing activity. Such traces are needed for some applications to meet minimum regulatory or financial standards. An audit trace requirement may, for example, state that all changes to a payroll database shall be recorded in a trace file with before and after values.

### 3.5 Design and Implementation

#### 3.5.1 Installation
Constraints to ensure that the software-to-be will run smoothly on the target implementation platform.

#### 3.5.2 Distribution
Constraints on software components to fit the geographically distributed structure of the host organization, the distribution of data to be processed, or the distribution of devices to be controlled.

#### 3.5.3 Maintainability
Specify attributes of software that relate to the ease of maintenance of the software itself. These may include requirements for certain modularity, interfaces, or complexity limitation. Requirements should not be placed here just because they are thought to be good design practices.

#### 3.5.4 Reusability
<!-- TODO: come up with a description -->

#### 3.5.5 Portability
Specify attributes of software that relate to the ease of porting the software to other host machines and/or operating systems.

#### 3.5.6 Cost
Specify monetary cost of the software product.

#### 3.5.7 Deadline
Specify schedule for delivery of the software product.

#### 3.5.8 Proof of Concept
<!-- TODO: come up with a description -->

## 4. Verification
> This section provides the verification approaches and methods planned to qualify the software. The information items for verification are recommended to be given in a parallel manner with the requirement items in Section 3. The purpose of the verification process is to provide objective evidence that a system or system element fulfills its specified requirements and characteristics.

<!-- TODO: give more guidance, similar to section 3 -->
<!-- ieee 15288:2015 -->

## 5. Appendixes
