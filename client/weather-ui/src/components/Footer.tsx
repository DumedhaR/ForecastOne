const Footer = () => {
  return (
    <footer
      className="relative z-20 w-full text-white py-9"
      style={{ backgroundColor: "#30333d", color: "#f1f1f1" }}
    >
      <div className="px-6 sm:px-12 lg:px-18 mx-auto capitalize">
        <div className="text-center text-sm opacity-60">
          &copy; {new Date().getFullYear()} Fidenz Technologies
        </div>
      </div>
    </footer>
  );
};

export default Footer;
