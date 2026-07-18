import { createRootRoute, Link, Outlet } from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";

const RootLayout = () => (
  <>
    {/* <div data-theme="light" className="min-h-screen bg-base-200">
      <div className="container mx-auto p-6">
        <div className="bg-base-100 rounded-lg shadow">
          <div className="navbar bg-base-100 border-b">
            <div className="flex gap-2">
              <Link to="/" className="btn btn-ghost">
                Home
              </Link>

              <Link to="/about" className="btn btn-ghost">
                About
              </Link>
            </div>
          </div>

          <div className="container mx-auto p-6">
            <Outlet />
          </div>
        </div>
      </div>

      <TanStackRouterDevtools />
    </div> */}
    <div data-theme="light" className="min-h-screen bg-base-200">
      <nav className="navbar bg-base-100 shadow-sm">
        <div className="flex-1">
          <a className="btn btn-ghost text-xl">MySite</a>
        </div>
        <div className="flex-none">
          <ul className="menu menu-horizontal px-1">
            <li>
              <Link to="/">Home</Link>
            </li>
            <li>
              <Link to="/about">About</Link>
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
