import { User } from "@/types/types";
import { z } from "zod";

export const UserSchema = z.object({ 
    id: z.number(),
    name: z.string(),
    email: z.string(),
    date_created: z.string(),
    date_modified: z.string()
});