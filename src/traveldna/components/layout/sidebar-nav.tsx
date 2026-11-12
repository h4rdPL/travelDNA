"use client";

import { useRef, type ComponentType, type Ref } from "react";
import Link from "next/link";
import { HomeIcon } from "@/components/ui/home";
import { EarthIcon } from "@/components/ui/earth";
import { PlaneTakeoffIcon } from "@/components/ui/plane-takeoff";
import { ChartBarIncreasingIcon } from "@/components/ui/chart-bar-increasing";

type IconHandle = {
  startAnimation: () => void;
  stopAnimation: () => void;
};

type IconComponent = ComponentType<{
  className?: string;
  ref?: Ref<IconHandle>;
}>;

const navigation: { name: string; href: string; icon: IconComponent }[] = [
  { name: "Dashboard", href: "/dashboard", icon: HomeIcon },
  { name: "My world", href: "/dashboard/my-world", icon: EarthIcon },
  { name: "Journeys", href: "/dashboard/journeys", icon: PlaneTakeoffIcon },
  { name: "Statistics", href: "/dashboard/statistics", icon: ChartBarIncreasingIcon },
];

function NavItem({
  name,
  href,
  Icon,
}: {
  name: string;
  href: string;
  Icon: IconComponent;
}) {
  const iconRef = useRef<IconHandle>(null);

  return (
    <Link
      href={href}
      onMouseEnter={() => iconRef.current?.startAnimation()}
      onMouseLeave={() => iconRef.current?.stopAnimation()}
      className="flex items-center gap-3 rounded-lg px-3 py-2.5 text-sm font-medium text-slate-300 transition hover:bg-slate-800 hover:text-white"
    >
      <Icon ref={iconRef} className="h-5 w-5" />
      {name}
    </Link>
  );
}

export default function SidebarNav() {
  return (
    <nav className="flex-1 space-y-2 p-4">
      {navigation.map((item) => (
        <NavItem
          key={item.name}
          name={item.name}
          href={item.href}
          Icon={item.icon}
        />
      ))}
    </nav>
  );
}