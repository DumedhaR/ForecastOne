import Footer from "../components/Footer";
import { Outlet } from "react-router-dom";
import ProfileMenu from "../components/ProfileMenu";

function MainLayout() {
  return (
    <div
      className="relative flex flex-col w-full min-h-screen bg-no-repeat bg-contain bg-fixed gap-20"
      style={{
        backgroundImage: `url('/images/BgMain4x.svg')`,
        backgroundPosition: "top center",
      }}
    >
      <div className="absolute inset-0 bg-black opacity-4 pointer-events-none z-10" />
      <div className="absolute top-4 right-4 z-50">
        <ProfileMenu />
      </div>
      <div className="relative z-20 flex-grow px-6 md:px-12">
        <div className="flex items-center justify-center gap-3 mt-18 font-semibold">
          <img
            src="/images/Logo4x.svg"
            alt="Weather App logo"
            className="w-12 h-12 object-contain"
          />
          <h1 className="text-[28px]">Weather App</h1>
        </div>
        <div className="mt-12">
          <Outlet />
        </div>
      </div>
      <Footer />
    </div>
  );
}

export default MainLayout;
