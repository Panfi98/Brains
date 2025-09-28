# Job Kid (Backend)

Server-side application for the **Job Kid** project.  
Provides REST APIs for authentication, job management, CV/cover letter creation, and data persistence.

---

##  About

Brains is a backend built with **ASP.NET Core** and **Entity Framework Core**.  
It works together with [BrainsFrontend](https://github.com/Panfi98/BrainsFrontend) to deliver a complete job-finding and CV-building platform.

Currently, the project is not fully completed. At this stage, it supports only basic CV creation with a single template, but it has strong potential for future expansion.

## Installation / Setup

1. Clone the repo:  
   ```bash
   git clone https://github.com/Panfi98/Brains.git
   cd Brains
2. Restore dependencies:

       dotnet restore

3. Configure environment (e.g. appsettings.json or environment variables):

Database connection string
JWT secret key
Email/SMTP settings (if used)

4.Apply database migrations:

    dotnet ef database update

5.Run the server:

    dotnet run

The API should now be running on http://localhost:5000 (or configured port).

## API Overview
Some of the core endpoints:

POST /api/auth/register – user registration

POST /api/auth/login – login and receive JWT

GET /api/jobs – list/search jobs

POST /api/jobs/{id}/apply – apply for a job

GET /api/cv – get user CV

POST /api/cv – create/update CV

(Endpoints may change as the project evolves.)

## Roadmap
Multiple CV & cover letter templates

Improved job search features

Email notifications

Admin & recruiter roles

PDF export for CVs and cover letters

## Author
Developed by Panfi98, Crunch25 and ShapovalYehor
