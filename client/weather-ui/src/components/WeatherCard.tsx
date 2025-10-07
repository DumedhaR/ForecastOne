import cardBgImg from "../assets/images/BgCard4x.svg";
import arrowIcon from "../assets/icons/Arrow4x.svg";
import { FiX } from "react-icons/fi";
import { type WeatherRecord } from "../types/weatherData";
import { Link } from "react-router-dom";

const WeatherCard = (cardData: WeatherRecord) => {
  return (
    <div className="flex flex-col rounded-lg overflow-hidden shadow-md relative text-[15px] md:text-[16px]">
      <button className="absolute top-2 right-2 p-1" aria-label="Close">
        <FiX className="w-5 h-5 text-white" />
      </button>
      <Link
        to={`/weather/${cardData.id}`}
        className={`flex p-6 sm:p-8 lg:p-10 w-full gap-x-3 md:gap-x-5 bg-no-repeat bg-bottom bg-contain overflow-hidden`}
        style={{
          backgroundImage: `url(${cardBgImg})`,
          backgroundColor: `var(--bg-${cardData.weatherType})`,
        }}
      >
        <div className="flex flex-col flex-1 justify-between gap-y-5">
          <div>
            <h3 className="text-[24px] md:text-[28px] font-semibold capitalize">{`${cardData.cityName}, ${cardData.country}`}</h3>
            <p className="mt-1">{cardData.dateTime}</p>
          </div>
          <div className="flex flex-col md:flex-row items-center justify-center gap-1 sm:gap-2">
            <img
              src={`/icons/${cardData.weatherType}.svg`}
              alt={`${cardData.weatherDesc}`}
              className="w-9 h-9 object-contain text-[8px]"
            />
            <p className="md:text-[18px] capitalize font-semibold">
              {cardData.weatherDesc}
            </p>
          </div>
        </div>
        <div className="flex flex-col flex-1 items-center text-left gap-6 font-semibold">
          <p className="text-[48px] sm:text-[56px]">{`${cardData.temp}°c`}</p>
          <div>
            <p>{`Temp Min: ${cardData.minTemp}°c`}</p>
            <p>{`Temp Max: ${cardData.maxTemp}°c`}</p>
          </div>
        </div>
      </Link>
      <div className="flex bg-[#383b47] justify-between p-6 sm:p-8 gap-x-3 lg:gap-x-5 items-center overflow-hidden">
        <ul className="space-y-1 text-left">
          <li>
            <span className="font-semibold">Pressure:</span>
            {` ${cardData.pressure}hPa`}
          </li>
          <li>
            <span className="font-semibold">Humidity:</span>
            {` ${cardData.humidity}%`}
          </li>
          <li>
            <span className="font-semibold">Visibility:</span>
            {` ${cardData.visibility}km`}
          </li>
        </ul>
        <div className="flex flex-col items-center gap-2 border-x-2 border-gray-500/50 h-full px-3 sm:px-5">
          <img
            src={arrowIcon}
            alt="Wind Direction"
            className="w-7 h-7 object-contain"
          />
          <p>{`${cardData.windSpeed}m/s ${cardData.windDeg} Degree`}</p>
        </div>
        <ul className="space-y-1 text-right h-full">
          <li>
            <span className="font-semibold">Sunrise:</span>
            {` ${cardData.sunriseTime}`}
          </li>
          <li>
            <span className="font-semibold">Sunset:</span>
            {` ${cardData.sunsetTime}`}
          </li>
        </ul>
      </div>
    </div>
  );
};

export default WeatherCard;
