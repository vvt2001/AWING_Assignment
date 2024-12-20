# AWING Assignment

## Prerequisites

Ensure you have the following installed:

- [Node.js](https://nodejs.org/) (v16+)
- [.NET SDK](https://dotnet.microsoft.com/download) (v8.0+)
- [SQL Server](https://www.microsoft.com/sql-server)

## Installation

### 1. Clone the Repository
```bash
git clone https://github.com/vvt2001/AWING_Assignment.git
cd AWING_Assignment
```

### 2. Set Up the Backend (C# with Entity Framework)
#### 1. Navigate to the backend project directory:
```bash
cd AWING_Assignment
```

#### 2. Update the database connection string in appsettings.json:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=your-server;Database=your-database;User Id=your-user;Password=your-password;"
}
```

#### 3. Navigate to the backend Data project directory:
```bash
cd ..
cd AWING_Assignment_Data
```

#### 4. Update the database connection string in DbContextFactory.cs:
```C#
var connectionString = "Server=your-server;Database=your-database;User Id=your-user;Password=your-password;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=True";
```

#### 5. Run Entity Framework migrations:
```bash
dotnet ef database update
```

#### 6. Start the backend server:
```bash
cd ..
cd AWING_Assignment
dotnet run
```

### 3. Set Up the Frontend (Node.js)
#### 1. Navigate to the frontend project directory:
```bash
cd awing_assignment_web
```
#### 2. Install dependencies:
```bash
npm install
```
#### 3. Start the frontend server:
```bash
npm start
```

## Running the Project
### 1. Ensure the backend server is running:
```arduino
http://localhost:7284
```

### 2. Access the frontend:
```arduino
http://localhost:3000
```

## License
This project is licensed under the MIT License. See the LICENSE file for details.


