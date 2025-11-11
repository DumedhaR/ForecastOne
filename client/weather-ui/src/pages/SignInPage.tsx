import GoogleLoginButton from "../components/GoogleSignIn";
import FadeSlider from "../components/FadeSlider";
import SignUp from "../components/LocalSignUp";

const images = [
  "/images/cover1.jpg",
  "/images/cover2.jpg",
  "/images/cover3.jpg",
  "/images/cover4.jpg",
];

export default function SignInPage() {
  return (
    <div className="w-full h-screen grid grid-cols-2">
      <div className="bg-gray-100 w-full h-full">
        <FadeSlider images={images} />
      </div>

      <div className="flex flex-col justify-center items-center">
        <div>
          <img
            src="/images/Logo4x.svg"
            alt="LinkPi Logo"
            className="mx-auto mb-2 w-15 h-15 object-contain"
          />
          <h1 className="text-2xl font-semibold">
            Welcome to <br />
            ForecastOne
          </h1>
        </div>
        <div className="w-full max-w-md text-center px-6">
          <div className="flex flex-col items-center justify-center p-6 text-gray-800">
            <p className="text-gray-400 mb-4">Signup to continue</p>
            <SignUp />
            <p className="text-gray-400 my-4">or</p>
            <GoogleLoginButton />
          </div>
        </div>
      </div>
    </div>
  );
}
