# UserAPI - .NET 9 Microservice

A modern, secure user authentication microservice built with .NET 9, featuring JWT-based authentication, BCrypt password hashing, and PostgreSQL database integration.

## Features

- User registration with secure password hashing (BCrypt)
- User login with credential validation
- JWT token generation and validation
- Protected endpoints requiring authentication
- Swagger/OpenAPI documentation
- Docker support with multi-stage builds
- PostgreSQL database integration with Entity Framework Core
- Layered architecture (Controllers, Services, Models, Data)

## Architecture

```
UserAPI/
├── Controllers/       # API endpoints
├── Services/          # Business logic
├── Models/            # Data models and DTOs
├── Data/              # Database context
└── Program.cs         # Application configuration
```

## Prerequisites

- .NET 9 SDK or Docker
- PostgreSQL (or use the provided docker-compose setup)

## Environment Variables

The application requires the following environment variables:

| Variable | Description | Example |
|----------|-------------|---------|
| `DATABASE_URL` | PostgreSQL connection string | `Host=localhost;Port=5432;Database=userapi;Username=postgres;Password=postgres` |
| `JWT_SECRET` | Secret key for JWT signing (min 32 characters) | `your-super-secret-jwt-key-change-this-in-production-min-32-chars` |
| `JWT_ISSUER` | JWT token issuer | `UserAPI` |
| `JWT_AUDIENCE` | JWT token audience | `UserAPI` |
| `ASPNETCORE_ENVIRONMENT` | Application environment | `Development` or `Production` |

Copy `.env.example` to `.env` and update the values as needed.

## Running with Docker Compose (Recommended)

The easiest way to run the application is using Docker Compose, which sets up both the API and PostgreSQL database:

```bash
docker-compose up -d
```

The API will be available at `http://localhost:8080`

To stop the services:

```bash
docker-compose down
```

To view logs:

```bash
docker-compose logs -f userapi
```

## Running Locally with .NET

1. Ensure PostgreSQL is running and accessible

2. Set environment variables (or use appsettings.json):

```bash
export DATABASE_URL="Host=localhost;Port=5432;Database=userapi;Username=postgres;Password=postgres"
export JWT_SECRET="your-super-secret-jwt-key-change-this-in-production-min-32-chars"
export JWT_ISSUER="UserAPI"
export JWT_AUDIENCE="UserAPI"
```

3. Restore dependencies and run:

```bash
cd UserAPI
dotnet restore
dotnet run
```

The API will be available at `http://localhost:5000` (or the port specified in launchSettings.json)

## API Documentation

Once the application is running, access the Swagger UI at:

```
http://localhost:8080/swagger
```

## API Endpoints

### Public Endpoints

#### Register User
```http
POST /api/auth/register
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "securePassword123"
}
```

Response:
```json
{
  "success": true,
  "message": "User registered successfully",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "user": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "email": "user@example.com",
    "createdAt": "2025-11-19T22:00:00Z"
  }
}
```

#### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "securePassword123"
}
```

Response:
```json
{
  "success": true,
  "message": "Login successful",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "user": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "email": "user@example.com",
    "createdAt": "2025-11-19T22:00:00Z"
  }
}
```

### Protected Endpoints

#### Get Current User
```http
GET /api/user/me
Authorization: Bearer <your-jwt-token>
```

Response:
```json
{
  "success": true,
  "user": {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "email": "user@example.com",
    "createdAt": "2025-11-19T22:00:00Z"
  }
}
```

## Testing with cURL

### Register a new user:
```bash
curl -X POST http://localhost:8080/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{"email":"test@example.com","password":"password123"}'
```

### Login:
```bash
curl -X POST http://localhost:8080/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"test@example.com","password":"password123"}'
```

### Access protected endpoint:
```bash
curl -X GET http://localhost:8080/api/user/me \
  -H "Authorization: Bearer YOUR_JWT_TOKEN"
```

## Security Features

- **Password Hashing**: Passwords are hashed using BCrypt with automatic salt generation
- **JWT Authentication**: Secure token-based authentication with configurable expiration
- **HTTPS Ready**: Configured for HTTPS in production environments
- **Input Validation**: Model validation on all endpoints
- **Secure Defaults**: Non-root user in Docker container, minimal attack surface

## Database Schema

The application automatically creates the following table structure:

### Users Table
| Column | Type | Constraints |
|--------|------|-------------|
| id | uuid | Primary Key, Default: gen_random_uuid() |
| email | text | Not Null, Unique |
| password_hash | text | Not Null |
| created_at | timestamptz | Default: now() |

## Building for Production

### Build Docker Image
```bash
docker build -t userapi:latest .
```

### Run in Production
```bash
docker run -d \
  -p 8080:8080 \
  -e DATABASE_URL="your-production-db-url" \
  -e JWT_SECRET="your-production-jwt-secret" \
  -e JWT_ISSUER="UserAPI" \
  -e JWT_AUDIENCE="UserAPI" \
  -e ASPNETCORE_ENVIRONMENT="Production" \
  --name userapi \
  userapi:latest
```

## Development

### Project Structure

- **Controllers**: Handle HTTP requests and responses
- **Services**: Contain business logic (UserService, JwtService)
- **Models**: Define data structures and DTOs
- **Data**: Database context and Entity Framework configuration

### Adding New Features

1. Create models in `Models/`
2. Add business logic in `Services/`
3. Create controllers in `Controllers/`
4. Update database schema in `Data/AppDbContext.cs`

## Troubleshooting

### Database Connection Issues
- Verify PostgreSQL is running
- Check DATABASE_URL environment variable
- Ensure database exists and credentials are correct

### JWT Token Issues
- Verify JWT_SECRET is at least 32 characters
- Check token expiration
- Ensure Authorization header format: `Bearer <token>`

### Docker Issues
- Check logs: `docker-compose logs userapi`
- Verify ports are not already in use
- Ensure Docker daemon is running

## License

This project is provided as-is for educational and commercial use.
