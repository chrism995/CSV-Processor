"use client";
import React, { useState } from "react";

const FileUpload = () => {
  const [file, setFile] = useState<File | null>(null);
  const [successCount, setSuccessCount] = useState<number | null>(null);
  const [failureCount, setFailureCount] = useState<number | null>(null);

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const uploadedFile = e.target.files ? e.target.files[0] : null;
    setFile(uploadedFile);
  };

  const handleUpload = async () => {
    if (!file) {
      alert("Please select a file to upload.");
      return;
    }

    const formData = new FormData();
    formData.append("file", file); // Append file to form data

    try {
      const response = await fetch("/api/upload", {
        method: "POST",
        body: formData, // Send the form data
      });

      if (response.ok) {
        const result = await response.json();
        setSuccessCount(result.successCount);
        setFailureCount(result.failureCount);
        console.log(result);
      }
    } catch (error) {
      console.error("Error uploading file:", error);
      alert("Error uploading file.");
    }
  };

  return (
    <div className="p-4 space-y-4">
      {/* File Input */}
      <input
        type="file"
        accept=".csv"
        onChange={handleFileChange}
        className="border border-gray-300 p-2 rounded-md"
      />

      {/* Upload Button */}
      <button
        onClick={handleUpload}
        className="bg-blue-500 text-white p-2 rounded-md"
      >
        Upload
      </button>

      {/* Display Success and Failure Counts */}
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
