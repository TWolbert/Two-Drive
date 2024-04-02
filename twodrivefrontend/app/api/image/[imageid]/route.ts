import axios from "axios";
import { NextRequest, NextResponse } from "next/server";

export async function GET(request: NextRequest) {
    try {
        // Get the image id from the request URL
        const url = new URL(request.url);
        const imageid = url.pathname.split('/').pop(); // Extract the image ID from the path

        // Fetch the image from the backend server
        const response = await axios.get(`${process.env.BACKEND_URL}/Image/${imageid}`, {
            responseType: 'arraybuffer' // Ensure response is treated as binary data
        });

        // Check if image retrieval was successful
        if (response.status === 200 && response.data) {
            // Return the image as a response
            return new Response(response.data, {
                headers: { 'content-type': 'image/png' } // Adjust content type based on actual image type
            });
        }

        // Return a 404 response if image was not found
        return new Response(null, { status: 404 });
    } catch (error) {
        console.error("Error fetching image:", error);
        return new Response(null, { status: 500 });
    }
}
