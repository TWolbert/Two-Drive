"use server"

import axios from "axios"
import { cookies } from "next/headers"

const backendURL = process.env.BACKEND_URL

type AuthLoginResponse = {
    token: string | null,
    message: string | null,
    error: string | null
}

export async function Login(formData: FormData) {
    const res = await axios.post(`${backendURL}/User/login`, {
        Name: formData.get("username"),
        Password: formData.get("password")
    })

    const data: AuthLoginResponse = res.data

    if (data.token) {
        cookies().set("token", data.token);
    }

    return data
}