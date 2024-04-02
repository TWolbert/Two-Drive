import Link from "next/link";
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';

export default function AuthLayout({
    children,
}: {
    children: React.ReactNode;
}) {
    return (
        <div className="flex flex-col gap-2 h-screen w-full">
            <div className="flex items-center justify-center flex-col">
                <h1 className=" font-bold text-3xl">Two<span className=" bg-gradient-to-tr from-blue-500 to-blue-700 text-transparent bg-clip-text">Drive</span></h1>
                <div className=" flex gap-2">
                    <Link href="/drive" className="w-fit">
                        <span className="text-blue-500 w-fit">Back to drive selector</span>
                    </Link>
                </div>
            </div>

            <div className="h-full">
                {children}
            </div>
        </div>
    );
}
