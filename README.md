# RESTful API Automation Tests (C# + xUnit)

## Overview
This project contains automated tests for https://restful-api.dev API.

### Test Scenarios Covered

1. Get all objects
2. Create object using POST
3. Get object by ID
4. Update object
5. Delete object
6. Verify deleted object is removed

## Tech Stack

- .NET 8
- xUnit
- FluentAssertions
- HttpClient

## Setup Instructions

### 1. Install .NET SDK
https://dotnet.microsoft.com/download

### 2. Clone repository

git clone <repo-url>

### 3. Navigate to project

cd RestfulApiTests

### 4. Restore packages

dotnet restore

### 5. Run tests

dotnet test

## Project Structure

- Models → Data models
- Client → API client
- Fixtures → Test setup
- Tests → Test cases