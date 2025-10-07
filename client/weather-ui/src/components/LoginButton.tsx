import { useAuth0 } from "@auth0/auth0-react";
import { useNavigate } from "react-router-dom";

const LoginButton = () => {
  const { loginWithRedirect, isAuthenticated } = useAuth0();
  const navigate = useNavigate();

  return (
    <button
      className="bg-white text-md md:text-lg text-indigo-800 font-semibold px-4 py-3 rounded-lg shadow-xl hover:bg-gray-200 transition mt-12"
      onClick={() =>
        isAuthenticated ? navigate("/weather") : loginWithRedirect()
      }
    >
      Get Started
    </button>
  );
};

export default LoginButton;
