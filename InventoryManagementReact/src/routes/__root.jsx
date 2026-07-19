import { createRootRoute, Link, Outlet } from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";

const RootLayout = () => (
  <>
    <div data-theme="light" className="min-h-screen bg-base-200">
      <nav className="navbar bg-base-100 shadow-sm">
        <div className="flex-1">
          <a className="btn btn-ghost text-xl">Inventory Management</a>
        </div>
        <div className="flex-none">
          <ul className="menu menu-horizontal px-1">
            <li>
              <Link to="/">Home</Link>
            </li>
            <li>
              <Link to="/InventoryProcess">Inventory Process</Link>
            </li>
          </ul>
        </div>
      </nav>
      <div className="container mx-auto px-4 py-6 ">
        <Outlet />
      </div>

      <TanStackRouterDevtools />
    </div>
  </>
);

export const Route = createRootRoute({ component: RootLayout });
