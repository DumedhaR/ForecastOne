import { useNavigate } from "react-router-dom";

const GetStartButton = () => {
  const navigate = useNavigate();

  return (
    <button
      className="bg-white text-md md:text-lg text-indigo-800 font-semibold px-4 py-3 rounded-lg shadow-xl hover:bg-gray-200 transition"
      onClick={() => navigate("/signIn")}
    >
      Get Started
    </button>
  );
};

export default GetStartButton;
