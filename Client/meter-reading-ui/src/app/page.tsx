import DeleteMeterReadingsButton from "./components/DeleteButton";
import FileUpload from "./components/FileUpload";
import MeterReadingsList from "./components/MeterReadingList";

export default function Home() {
  return (
    <div className="min-h-screen p-8 sm:p-20 bg-gray-50 font-sans">
      <header className="text-center mb-10">
        <h1 className="text-3xl font-bold text-gray-800">
          Meter Reading Management
        </h1>
        <p className="text-gray-600">
          Upload, View, and Manage Your Meter Readings
        </p>
      </header>

      <main className="space-y-12">
        {/* File Upload & Result Display Section */}
        <section className="flex flex-col sm:flex-row sm:gap-10 items-center sm:items-start justify-center bg-white shadow-md rounded-lg p-8">
          <div className="flex flex-col items-center sm:items-start">
            <h2 className="text-xl font-semibold text-gray-700 mb-4">
              Upload Your File
            </h2>
            <FileUpload />
          </div>
        </section>

        {/* Meter Readings List Section */}
        <section className="bg-white shadow-md rounded-lg p-8">
          <h2 className="text-xl font-semibold text-gray-700 mb-6">
            Meter Readings List
          </h2>
          <MeterReadingsList />
        </section>

        {/* Delete Button Section */}
        <section className="text-center">
          <DeleteMeterReadingsButton />
        </section>
      </main>

      <footer className="text-center text-gray-500 mt-12">
        &copy; {new Date().getFullYear()} Meter Reading Management System
      </footer>
    </div>
  );
}
