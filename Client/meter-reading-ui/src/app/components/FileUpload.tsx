"use client";
import React, { useState, useEffect } from "react";
import useMeterReadingStore from "../store/meterReadingStore"; // Import the Zustand store

const FileUpload = () => {
  const [file, setFile] = useState<File | null>(null);
  const [successCount, setSuccessCount] = useState<number | null>(null);
  const [failureCount, setFailureCount] = useState<number | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const setMeterReadings = useMeterReadingStore(
    (state) => state.setMeterReadings
  );

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const uploadedFile = e.target.files ? e.target.files[0] : null;
    setFile(uploadedFile);
  };

  const handleUpload = async () => {
    if (!file) {
      return showError("Please select a file to upload.");
    }

    setLoading(true);

    const formData = new FormData();
    formData.append("file", file);

    try {
      const response = await fetch("/api/upload", {
        method: "POST",
        body: formData,
      });

      if (response.ok) {
        const result = await response.json();
        setSuccessCount(result.successCount);
        setFailureCount(result.failureCount);
        console.log(result);
        await fetchMeterReadings(); // Fetch meter readings after upload
      } else {
        showError(`Error: ${response.statusText}`);
      }
    } catch {
      showError("Error uploading file.");
    } finally {
      setLoading(false);
    }
  };

  // Fetch the meter readings
  const fetchMeterReadings = async () => {
    try {
      const response = await fetch("/api/get");
      if (response.ok) {
        const data = await response.json();
        setMeterReadings(data);
      } else {
        showError("Error fetching meter readings.");
      }
    } catch {
      showError("Error fetching meter readings.");
    }
  };

  const showError: (message: string) => void = (message) => {
    alert(message);
  };

  // Load meter readings on initial render
  useEffect(() => {
    const loadReadings = async () => {
      await fetchMeterReadings();
    };

    loadReadings();
  }, []);

  return (
    <div className="p-4 space-y-4">
      <input
        type="file"
        accept=".csv"
        onChange={handleFileChange}
        className="border border-gray-300 p-2 rounded-md"
      />

      <button
        onClick={handleUpload}
        disabled={loading} // Disable button when loading
        className="bg-blue-500 text-white p-2 rounded-md"
      >
        {loading ? "Uploading..." : "Upload"}
      </button>

      {successCount !== null && failureCount !== null && (
        <div className="mt-4">
          <p className="text-green-600">
            Successfully processed: {successCount} rows
          </p>
          <p className="text-red-600">Failed to process: {failureCount} rows</p>
        </div>
      )}
    </div>
  );
};

export default FileUpload;
