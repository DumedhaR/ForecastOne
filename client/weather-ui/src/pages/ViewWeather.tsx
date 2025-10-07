// import { useWeather } from "../hooks/useWeather";
import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { type WeatherRecord } from "../types/weatherData";
// import { getWeatherByCity } from "../services/weatherService";
import LoadingSping from "../components/LoadingSpin";
import arrowIcon from "../assets/icons/Arrow4x.svg";
import { FiArrowLeft } from "react-icons/fi";
import { Link } from "react-router-dom";

const ViewWeather = () => {
  const { cityId } = useParams<{ cityId: string }>();
  // const { weatherData } = useWeather();
  const [weatherView, setWeatherView] = useState<WeatherRecord | null>(null);

  // useEffect(() => {
  //   if (!cityId) return;

  //   const fetchWeather = async () => {
  //     const record = weatherData.find((r) => r.id === Number(cityId));
  //     if (record) {
  //       setWeatherView(record);
  //     } else {
  //       try {
  //         const accessToken = await getAccessTokenSilently({
  //           authorizationParams: {
  //             audience: AUTH0_AUDIENCE,
  //           },
  //         });
  //         const data = await getWeatherByCity(accessToken, cityId);
  //         setWeatherView(data);
  //       } catch (err) {
  //         console.error("Failed to load weather forecast data:", err);
  //       }
  //     }
  //   };

  //   fetchWeather();
  // }, [cityId, weatherData, getAccessTokenSilently]);

  if (!weatherView) {
    return <LoadingSping />;
  }

  return (
    <div className="flex flex-col rounded-md overflow-hidden shadow-md relative max-w-5xl mx-auto text-[15px] md:text-[16px]">
      <Link
        to={"/weather"}
        className="absolute top-4 left-4 p-1"
        aria-label="Close"
      >
        <FiArrowLeft className="w-5 h-5 text-white" />
      </Link>
      <div
        className={`flex flex-col p-6 md:p-12 w-full gap-5 md:gap-8 bg-no-repeat bg-bottom overflow-hidden`}
        style={{
          backgroundColor: `var(--bg-${weatherView.weatherType})`,
        }}
      >
        <div className="flex flex-col text-center justify-center">
          <div>
            <h3 className="text-xl md:text-[28px] font-semibold capitalize">{`${weatherView.cityName}, ${weatherView.country}`}</h3>
            <p className="mt-1 text-sm md:text-base">{weatherView.dateTime}</p>
          </div>
        </div>
        <div className="flex justify-center items-center text-left font-semibold mb-2">
          <div className="flex flex-col items-center justify-center gap-4 pr-8 md:pr-16 border-r-2  border-white/50 ">
            <img
              src={`/icons/${weatherView.weatherType}.svg`}
              alt={`${weatherView.weatherDesc}`}
              className="w-14 h-14 object-contain text-[8px]"
            />
            <p className="capitalize">{weatherView.weatherDesc}</p>
          </div>
          <div className="pl-8 md:pl-16 ">
            <p className="text-[48px] md:text-[56px] pb-3">{`${weatherView.temp}°c`}</p>
            <p>{`Temp Min: ${weatherView.minTemp}°c`}</p>
            <p>{`Temp Max: ${weatherView.maxTemp}°c`}</p>
          </div>
        </div>
      </div>
      <div className="flex bg-[#383b47] p-6 md:p-12 items-center justify-between md:justify-center gap-x-3 md:gap-x-20 overflow-hidden">
        <ul className="space-y-1 text-left">
          <li>
            <span className="font-semibold">Pressure:</span>
            {` ${weatherView.pressure}hPa`}
          </li>
          <li>
            <span className="font-semibold">Humidity:</span>
            {` ${weatherView.humidity}%`}
          </li>
          <li>
            <span className="font-semibold">Visibility:</span>
            {` ${weatherView.visibility}km`}
          </li>
        </ul>
        <div className="flex flex-col items-center gap-2 border-x-2 border-gray-500/50 h-full px-3 md:px-20">
          <img
            src={arrowIcon}
            alt="Wind Direction"
            className="w-7 h-7 object-contain"
          />
          <p>{`${weatherView.windSpeed}m/s ${weatherView.windDeg} Degree`}</p>
        </div>
        <ul className="space-y-1 text-right h-full">
          <li>
            <span className="font-semibold">Sunrise:</span>
            {` ${weatherView.sunriseTime}`}
          </li>
          <li>
            <span className="font-semibold">Sunset:</span>
            {` ${weatherView.sunsetTime}`}
          </li>
        </ul>
      </div>
    </div>
  );
};
export default ViewWeather;
