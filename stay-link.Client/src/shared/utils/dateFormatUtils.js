export const formatDate = (dateString) => {
  const date = new Date(dateString);

  const options = {
    year: 'numeric',
    month: 'long',
    day: 'numeric'
  };

  return date.toLocaleDateString('en-US', options);
};

export const formatDateTime = (dateString) => {
  const date = new Date(dateString);
  const timeOptions = {
    hour: '2-digit',
    minute: '2-digit',
    hour12: false
  };
  const formattedDate = formatDate(dateString);
  const formattedTime = date.toLocaleTimeString('en-US', timeOptions);
  return `${formattedDate} ${formattedTime}`;
};

export const calculateDateDifference = (startDate, endDate) => {
  const start = new Date(startDate);
  const end = new Date(endDate);

  const startDateOnly = new Date(startDate.split('T')[0]);
  const endDateOnly = new Date(endDate.split('T')[0]);

  if (startDateOnly.toISOString() === endDateOnly.toISOString()) {
    const diffInMilliseconds = end.getTime() - start.getTime();
    const diffInHours = Math.ceil(
      diffInMilliseconds / (1000 * 60 * 60)
    );
    return formatHours(diffInHours);
  } else {
    const diffInMilliseconds =
      endDateOnly.getTime() - startDateOnly.getTime();
    const diffInDays = Math.ceil(
      diffInMilliseconds / (1000 * 60 * 60 * 24) + 1
    );
    return `${diffInDays} days`;
  }
};

const formatHours = (hours)=> {
  return hours === 1 ? `${hours} hour` : `${hours} hours`;
};

export const extractTime = (dateTime) => {
  const date = new Date(dateTime);
  const hours = date.getHours().toString().padStart(2, '0');
  const minutes = date.getMinutes().toString().padStart(2, '0');
  return `${hours}:${minutes}`;
};
