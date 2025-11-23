export function getFriendlyErrorMessage(status: number): string {
  switch (status) {
    case 400:
      return 'Bad request. Please provide valid task inputs.';
    case 404:
      return 'Task Not found.';
    case 500:
      return 'Something went wrong on the server.';
    default:
      return 'An unexpected error occurred.';
  }
}
