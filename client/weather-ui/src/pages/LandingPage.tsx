import { WiDayCloudy } from "react-icons/wi";
import GetStartButton from "../components/GetStartButton";
// import { useEffect } from "react";
// import { useNavigate } from "react-router-dom";
// import LoadingSping from "../components/LoadingSpin";

const LandingPage = () => {
  // const navigate = useNavigate();

  // useEffect(() => {
  //   if (!isLoading) {
  //     if (isAuthenticated) {
  //       navigate("/weather");
  //     }
  //   }
  // }, [isAuthenticated, isLoading, navigate]);

  // if (isLoading) {
  //   return (
  //     <div className="flex justify-center items-center w-screen h-screen">
  //       <LoadingSping />
  //     </div>
  //   );
  // }

  // if (isAuthenticated) {
  //   return null;
  // }

  return (
    <div
      className="w-full h-screen flex gap-1 items-center justify-center flex-col bg-gradient-to-br
      from-blue-600 via-indigo-600 to-violet-600 text-white"
    >
      <WiDayCloudy className="text-8xl md:text-9xl font-semibold" />
      <h2 className="text-4xl md:text-5xl font-semibold">Welcome to</h2>
      <h1 className="text-6xl md:text-7xl font-semibold mb-6">ForecastOne</h1>
      <p className="text-md md:text-lg mb-12">
        Check the Latest Weather Updates <br /> Anytime, Anywhere
      </p>
      <GetStartButton />
    </div>
  );
};

export default LandingPage;
