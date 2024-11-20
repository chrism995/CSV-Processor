import { NextRequest, NextResponse } from "next/server";
import axios from "axios";

export async function POST(request: NextRequest) {
  try {
    const apiUrl = `${process.env.CSV_API_BASE_URI}meter-reading-uploads`;
    const formData = new FormData();
    const file = await request.formData();

    const fileUpload = file.get("file");

    if (!fileUpload) {
      return new NextResponse("No file uploaded", { status: 400 });
    }

    formData.append("file", fileUpload);

    const response = await axios.post(apiUrl, formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });

    if (response.status === 200) {
      // Return the same response data to the frontend
      return NextResponse.json(response.data, { status: 200 });
    } else {
      console.error(`Error from .NET API: ${response.statusText}`);
      return NextResponse.json(
        { message: "Error from .NET API", details: response.statusText },
        { status: 500 }
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
