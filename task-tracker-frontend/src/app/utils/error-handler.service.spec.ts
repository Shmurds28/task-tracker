import { getFriendlyErrorMessage } from 'src/app/utils/error-handler.service';

describe('getFriendlyErrorMessage', () => {
  it('should return correct message for 400', () => {
    expect(getFriendlyErrorMessage(400)).toBe('Bad request. Please provide valid task inputs.');
  });

  it('should return correct message for 404', () => {
    expect(getFriendlyErrorMessage(404)).toBe('Task Not found.');
  });

  it('should return correct message for 500', () => {
    expect(getFriendlyErrorMessage(500)).toBe('Something went wrong on the server.');
  });

  it('should return default message for unknown status', () => {
    expect(getFriendlyErrorMessage(999)).toBe('An unexpected error occurred.');
  });
});
