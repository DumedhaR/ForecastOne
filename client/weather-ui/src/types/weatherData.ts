export interface WeatherData {
  id: number; // City Code
  name: string; // City name
  dt: number; // Data timestamp in UNIX sec
  timezone: number; // Shift in seconds from UTC

  main: {
    temp: number; // Current temperature
    humidity?: number; // Humidity percentage
    pressure?: number; // Atmospheric pressure
    temp_min: number; // Min temperature
    temp_max: number; // Max temperature
  };

  weather: {
    main: string; // Weather main category
    description: string; // Weather description
  }[];

  wind?: {
    speed: number; // Wind speed in (m/s)
    deg: number; // Wind direction in degrees
  };

  visibility?: number; // Visibility in meters

  sys: {
    country: string; // Country code
    sunrise: number; // Sunrise time in UNIX sec
    sunset: number; // Sunset time in UNIX sec
  };
}

export interface WeatherResponse {
  status: string;
  results: number;
  data: {
    cityId: string;
    weatherData: WeatherData;
    fromCache: boolean;
  }[];
  failed?: {
    cityId: string;
    error: string;
  }[];
}

export interface weatherByCityResponse {
  status: string;
  weatherData: WeatherData;
  fromCache: boolean;
}

export interface WeatherRecord {
  id: number;
  cityName: string;
  country: string;
  dateTime: string;
  weatherDesc: string;
  weatherType: string;
  temp: number;
  minTemp: number;
  maxTemp: number;
  pressure: number;
  humidity: number;
  visibility: number;
  windSpeed: number;
  windDeg: number;
  sunriseTime: string;
  sunsetTime: string;
}

export interface WeatherContextType {
  weatherData: WeatherRecord[];
  setWeatherData: (data: WeatherRecord[]) => void;
}
