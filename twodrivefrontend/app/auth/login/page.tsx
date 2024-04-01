"use client"

import { useState } from "react";
import { toast } from "react-toastify";
import { Login } from "../_actions/actions";

export default function LoginPage() {
    const [fetching, setFetching] = useState(false)

    async function ValidateLogin(formData: FormData) {
        // Validate the login form
        const username = formData.get("username")
        const password = formData.get("password")

        if (!username || !password) {
            toast.error("Please fill in all fields")
            return
        }

        setFetching(true)
        // Login the user
        const data = await Login(formData);
        setFetching(false)

        if (data.error) {
            toast.error(data.error)
            return
        }

        toast.success(data.message)
    }

    return (
        <div className="flex flex-col gap-2 justify-center items-center h-full w-full">
            <h2>Login</h2>
            <form
                action={ValidateLogin}
            className=" border-2 border-zinc-300 p-2 gap-2 rounded-xl flex flex-col">
                <label>
                    Username:
                </label>
                <input type="text" name="username" className=" px-3 py-2 rounded-md bg-zinc-200 text-black" placeholder="Username" />
                <label>
                    Password:
                </label>
                <input type="password" name="password" className=" px-3 py-2 rounded-md bg-zinc-200 text-black" placeholder="Password" />
                <button type="submit" className=" bg-blue-500 px-3 py-2 font-bold text-white rounded-md">Login</button>
            </form>
        </div>
    );
}
