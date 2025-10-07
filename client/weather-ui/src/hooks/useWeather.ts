import { useContext } from "react";
import WeatherContext from "../contexts/WeatherContext";
import { type WeatherContextType } from "../types/weatherData";

export const useWeather = (): WeatherContextType => {
  const context = useContext(WeatherContext);
  if (!context) {
    throw new Error(
      "WeatherContext not found. Your component should be wrapped inside <WeatherProvider>!"
    );
  }
  return context;
};
