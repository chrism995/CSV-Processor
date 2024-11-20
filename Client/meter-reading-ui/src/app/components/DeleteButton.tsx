"use client";
import React, { useState } from "react";

export default function DeleteMeterReadingsButton() {
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState("");

  const handleDelete = async () => {
    setLoading(true);
    setMessage("");

    try {
      const response = await fetch("/api/delete", {
        method: "DELETE",
      });

      if (response.ok) {
        const data = await response.json();
        setMessage("Successfully deleted meter readings!");
        console.log("Response:", data);
      } else {
        const errorData = await response.json();
        setMessage(`Error: ${errorData.message}`);
        console.error("Error response:", errorData);
      }
    } catch (error) {
      console.error("Network error:", error);
      setMessage("Error deleting meter readings. Please try again.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="text-center">
      <button
        onClick={handleDelete}
        disabled={loading}
        className={`w-full sm:w-auto mt-4 px-6 py-3 text-white font-bold rounded-lg transition-all ${
          loading
            ? "bg-gray-400 cursor-not-allowed"
            : "bg-red-600 hover:bg-red-700 focus:outline-none focus:ring-4 focus:ring-red-300"
        }`}
      >
        {loading ? "Deleting..." : "Delete Meter Readings"}
      </button>
      {message && <p className="mt-4 text-center text-gray-700">{message}</p>}
    </div>
  );
}
