import Sidebar from "@/components/layout/sidebar";

export default function HomePage() {
  return (
    <div className="flex min-h-screen bg-slate-100">
      <Sidebar />

      <main className="min-w-0 flex-1 p-6 md:p-8">
        <h1 className="text-3xl font-bold text-slate-900">
          TravelDNA dashboard
        </h1>
      </main>
    </div>
  );
}