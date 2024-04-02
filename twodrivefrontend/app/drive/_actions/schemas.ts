import { z } from "zod";

export const DriveSchema = z.object({
    id: z.number(),
    userid: z.number(),
    name: z.string(),
    imageId: z.string(),
    encryptionkey: z.string(),
    date_created: z.string(),
    date_modified: z.string()
});

export const imageSchema = z.object({
    id: z.number(),
    userid: z.number(),
    name: z.string(),
    path: z.string(),
    date_created: z.string(),
    date_modified: z.string()
});