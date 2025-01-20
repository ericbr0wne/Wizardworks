const API_BASE_URL = 'http://localhost:5291/api/squares';

export const squareService = {
  getSquares: async () => {
    try {
      const response = await fetch(API_BASE_URL);
      if (!response.ok) {
        throw new Error('Failed to fetch squares');
      }
      return await response.json();
    } catch (error) {
      console.error('Error fetching squares:', error);
      throw error;
    }
  },

  addSquare: async () => {
    try {
      const response = await fetch(API_BASE_URL, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        }
      });
      if (!response.ok) {
        throw new Error('Failed to add square');
      }
      return await response.json();
    } catch (error) {
      console.error('Error adding square:', error);
      throw error;
    }
  }
};