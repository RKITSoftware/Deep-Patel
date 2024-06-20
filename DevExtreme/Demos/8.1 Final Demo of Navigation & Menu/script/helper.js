function generateRandomId() {
  const timestamp = new Date().getTime(); // Get current timestamp in milliseconds
  const randomNumber = Math.floor(Math.random() * 1000000); // Generate random number between 0 and 999999
  return `${timestamp}-${randomNumber}`;
}

function notify(toast, message, type) {
  toast.option({
    message: message,
    type: type,
  });

  toast.show();
}

export { generateRandomId, notify };
