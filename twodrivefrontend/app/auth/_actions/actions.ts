"use server";

import { ErrorResponse, User, ErrorType } from "@/types/types";
import axios from "axios";
import { cookies } from "next/headers";
import { z } from "zod";
import { UserSchema } from "./schemas";

const backendURL = process.env.BACKEND_URL;

type AuthLoginResponse = {
    token: string | null;
    message: string | null;
    error: string | null;
};

type DriveIconResponse = {
    iconUrl: string | null;
    message: string | null;
    error: string | null;
};

export async function Login(formData: FormData) {
    const res = await axios.post(`${backendURL}/User/login`, {
        Name: formData.get("username"),
        Password: formData.get("password"),
    });

    const data: AuthLoginResponse = res.data;

    if (data.token) {
        cookies().set("token", data.token);
    }

    return data;
}

export async function GetUser(): Promise<User | ErrorResponse> {
    const token = cookies().get("token");

    if (!token) {
        return {
            error: "No token found",
            type: ErrorType.Frontend,
        };
    }

    const res = await axios.post(`${backendURL}/User/me`, {
        token: token?.value,
    });

    const User = UserSchema.safeParse(res.data);

    if (!User.success) {
        return {
            error: "Invalid data sent from server",
            type: ErrorType.Backend,
        };
    }

    return User.data;
}
