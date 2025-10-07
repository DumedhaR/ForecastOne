import { useState, createContext, type ReactNode } from "react";
import { type WeatherRecord } from "../types/weatherData";
import { type WeatherContextType } from "../types/weatherData";

const WeatherContext = createContext<WeatherContextType | undefined>(undefined);

interface UserProviderProps {
  children: ReactNode;
}

export const WeatherProvider = ({ children }: UserProviderProps) => {
  const [weatherData, setWeatherData] = useState<WeatherRecord[]>([]);

  return (
    <WeatherContext.Provider value={{ weatherData, setWeatherData }}>
      {children}
    </WeatherContext.Provider>
  );
};
export default WeatherContext;
