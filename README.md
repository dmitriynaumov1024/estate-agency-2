# estate-agency-2

Next gen estate agency

(C) 2022 Dmitriy Naumov

## Goals

### Level 1
- Create entity schema
- Create an abstract class for database backend

### Level 2
- Implement database backend
- Test the backend
- Create a dummy database

### Level 3
- Create a server with handlers corresponding to use case
- Create a front-end 

## Technologies

- C# (server and database backend)
- ASP.NET (server)
- Vue 2 (front-end)
- ? (database)

## Coursework outline

- 1 Проектування інформаційної системи "Агентство нерухомості"
  - 1.1 Теоретичні відомості про ASP.NET
  - 1.2 Теоретичні відомості про Vue
  - 1.3 Опис інформаційної системи
- 2 Реалізація веб-сайту агентства нерухомості 
  - 2.1 Розробка сутностей та адаптеру бази даних
  - 2.2 Розробка Web API для взаємодії з системою
  - 2.3 Розробка клієнтської частини

## Requirements

1. dotnet sdk: 5.0 or higher
2. npm: latest

## Usage

1. Go to `src/VueApp`, execute `npm install` and `publish.sh` there
2. Go to `src/ServerApp`, execute `dotnet run` there
3. In web browser, navigate to `http://localhost:5000`
4. If everything is alright, you should see Estate agency.
