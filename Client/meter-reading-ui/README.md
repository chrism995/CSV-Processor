This is a [Next.js](https://nextjs.org) project bootstrapped with [`create-next-app`](https://nextjs.org/docs/app/api-reference/cli/create-next-app).

## Getting Started

Run npm i in order to ensure all packages are installed locally before running the application

Then run the development server:

```bash
npm run dev
# or
yarn dev
# or
pnpm dev
# or
bun dev
```

Open [http://localhost:3000](http://localhost:3000) with your browser to see the result.

# Next.js CSV Meter Reading Upload for Backend Processing

This project allows a user to upload a CSV file, which will be sent to the backend for processing. The backend will handle parsing the CSV data and performing necessary actions (e.g., saving records to a database if they meet the acceptance criteria outlined in the project documentation).

## Features

- **CSV Upload**: Allows users to upload a CSV file through a simple form.
- **Displays number of successful and failure results**
- **Table display**: Displays a list of users who have saved records
- **Delete**: Button to remove all saved user readings

## Prerequisites

- Node.js and npm installed on your local machine.
- The backend API must be running in order for the solution to work end 2 end

*** Make sure to add an .env.local file with the following entry ***
CSV_API_BASE_URI='http://localhost:5106/api/MeterReadings/'