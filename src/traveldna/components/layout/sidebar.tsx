import { HomeIcon } from "@/components/ui/home";
import { EarthIcon } from "@/components/ui/earth";
import { PlaneTakeoffIcon } from "@/components/ui/plane-takeoff";
import { ChartBarIncreasingIcon } from "@/components/ui/chart-bar-increasing";
import Link from "next/link";
import SidebarNav from "./sidebar-nav";

const navigation = [
  { name: "Dashboard", href: "/dashboard", icon: HomeIcon },
  { name: "My world", href: "/dashboard/world", icon: EarthIcon },
  { name: "Journeys", href: "/dashboard/journey", icon: PlaneTakeoffIcon },
  { name: "Statistics", href: "/dashboard/statistics", icon: ChartBarIncreasingIcon },
];
export default function Sidebar() {
  return (
    <aside className="hidden min-h-screen w-64 flex-col bg-slate-900 text-white md:flex">
      <div className="flex justify-center h-16 items-center border-b border-slate-800 px-6">
        <Link href="/dashboard" className=" text-xl font-bold">
         <img
            src="/logo.svg"
            alt="TravelDNA"
            className="w-auto"
          />        
          </Link>
      </div>

      <SidebarNav />


      <div className="border-t border-slate-800 p-4">
        <div className="flex items-center gap-3">
          <div className="flex h-10 w-10 items-center justify-center rounded-full bg-indigo-500 font-semibold">
          </div>

          <div className="min-w-0">
            <p className="truncate text-sm font-medium">
              John Doe
            </p>
            <p className="truncate text-xs text-slate-400">
              john@example.com
            </p>
          </div>
        </div>
      </div>
    </aside>
  );
}