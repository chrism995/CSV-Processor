"use client";
import { useState, useEffect } from "react";

interface MeterReading {
  accountId: number;
  meterReadingDateTime: string; // Use string because dates are typically returned as ISO strings from APIs
  meterReadValue: number;
}

export default function MeterReadingsList() {
  const [meterReadings, setMeterReadings] = useState<MeterReading[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");

  // Fetch meter readings from the API
  useEffect(() => {
    const fetchMeterReadings = async () => {
      try {
        const response = await fetch("/api/get");
        if (response.ok) {
          const data = await response.json();
          setMeterReadings(data);
        } else if (response.status === 204) {
          setError("No meter readings found.");
        } else {
          setError("Failed to fetch meter readings.");
        }
      } catch (err) {
        console.error("Error fetching meter readings:", err);
        setError("An error occurred while fetching meter readings.");
      } finally {
        setLoading(false);
      }
    };

    fetchMeterReadings();
  }, []);

  if (loading) {
    return (
      <p className="text-center text-gray-500">Loading meter readings...</p>
    );
  }

  if (error) {
    return <p className="text-center text-red-500 font-bold">{error}</p>;
  }

  return (
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4 text-center">Meter Readings</h1>
      {meterReadings.length > 0 ? (
        <table className="w-full border-collapse border border-gray-300">
          <thead>
            <tr className="bg-gray-200">
              <th className="border border-gray-300 px-4 py-2 text-left">
                Account ID
              </th>
              <th className="border border-gray-300 px-4 py-2 text-left">
                Date/Time
              </th>
              <th className="border border-gray-300 px-4 py-2 text-left">
                Value
              </th>
            </tr>
          </thead>
          <tbody>
            {meterReadings.map((reading) => (
              <tr
                key={reading.accountId}
                className="odd:bg-white even:bg-gray-50"
              >
                <td className="border border-gray-300 px-4 py-2">
                  {reading.accountId}
                </td>
                <td className="border border-gray-300 px-4 py-2">
                  {new Date(reading.meterReadingDateTime).toLocaleString()}
                </td>
                <td className="border border-gray-300 px-4 py-2">
                  {reading.meterReadValue}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        <p className="text-center text-gray-500">
          No meter readings available.
        </p>
      )}
    </div>
  );
}
