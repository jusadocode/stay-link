# StayLink

## Projekto aprašymas
**StayLink** – tai moderni viešbučių kambarių rezervacijos sistema, skirta vartotojams, norintiems patogiai rezervuoti kambarius įvairiuose viešbučiuose, ir administratoriams, kurie gali valdyti viešbučių informaciją, užimtumą bei užsakymus. Naudodami šią sistemą, vartotojai gali prisijungti, peržiūrėti kambarius, rezervuoti juos tam tikram laikotarpiui bei peržiūrėti savo rezervacijas.

## Funkciniai reikalavimai

### Svečiai:
- Viešbučių ir kambarių peržiūra.

### Vartotojai:
- Prisijungimas ir registracija.
- Viešbučių ir kambarių peržiūra.
- Kambario rezervacija pasirinktoms datoms.
- Rezervacijų peržiūra.

### Administratoriai:
- Viešbučių ir kambarių kūrimas, redagavimas ir rezervacijų šalinimas.
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
# Naudotojo sąsajos wireframe

Toliau pateikiama keletas naudotojo sąsajos wireframe pavyzdžių:

|    |    |
|-------------|-------------|
| <p align="center"><img src="https://github.com/user-attachments/assets/690e9d3b-41c1-42c6-a33a-255fe33f359e" width="400" height="300" /></p> | <p align="center"><img src="https://github.com/user-attachments/assets/d3c5f755-afaf-46f5-9f95-1c8d53cc77fd" width="400" height="300" /></p> |
|    |    |
| <p align="center"><img src="https://github.com/user-attachments/assets/c704de52-6ffc-4434-bddb-4868f4611242" width="500" height="600" /></p> | <p align="center"><img src="https://github.com/user-attachments/assets/9ad190fa-3690-47de-b955-b84d993931dc" width="500" height="600" /></p> |

---

