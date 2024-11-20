import { NextResponse } from "next/server";
import axios from "axios";

export async function DELETE() {
  const dotNetApiUrl = "http://localhost:5106/api/MeterReadings/delete-all";

  return axios
    .delete(dotNetApiUrl)
    .then((response) => {
      if (response.status === 200) {
        return NextResponse.json(response.data, { status: 200 });
      } else {
        console.error(`Error from .NET API: ${response.statusText}`);
        return NextResponse.json(
          { message: "Error from .NET API", details: response.statusText },
          { status: 500 }
        );
      }
    })
    .catch((error) => {
      console.error("Error calling .NET API:", error);
      return NextResponse.json(
        { message: "Error calling .NET API" },
        { status: 500 }
      );
    });
}
