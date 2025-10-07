import apiClient from "./apiClient";
import {
  type WeatherResponse,
  type weatherByCityResponse,
  type WeatherData,
  type WeatherRecord,
} from "../types/weatherData";
import { formatUnixDateTime } from "../utils/dateUtils";
import { roundToDecimals } from "../utils/mathUtils";

const transformData = (rawData: WeatherData): WeatherRecord => {
  const transformedData: WeatherRecord = {
    id: rawData.id,
    cityName: rawData.name,
    country: rawData.sys.country,
    dateTime: formatUnixDateTime(rawData.dt, rawData.timezone, "datetime"),
    weatherDesc: rawData.weather[0].description,
    weatherType: rawData.weather[0].main.toLocaleLowerCase(),
    temp: roundToDecimals(rawData.main.temp, 0),
    minTemp: roundToDecimals(rawData.main.temp_min, 0),
    maxTemp: roundToDecimals(rawData.main.temp_max, 0),
    pressure: rawData.main.pressure ?? 1018,
    humidity: rawData.main.humidity ?? 78,
    visibility: (rawData.visibility ?? 8000) / 1000,
    windSpeed: roundToDecimals(rawData.wind?.speed ?? 4, 1),
    windDeg: rawData.wind?.deg ?? 120,
    sunriseTime: formatUnixDateTime(
      rawData.sys.sunrise,
      rawData.timezone,
      "time"
    ),
    sunsetTime: formatUnixDateTime(
      rawData.sys.sunset,
      rawData.timezone,
      "time"
    ),
  };
  return transformedData;
};

export const getAllWeatherData = async (accessToken: string) => {
  const response = await apiClient.get<WeatherResponse>("/api/weather", {
    headers: {
      Authorization: `Bearer ${accessToken}`,
    },
  });

  if (response.data.status !== "success") {
    throw new Error("API error or unexpected status");
  }
  if (response.data.failed && response.data.failed.length > 0) {
    console.warn("Some weather data failed to fetch:", response.data.failed);
  }
  const weatherRecords: WeatherRecord[] = response.data.data.map((e) =>
    transformData(e.weatherData)
  );
  console.log(weatherRecords);
  return weatherRecords;
};

export const getWeatherByCity = async (accessToken: string, cityId: string) => {
  const response = await apiClient.get<weatherByCityResponse>(
    `/api/weather/${cityId}`,
    {
      headers: {
        Authorization: `Bearer ${accessToken}`,
      },
    }
  );

  if (response.data.status !== "success") {
    throw new Error("API error or unexpected status");
  }

  const weatherRecord: WeatherRecord = transformData(response.data.weatherData);

  console.log(weatherRecord);
  return weatherRecord;
};
