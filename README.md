# StayLink

## Projekto aprašymas
**StayLink** – tai moderni viešbučių kambarių rezervacijos sistema, skirta vartotojams, norintiems patogiai rezervuoti kambarius įvairiuose viešbučiuose, ir administratoriams, kurie gali valdyti viešbučių informaciją ir užimtumą. Naudodami šią sistemą, vartotojai gali prisijungti, peržiūrėti kambarius, rezervuoti juos tam tikram laikotarpiui bei tvarkyti savo rezervacijas.

## Funkciniai reikalavimai

### Svečiai:
- Viešbučių ir kambarių peržiūra.

### Vartotojai:
- Prisijungimas ir registracija.
- Viešbučių ir kambarių peržiūra.
- Kambario rezervacija pasirinktoms datoms.
- Rezervacijų peržiūra, atšaukimas ir keitimas.

### Administratoriai:
- Viešbučių ir kambarių kūrimas, redagavimas ir šalinimas.
- Vartotojų rezervacijų valdymas.
- Kambarių užimtumo stebėjimas.

## Pasirinktų technologijų aprašymas

### Back-end:
- **C# .NET Core** – Pagrindinei serverio logikai realizuoti ir API kūrimui.
- **Entity Framework Core** – Duomenų valdymui ir ryšiui su duomenų baze.
- **Duomenų bazė:** PostgreSQL.
- **JWT** – Autentifikacijai ir autorizacijai.

### Front-end:
- **React** – Interaktyviai vartotojo sąsajai kurti.
- **MUI (Material-UI)** – UI komponentų bibliotekai, suteikiančiai modernų dizainą.

## Hierarchinis ryšys
**Viešbutis -> Kambarys -> Rezervacija**
