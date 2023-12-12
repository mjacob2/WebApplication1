# Todo List API

## Description

This Todo List API is a web service implemented using ASP.NET Core and Entity Framework Core. The API facilitates managing a list of todo items, allowing users to perform Create, Read, Update, and Delete (CRUD) operations on todo items.

## Prerequisites

Before you begin, ensure you have met the following requirements:

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later installed on your machine
- An SQL Server instance available (LocalDB, SQL Express, or a full SQL Server)
- Entity Framework Core tools installed (for running migrations)

## Setup and Installation

Follow these steps to get your development environment set up:

1. **Clone the repository**

   Use the following git command to clone the project repository:

   ```bash
   git clone https://github.com/your-username/todo-api.git
   cd todo-api
Replace https://github.com/your-username/todo-api.git with the actual URL of your repository.

Configure the database

Edit appsettings.json to include your SQL Server connection string:

json


"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TodoListDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
Run migrations

Ensure your database is updated to the latest schema:

bash


dotnet ef database update
Start the application

Launch the application with the following command:

bash


dotnet run
By default, the API will be accessible at http://localhost:5000.

Accessing the API

Interact with the API using tools such as Postman, or access Swagger UI at http://localhost:5000/swagger if enabled, to explore and test the API endpoints.

Running Tests
You can execute the automated test suite using the following command:

bash


dotnet test
API Endpoints
Below are some of the available endpoints:

GET /api/todos - Retrieves all todo items.
GET /api/todos/{id} - Retrieves a single todo item by ID.
POST /api/todos - Creates a new todo item.
PUT /api/todos/{id} - Updates an existing todo item.
DELETE /api/todos/{id} - Deletes a todo item by ID.
Contributing
Contributions are welcome! For major changes, please open an issue first to discuss what you would like to change.

To contribute, follow these steps:

Fork the repository.
Create a new branch (git checkout -b feature/AmazingFeature).
Make the necessary changes/additions in your branch.
Commit your changes (git commit -m 'Add some AmazingFeature').
Push to the branch (git push origin feature/AmazingFeature).
Open a pull request.
License
This project is licensed under the MIT License - see the LICENSE file for details.

Feedback Using AI (ChatGPT)
Was it easy to complete the task using AI?
 NO
How long did task take you to complete? x minutes/hours
few hours
Was the code ready to run after generation?
No
What did you have to change to make it usable?
prepare database
Which challenges did you face during completion of the task?
app project is multi files. Response is only one window.
Which specific prompts you learned as a good practice to complete the task?
to give more input data at the beginning
