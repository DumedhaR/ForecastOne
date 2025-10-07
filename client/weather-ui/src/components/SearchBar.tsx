const SearchBar = () => {
  return (
    <div className="flex items-center justify-center text-[16px]">
      <div className="relative w-full max-w-md">
        <input
          type="text"
          placeholder="Enter a city"
          className="bg-white/12 p-3 pr-36 rounded-lg outline-none w-full"
          disabled
        />
        <button className="absolute bg-[#6c5dd3] right-0 top-0 rounded-lg py-3 px-6 font-semibold shadow-md hover:bg-[#6c5dd3]/90 transition">
          Add City
        </button>
      </div>
    </div>
  );
};

export default SearchBar;
