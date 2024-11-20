# Meter Readings Backend API

This backend API processes CSV files containing meter readings. It allows users to upload a CSV file, validate its contents, and save valid records to a SQLite database. The API provides endpoints for uploading files, retrieving records, and deleting all records.

## Features

- **CSV Upload**: Accepts CSV files with meter reading data.
- **Data Validation**: Validates the uploaded meter readings (e.g., checks for valid `AccountId` and meter reading format).
- **Database Integration**: Saves valid records to a SQLite database and updates existing records if necessary.
- **Error Handling**: Returns appropriate error messages if the file is invalid or an issue occurs during processing.

## Prerequisites

- **.NET 8.0 or higher** installed on your machine.
- **SQLite** as the database for storing meter readings.
- Ensure that the required tables (`MeterReadings`, `Users`, etc.) are created in the SQLite database.

- Unit tests havent been written due to time limitations but I have outlined in the validator some of the happy path tests cases