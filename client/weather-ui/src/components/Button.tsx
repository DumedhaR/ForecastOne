import { useNavigate } from "react-router-dom";

interface ButtonProps {
  label?: string;
  to?: string;
  onClick?: () => void;
  className?: string;
  buttonType?: "button" | "submit" | "reset";
}

const Button = ({
  label = "Click Me",
  to,
  onClick,
  className = "",
  buttonType = "button",
}: ButtonProps) => {
  const navigate = useNavigate();

  const handleClick = () => {
    if (to) navigate(to);
    if (onClick) onClick();
  };

  return (
    <button
      className={`px-4 py-3 rounded-lg shadow-xl text-md md:text-lg transition duration-200 ${className}`}
      onClick={handleClick}
      type={buttonType}
    >
      {label}
    </button>
  );
};

export default Button;
