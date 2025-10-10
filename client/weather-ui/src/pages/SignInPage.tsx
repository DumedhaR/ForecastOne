import GoogleLoginButton from "../components/GoogleSignIn";
import FadeSlider from "../components/FadeSlider";

const images = [
  "/images/cover1.jpg",
  "/images/cover2.jpg",
  "/images/cover3.jpg",
  "/images/cover4.jpg",
];

export default function SignInPage() {
  return (
    <div className="h-screen grid grid-cols-2">
      <div className="bg-gray-100 h-screen">
        <FadeSlider images={images} />
      </div>

      <div className="flex flex-col justify-center items-center h-screen">
        <div className="w-full max-w-sm text-center px-6">
          <img
            src="/images/Logo4x.svg"
            alt="LinkPi Logo"
            className="mx-auto mb-2 w-20 h-20 object-contain"
          />
          <h1 className="text-3xl font-semibold mb-6">
            Welcome to <br />
            ForecastOne
          </h1>
          <p className="text-gray-400 mb-4">Login or Signup to continue</p>
          <GoogleLoginButton />
        </div>
      </div>
    </div>
  );
}
