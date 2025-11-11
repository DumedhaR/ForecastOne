import { useState, type ChangeEvent, type FormEvent } from "react";
import Button from "./Button";

const SignUp = () => {
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    confirmPassword: "",
  });

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (formData.password !== formData.confirmPassword) {
      alert("Passwords do not match");
      return;
    }
    console.log("Signup data:", formData);
  };

  return (
    <form onSubmit={handleSubmit} className="w-full max-w-md space-y-4">
      {/** Form fields with placeholders instead of labels */}
      {["firstName", "lastName", "email", "password", "confirmPassword"].map(
        (field) => (
          <div key={field}>
            <input
              id={field}
              name={field}
              type={
                field.toLowerCase().includes("password")
                  ? "password"
                  : field === "email"
                  ? "email"
                  : "text"
              }
              value={formData[field as keyof typeof formData]}
              onChange={handleChange}
              required
              minLength={
                field.toLowerCase().includes("password") ? 8 : undefined
              }
              maxLength={field.includes("Name") ? 20 : undefined}
              placeholder={
                field === "confirmPassword"
                  ? "Confirm Password"
                  : field.charAt(0).toUpperCase() + field.slice(1)
              }
              className="px-4 py-2.5 rounded-lg border border-gray-300 focus:outline-none focus:border-gray-500 disabled:bg-gray-100 disabled:cursor-not-allowed w-full bg-white"
            />
          </div>
        )
      )}

      <Button
        buttonType="submit"
        label="Sign Up"
        className="w-full bg-primary hover:bg-primary/90 text-white font-semibold rounded-lg transition-colors mt-2"
      />
    </form>
  );
};

export default SignUp;
