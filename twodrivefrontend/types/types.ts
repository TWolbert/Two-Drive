import { UserSchema } from "@/app/auth/_actions/schemas";
import { DriveSchema, imageSchema as ImageSchema } from "@/app/drive/_actions/schemas";
import { z } from "zod";

export type User = z.infer<typeof UserSchema>;
export type Drive = z.infer<typeof DriveSchema>
export type Image = z.infer<typeof ImageSchema>

export interface ErrorResponse {
    error: string,
    type: ErrorType
}

export enum ErrorType {
    "Backend",
    "Frontend"
}