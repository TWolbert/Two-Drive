"use server";

import { ErrorResponse, User, ErrorType, Drive } from "@/types/types";
import axios from "axios";
import { cookies } from "next/headers";

const backendURL = process.env.BACKEND_URL;

export type GetDrivesResponse = {
    drives: Drive[];
};

export async function GetDrives(
    User: User
): Promise<GetDrivesResponse | ErrorResponse> {
    try {
        const token = cookies().get("token");

        if (!token) {
            return {
                error: "No token found",
                type: ErrorType.Frontend,
            };
        }

        const res = await axios.get(`${backendURL}/Drive`, {
            headers: {
                Authorization: `Bearer ${token.value}`,
            },
            params: {
                user_id: User.id,
            },
        });

        return res.data as GetDrivesResponse;
    } catch (e) {
        return {
            error: "Something went wrong. Please try again later.",
            type: ErrorType.Backend,
        };
    }
}

export type CreateDriveResponse = {
    driveUrl: string;
};

export async function CreateDrive(
    formData: FormData, imageUrl: string,
): Promise<CreateDriveResponse | ErrorResponse> {
    try {
        const token = cookies().get("token");

        if (!token) {
            return {
                error: "No token found",
                type: ErrorType.Frontend,
            };
        }

        const res = await axios.post(
            `${backendURL}/Drive`,
            {
                name: formData.get("drivename"),
                encryptionkey: formData.get("encryptionkey"),
                imageUrl: imageUrl,
            },
            {
                headers: {
                    Authorization: `Bearer ${token.value}`,
                },
            }
        );

        return res.data as CreateDriveResponse;
    } catch (e) {
        return {
            error: "Something went wrong. Please try again later.",
            type: ErrorType.Backend,
        };
    }
}
