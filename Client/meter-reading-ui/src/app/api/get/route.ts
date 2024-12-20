import { NextResponse } from "next/server";
import axios from "axios";

export async function GET() {
  try {
    const apiUrl = `${process.env.CSV_API_BASE_URI}get-all`;

    const response = await axios.get(apiUrl, {
      headers: {
        "Content-Type": "application/json",
      },
    });

    if (response.status === 200) {
      console.log("Response from .NET API:", response.data);
      return NextResponse.json(response.data, { status: 200 });
    } else if (response.status === 204) {
      // Handle no content case
      return NextResponse.json(
        { message: "No meter readings found." },
        { status: 204 }
      );
    } else {
      console.error(`Error from .NET API: ${response.statusText}`);
      return NextResponse.json(
        { message: "Error from .NET API", details: response.statusText },
        { status: response.status }
      );
    }
  } catch (error) {
    console.error("Error calling .NET API:", error);
    return NextResponse.json(
      { message: "Error calling .NET API" },
      { status: 500 }
    );
  }
}
