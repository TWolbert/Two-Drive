export default function DrivePage({ params } : { params: { drive: string } }) {
    return (
        <div>
            <h1>Drive: {params.drive}</h1>
            <p>Here you can view and manage your files.</p>
        </div>
    );
 }