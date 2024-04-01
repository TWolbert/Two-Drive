import axios from 'axios';
import { NextResponse } from 'next/server'
import type { NextRequest } from 'next/server'

const backendURL = process.env.BACKEND_URL
 
// This function can be marked `async` if using `await` inside
export async function middleware(request: NextRequest) {
    // Check if user Cookie is set
    if (!request.cookies.get('token')) {
        return NextResponse.redirect(new URL('/auth/login', request.url));
    }

    const token = request.cookies.get('token')

    const tokenValue = token?.value

    // Check if token is valid
    const res = await fetch(`${backendURL}/User/validate`, { 
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ Token: tokenValue }),
    });

    const data = await res.json()

    if (res.status !== 200) { 
        return NextResponse.redirect(new URL('/auth/login', request.url));
    }

    if (data.error) {
        return NextResponse.redirect(new URL('/auth/login', request.url));
    }

    return NextResponse.next()
}
 
// See "Matching Paths" below to learn more
export const config = {
    matcher: '/drive/:path*'
}