const API_BASE_URL = 'http://localhost:5100/api';

export const ENDPOINTS = {
  AUTH: {
    LOGIN: `${API_BASE_URL}/auth/login`,
    REGISTER: `${API_BASE_URL}/auth/register`,
  },
  PROPERTY: {
    COMPLEXES: `${API_BASE_URL}/property/complexes`,
    DEVELOPERS: `${API_BASE_URL}/property/developers`,
  },
  USER: {
    PROFILE: `${API_BASE_URL}/user/profile`,
  },
  FAVORITE: {
    GET: `${API_BASE_URL}/favorite`,
    TOGGLE: (id) => `${API_BASE_URL}/favorite/toggle/${id}`,
    COMPARE: `${API_BASE_URL}/favorite/compare`,
  }
};

export default API_BASE_URL;
