import Image from "next/image";

export default function Home() {
  return (
    <div>
      <h1>TwoDrive</h1>
      <p>TwoDrive is a cloud storage service that allows you to store and share files.</p>
      <Image src="/twodrive.png" alt="TwoDrive Logo" width={500} height={500} />
    </div>
  );
}