![image](https://github.com/user-attachments/assets/72e2fb04-c9ce-4e78-bcb3-c43f80e4d545)

![image](https://github.com/user-attachments/assets/0bc63c2b-5f76-4119-ba8c-522fea24729a)


# Naudotojo sąsajos dizainas    

Toliau pateikiama dalis įgyvendintų naudotojo sąsajos dizaino pavyzdžių:
|    |    |
|-------------|-------------|
| <p align="center"><img src="https://github.com/user-attachments/assets/f5726090-6a70-4108-8baf-de678f81f0a2" width="400" height="400" /></p> | <p align="center"><img src="https://github.com/user-attachments/assets/8a99493a-25e4-4c15-b48f-d7231bb456b1" width="400" height="400" /></p> |
|    |    |
| <p align="center"><img src="https://github.com/user-attachments/assets/c8dc6a8f-f5bd-4051-aff7-04c940360b3b" width="600" height="400" /></p> | <p align="center"><img src="https://github.com/user-attachments/assets/2b625e0b-3653-4c7e-83e9-d71dfcc785de" width="600" height="400" /></p> |


![image](https://github.com/user-attachments/assets/7b158a86-5687-4648-a860-2295f1d0622c)

![image](https://github.com/user-attachments/assets/f07b907b-707a-4a36-897e-1c87eda3facc)

![image](https://github.com/user-attachments/assets/46ad232f-5552-4264-be29-a351d44ad2e0)

# UML Deployment diagrama

## 1. Frontend

- **Frontend** komponentas talpinamas **DigitalOcean App Platform** sistemoje.
- Platforma užtikrina automatinį aplikacijos diegimą ir atnaujinimą, kas palengvina administravimą ir priežiūrą.

## 2. Backend

- **Backend** komponentas įdiegtas **DigitalOcean Droplet** (virtualioje mašinoje), naudojant **Docker** konteinerius.
- Backend veikia kaip atskiras konteineris ir bendrauja su **PostgreSQL** duomenų bazės konteineriu.

## 3. Nginx Serveris

- Nginx veikia kaip **reverse proxy** serveris ir suteikia papildomą apsaugą ir valdo atkeliaujančias užklausas iš interneto.
- Jis tvarko **SSL/TLS** sertifikatus ir užtikrina saugų **HTTPS** ryšį su išoriniais klientais.
- Nginx peradresuoja gautas užklausas į **HTTP** protokolą, kuris tuomet naudojamas vidiniame **Docker** tinkle.

![image](https://github.com/user-attachments/assets/6aeb6b05-db3b-491e-b752-9442460ffdd7)

## **Rezervacijos**

## **1. Gauti visas rezervacijas**

`GET /api/Bookings`

**Aprašymas:**  
Grąžinamas visų rezervacijų sąrašas.

**Atsakymas (200 OK):**
```json
[
  {
    "id": 1,
    "roomId": 1,
    "userId": "12345",
    "startDate": "2024-12-21",
    "endDate": "2024-12-23"
  },
  {
    "id": 2,
    "roomId": 2,
    "userId": "67890",
    "startDate": "2024-12-22",
    "endDate": "2024-12-24"
  }
]
```
**Atsakymas (401 Unauthorized)**

---

## **2. Sukurti naują rezervaciją**

`POST /api/Bookings`

**Aprašymas:**  
Leidžiama naudotojui sukurti naują rezervaciją.

**Užklausos pavyzdys:**
```json
{
  "roomId": 1,
  "userId": "12345",
  "startDate": "2024-12-21",
  "endDate": "2024-12-23"
}
```
**Atsakymas (201 Created):**
```json
{
  "id": 1,
  "roomId": 1,
  "userId": "12345",
  "startDate": "2024-12-21",
  "endDate": "2024-12-23"
}
```
**Galimi atsakymai (422 Unprocessable Entity):**
```json
{ "message": "Room does not belong to the specified hotel." }
```

```json
{ "message": "End date must be greater than the start date." }
```
---

## **3. Gauti rezervaciją pagal ID**

`GET /api/Bookings/{id}`

**Aprašymas:**  
Grąžinami rezervacijos duomenys pagal pateiktą ID.

**Atsakymas (200 OK):**
```json
{
  "id": 1,
  "roomId": 1,
  "userId": "12345",
  "startDate": "2024-12-21",
  "endDate": "2024-12-23"
}
```

**Galimas atsakymaas (404 Not found):**
```json
{ "message": "Booking not found." }
```
---

## **4. Atlikti rezervacijos atnaujinimą**

`PUT /api/Bookings/{id}`

**Aprašymas:**  
Leidžiama atnaujinti esamą rezervaciją pagal ID.

**Užklausos pavyzdys:**
```json
{
  "roomId": 1,
  "userId": "12345",
  "startDate": "2024-12-22",
  "endDate": "2024-12-24"
}
```

**Atsakymas (204 No Content):**

**Galimi atsakymai:**

- 400 Bad Request:
```json
 { "message": "Invalid data." }
```
- **403 Forbidden:**

- **404 Not Found:**

```json
{ "message": "Booking not found." }
```

## **5. Ištrinti rezervaciją**

`DELETE /api/Bookings/{id}`

**Aprašymas:**  
Leidžiama ištrinti rezervaciją pagal ID.

**Atsakymas (204 No Content):**

**Galimi atsakymai:**

- **404 Not Found:**
```json
{ "message": "Booking not found." }
```
- **403 Forbidden:**

## **Autentifikacija**

## **1. Registracija**

`POST /api/accounts`

**Aprašymas:**  
Asmenims galima registruotis sistemoje pateikiant vartotojo duomenis.

**Užklausos pavyzdys:**
```json
{
  "userName": "JohnDoe",
  "email": "JohnDoe@email.com",
  "password": "password123"
}
```
**Atsakymas (200 OK):**
```json
{
  "message": "Registration successful"
}
```
**Galimi atsakymai:**

- **400 Bad Request:**
```json
 { "message": "Invalid input." }
```
- **422 Unprocessable Entity:**
```json
  { "message": "Email already in use." }
```

## **2. Prisijungimas**

`POST /api/login`

**Aprašymas:**  
Leidžiama vartotojams prisijungti prie sistemos pateikiant vartotojo vardą ir slaptažodį.

**Užklausos pavyzdys:**

```json
{
  "userName": "JohnDoe",
  "password": "password123"
}
```
**Atsakymas (200 OK):**

```json
{
  "userUd": "23523535",
  "roles": [Admin, BookingUser]
}
```

**Galimi atsakymai:**

- **422 Unprocessable Entity:**

```json
{ "message": "Invalid username or password." }
```

## **3. Atnaujinti prisijungimo žetoną**

`POST /api/accessToken`

**Aprašymas:**  
Leidžiama atnaujinti prisijungimo žetoną.

**Atsakymas (200 OK):**

**Galimi atsakymai:**

- **422 Unprocessable Entity:**
```json
 { "message": "Invalid refresh token." }
```

```json
 { "message": "Refresh token not found in cookies." }
```

## **4. Atsijungimas**

`POST /api/logout`

**Aprašymas:**  
Leidžiama vartotojams atsijungti nuo sistemos.

**Atsakymas (200 OK):**
```json
{
  "message": "Logout successful"
}
```
**Galimi atsakymai:**

- **401 Unauthorized:**
