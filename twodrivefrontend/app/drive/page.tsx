/* eslint-disable @next/next/no-img-element */
import { ErrorType } from "@/types/types";
import { GetUser } from "../auth/_actions/actions";
import { GetDrives } from "./_actions/actions";
import Link from "next/link";
import Image from "next/image";

export default async function DriveIndex() {
    const User = await GetUser();

    if ("error" in User) {
        return (
            <div>
                <h1>Error</h1>
                <p>{User.error}</p>
                <p>
                    Error was caused by{" "}
                    {User.type === ErrorType.Backend
                        ? " the backend (C#)"
                        : " the frontend (NextJS)"}
                </p>
            </div>
        );
    }

    const drives = await GetDrives(User);

    if ("error" in drives && drives.error !== null) {
        console.log("there was an error in drives");
        return (
            <div>
                <h1>Error</h1>
                <p>{drives.error}</p>
                <p>
                    Error was caused by{" "}
                    {drives.type === ErrorType.Backend
                        ? " the backend (C#)"
                        : " the frontend (NextJS)"}
                </p>
            </div>
        );
    }

    if ("drives" in drives) {
        if (drives.drives.length === 0) {
            return (
                <div className=" flex h-full w-full items-center justify-center flex-col">
                    <h1 className=" font-bold text-3xl">No drives found</h1>
                    <p>
                        It seems like you don&apos;t have any drives yet. You
                        can create one by clicking the button below.
                    </p>
                    <Link
                        href={"/drive/create"}
                        className=" bg-gradient-to-tr shadow-inner from-blue-500 to-blue-700 rounded-md px-3 border-2 border-transparent py-2 transition-all hover:border-white active:scale-95"
                    >
                        Create Drive
                    </Link>
                </div>
            );
        }

        return (
            <div className=" flex h-full w-full items-center justify-center flex-col gap-2">
                <h1 className=" font-bold text-3xl">Your drives</h1>
                <ul>
                    {drives.drives.map((drive) => (
                        <Link href={`/drive/${drive.id}`} key={drive.id} className="group">
                            <div className=" group-hover:-translate-y-1 bg-gradient-to-tr transition-all from-blue-500 to-blue-700 p-2 rounded-xl">
                                <Image
                                    width={100}
                                    height={100}
                                    src={`/api/image/${drive.imageId}`}
                                    alt={`${drive.name}'s icon`}
                                    className=" aspect-square border-2 border-blue-500 rounded-xl object-cover group-hover:-translate-y-2 transition-all shadow-lg"
                                />
                                <h1 className="font-bold">{drive.name}</h1>
                            </div>
                        </Link>
                    ))}
                </ul>

                <Link
                    href={"/drive/create"}
                    className=" bg-gradient-to-tr shadow-inner from-blue-500 to-blue-700 rounded-md px-3 border-2 border-transparent py-2 transition-all hover:border-white active:scale-95"
                >
                    Create Drive
                </Link>
            </div>
        );
    }
}
