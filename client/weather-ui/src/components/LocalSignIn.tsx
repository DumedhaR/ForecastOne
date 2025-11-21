import { useState, type ChangeEvent } from "react";
import { Link } from "react-router-dom";
import Button from "./Button";

const fields = ["Email", "Password"].map((f) => f.toLowerCase());

const LocalSignIn = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    if (name === "email") {
      setEmail(value);
    }
    if (name === "password") {
      setPassword(value);
    }
  };

  return (
    <div className="w-full max-w-md space-y-4">
      {fields.map((field) => (
        <div key={field}>
          <input
            onChange={handleChange}
            value={field === "password" ? password : email}
            id={field}
            name={field}
            type={field === "password" ? "password" : "email"}
            required
            placeholder={field.charAt(0).toUpperCase() + field.slice(1)}
            className="px-4 py-2.5 rounded-lg border border-gray-300 focus:outline-none focus:border-gray-500 w-full bg-white"
          />
        </div>
      ))}

      <Button
        label="Sign In"
        className="w-full bg-primary hover:bg-primary/90 text-white font-semibold rounded-lg transition-colors mt-2"
      />

      <p className="text-center text-sm text-gray-500">
        Don't have an account?{" "}
        <Link to="/SignUp" className="text-primary font-medium">
          Sign up
        </Link>
      </p>
    </div>
  );
};

export default LocalSignIn;
