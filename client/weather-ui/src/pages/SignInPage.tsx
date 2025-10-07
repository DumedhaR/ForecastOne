import GoogleLoginButton from "../components/GoogleSignIn";

export default function SignInPage() {
  return (
    <div className="h-screen grid grid-cols-2">
      <div className="bg-gray-100 h-screen">
        <img
          src="/images/cover1.jpg"
          alt="Visual"
          className="w-full h-full object-cover"
        />
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
