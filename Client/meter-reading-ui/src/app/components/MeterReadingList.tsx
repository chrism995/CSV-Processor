"use client";
import useMeterReadingStore from "../store/meterReadingStore";

export default function MeterReadingsList() {
  const meterReadings = useMeterReadingStore((state) => state.meterReadings);

  const formatDate = (dateString: string) => {
    const date = new Date(dateString);
    return date.toLocaleString();
  };

  return (
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4 text-center">Meter Readings</h1>
      {meterReadings.length > 0 ? (
        <table className="w-full border-collapse border border-gray-300">
          <thead>
            <tr className="bg-gray-200">
              <th
                scope="col"
                className="border border-gray-300 px-4 py-2 text-left"
              >
                Account ID
              </th>
              <th
                scope="col"
                className="border border-gray-300 px-4 py-2 text-left"
              >
                Date/Time
              </th>
              <th
                scope="col"
                className="border border-gray-300 px-4 py-2 text-left"
              >
                Value
              </th>
            </tr>
          </thead>
          <tbody>
            {meterReadings.map((reading) => (
              <tr
                key={`${reading.accountId}-${reading.meterReadingDateTime}`}
                className="odd:bg-white even:bg-gray-50"
              >
                <td className="border border-gray-300 px-4 py-2">
                  {reading.accountId}
                </td>
                <td className="border border-gray-300 px-4 py-2">
                  {formatDate(reading.meterReadingDateTime)}
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
