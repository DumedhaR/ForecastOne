const monthNames = [
  "Jan",
  "Feb",
  "Mar",
  "Apr",
  "May",
  "Jun",
  "Jul",
  "Aug",
  "Sep",
  "Oct",
  "Nov",
  "Dec",
];

export const formatUnixDateTime = (
  unixTime: number,
  timezone: number,
  formatType: "time" | "date" | "datetime" = "datetime"
): string => {
  const date = new Date((unixTime + timezone) * 1000);

  const hours = date.getUTCHours();
  const minutes = date.getUTCMinutes();
  const padedMins = minutes.toString().padStart(2, "0"); // 6 => 06
  const ampm = hours >= 12 ? "pm" : "am";
  const hour12 = hours % 12 || 12; // 16 => 4

  const time = `${hour12}:${padedMins}${ampm}`;
  const day = date.getUTCDate();
  const month = monthNames[date.getUTCMonth()];

  switch (formatType) {
    case "date":
      return `${month} ${day}`;
    case "time":
      return time;
    case "datetime":
      return `${time}, ${month} ${day}`;
    default:
      return `${time}, ${month} ${day}`; // fallback to datetime to handle incorrect types.
  }
};
