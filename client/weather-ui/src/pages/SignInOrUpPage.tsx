import GoogleLoginButton from "../components/GoogleSignIn";
import FadeSlider from "../components/FadeSlider";
import SignUp from "../components/LocalSignUp";
import SignIn from "../components/LocalSignIn";

const images = [
  "/images/cover1.jpg",
  "/images/cover2.jpg",
  "/images/cover3.jpg",
  "/images/cover4.jpg",
];
interface SignInOrUpProps {
  isSignUp?: boolean;
  withExternals?: boolean;
}
export default function SignInPage({
  isSignUp = true,
  withExternals = true,
}: SignInOrUpProps) {
  return (
    <div className="w-full h-screen grid grid-cols-1 md:grid-cols-2">
      {/* slider with logo */}
      <div className="relative w-full h-full bg-gray-100">
        <FadeSlider images={images} />

        {/* Overlay */}
        <div className="absolute inset-0 bg-black/40 flex items-center justify-center z-20">
          <div className="flex flex-col items-center justify-center mb-12">
            <img
              src="/images/Logo4x.svg"
              alt="LinkPi Logo"
              className="w-20 h-20 mb-3 object-contain"
            />
            <h2 className="text-2xl font-semibold">Welcome to</h2>
            <h1 className="text-3xl font-semibold">ForecastOne</h1>
          </div>
        </div>
      </div>

      {/* signup form */}
      <div className="flex flex-col justify-center items-center bg-gray-50">
        <div className="w-full max-w-md px-8 py-10 rounded-2xl shadow-lg bg-white text-gray-800">
          <h1 className="text-3xl font-semibold mb-2">
            {isSignUp ? "Sign Up" : "Sign In"}
          </h1>
          <p className="text-gray-500 mb-6">to continue</p>

          {isSignUp ? <SignUp /> : <SignIn />}

          {withExternals ? (
            <>
              <div className="flex items-center gap-2 my-6 mx-2.5">
                <hr className="flex-grow border-gray-300" />
                <span className="text-gray-400">or</span>
                <hr className="flex-grow border-gray-300" />
              </div>
              <GoogleLoginButton />
            </>
          ) : (
            <></>
          )}
        </div>
      </div>
    </div>
  );
}
