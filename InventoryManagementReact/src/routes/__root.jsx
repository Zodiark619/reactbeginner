import { createRootRoute, Link, Outlet } from "@tanstack/react-router";
import { TanStackRouterDevtools } from "@tanstack/react-router-devtools";

const RootLayout = () => (
  <>
    <div data-theme="light" className="min-h-screen bg-base-200">
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
    </div>
  </>
);

export const Route = createRootRoute({ component: RootLayout });
