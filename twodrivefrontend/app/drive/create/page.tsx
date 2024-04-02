"use client";

import { useState } from "react";
import { toast } from "react-toastify";
import { CreateDrive } from "../_actions/actions";
import axios from "axios";

export default function DriveCreatePage() {
    const [fetching, setFetching] = useState(false);

    async function CreateDriveValidate(formData: FormData) {
        setFetching(true);

        // Validate the form
        const drivename = formData.get("drivename");
        const encryptionkey = formData.get("encryptionkey");
        const confirmencryptionkey = formData.get("confirmencryptionkey");
        const driveicon = formData.get("driveicon");

        if (
            !drivename ||
            !encryptionkey ||
            !confirmencryptionkey ||
            !driveicon
        ) {
            toast.error("Please fill in all fields");
            return;
        }

        if (encryptionkey !== confirmencryptionkey) {
            toast.error("Encryption keys do not match");
            return;
        }

        let imageUrl = "";

        const backendUrl = process.env.NEXT_PUBLIC_BACKEND_URL;

        // Upload the image
        const imageRes = await axios.post(
            `${backendUrl}/Drive/Icon`,
            {
                image: driveicon,
            },
            {
                // Set content type to multipart form data
                headers: {
                    "Content-Type": "multipart/form-data",
                },
            }
        );

        if (imageRes.status !== 200) {
            toast.error("Error uploading image (Not 200)");
            console.log(imageRes);
            return;
        }

        const imageResData = imageRes.data;

        if (imageResData.error) {
            toast.error("Error uploading image (Error in response)");
            console.log(imageResData);
            return;
        }

        imageUrl = imageResData.path;

        toast("Image uploaded");

        // Send the form
        const res = await CreateDrive(formData, imageUrl);
        setFetching(false);

        if ("error" in res && res.error !== null) {
            console.log(res.error);
            toast.error(res.error);
            return;
        }

        toast.success("Drive created successfully");

        if ("driveUrl" in res) {
            window.location.href = `${res.driveUrl.split("/").slice(-1)}`;
        }
    }

    return (
        <div className="flex flex-col gap-2 justify-center items-center h-full w-full">
            <h2>Create Drive</h2>
            <form
                autoComplete="off"
                action={CreateDriveValidate}
                className=" border-2 border-zinc-300 p-2 gap-2 rounded-xl flex flex-col"
            >
                <label>Drive Name:</label>
                <input
                    autoComplete="off"
                    type="text"
                    name="drivename"
                    className=" px-3 py-2 rounded-md bg-zinc-200 text-black"
                    placeholder="My Drive"
                />
                <label>Encryption Key:</label>
                <input
                    autoComplete="off"
                    type="password"
                    name="encryptionkey"
                    className=" px-3 py-2 rounded-md bg-zinc-200 text-black"
                    placeholder="Encryption Key"
                />
                <label>Confirm Encryption Key:</label>
                <input
                    autoComplete="off"
                    type="password"
                    name="confirmencryptionkey"
                    className=" px-3 py-2 rounded-md bg-zinc-200 text-black"
                    placeholder="Confirm Encryption Key"
                />

                <label>Drive Icon:</label>
                <input
                    type="file"
                    name="driveicon"
                    className=" px-3 py-2 rounded-md bg-zinc-200 text-black file:bg-gradient-to-tr file:from-blue-500 file:to-blue-700 file:shadow-inner file:text-white file:border-none file:rounded-md file:px-3 file:py-2 file:font-bold"
                />
                <p className=" text-xs text-zinc-500">
                    Please keep in mind the drive icon will be stored
                    un-encrypted.
                </p>
                <button
                    type="submit"
                    className=" bg-blue-500 px-3 py-2 font-bold text-white rounded-md"
                    disabled={fetching}
                >
                    Create Drive
                </button>
            </form>
        </div>
    );
}
