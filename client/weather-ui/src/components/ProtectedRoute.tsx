import { withAuthenticationRequired, useAuth0 } from "@auth0/auth0-react";
import { type ReactNode } from "react";
import LoadingSping from "./LoadingSpin";

interface ProtectedRouteProps {
  children: ReactNode;
}

const ProtectedRoute = ({ children }: ProtectedRouteProps) => {
  const { isLoading } = useAuth0();
  const component = () => children;

  if (isLoading) {
    return (
      <div className="flex justify-center items-center w-screen h-screen">
        <LoadingSping />
      </div>
    );
  }

  const ProtectedComponent = withAuthenticationRequired(component, {
    // Show a message while the user waits to be redirected to the login page.
    onRedirecting: () => (
      <div className="min-h-screen flex justify-center items-center text-lg">
        Redirecting to the login page...
      </div>
    ),
  });

  return <ProtectedComponent />;
};

export default ProtectedRoute;
