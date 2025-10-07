import { useEffect, useState } from "react";
import WeatherCard from "../components/WeatherCard";
import { useWeather } from "../hooks/useWeather";
import { getAllWeatherData } from "../services/weatherService";
import { useAuth0 } from "@auth0/auth0-react";
import LoadingSping from "../components/LoadingSpin";
import SearchBar from "../components/SearchBar";
import { AUTH0_AUDIENCE } from "../config";

const Dashboard = () => {
  const { weatherData, setWeatherData } = useWeather();
  const { getAccessTokenSilently } = useAuth0();
  const [isLoading, setLoading] = useState(true);

  useEffect(() => {
    const fetchAllWeather = async () => {
      try {
        const accessToken = await getAccessTokenSilently({
          authorizationParams: {
            audience: AUTH0_AUDIENCE,
          },
        });

        const weatherData = await getAllWeatherData(accessToken);
        setWeatherData(weatherData);
        setLoading(false);
      } catch (err) {
        console.error("Failed to load weather forecast data:", err);
      }
    };
    fetchAllWeather();
  }, [setWeatherData, getAccessTokenSilently]);

  return (
    <div className="flex flex-col gap-16">
      <SearchBar />
      {isLoading ? (
        <LoadingSping />
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 max-w-7xl mx-auto gap-6 sm:gap-8 md:gap-10">
          {weatherData.map((city) => {
            console.log(city);
            return (
              <div className="" key={city.id}>
                <WeatherCard {...city} />
              </div>
            );
          })}
        </div>
      )}
    </div>
  );
};

export default Dashboard;
