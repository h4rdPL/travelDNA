import Sidebar from "@/components/layout/sidebar";

export default function DashboardLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <div className="flex min-h-screen bg-slate-100">
      <Sidebar />

      <main className="min-w-0 flex-1">
        <header className="flex h-16 items-center border-b bg-white px-6 md:hidden">
          <button
            type="button"
            className="text-xl"
            aria-label="Open menu"
          >
            ☰
          </button>

          <span className="ml-4 font-bold">MyApp</span>
        </header>

        <div className="p-4 md:p-8">
          {children}
        </div>
      </main>
    </div>
  );
}