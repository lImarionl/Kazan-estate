import { ENDPOINTS } from './apiConfig';

const handleResponse = async (response) => {
  const result = await response.json();
  
  if (!result.isSuccess) {
    throw new Error(result.errorMessage || 'Что-то пошло не так');
  }
  
  return result.data; // Возвращаем только T из Result<T>
};

export const apiService = {
  get: async (url, token) => {
    const headers = token ? { 'Authorization': `Bearer ${token}` } : {};
    const response = await fetch(url, { headers });
    return handleResponse(response);
  },

  post: async (url, body, token) => {
    const headers = { 'Content-Type': 'application/json' };
    if (token) headers['Authorization'] = `Bearer ${token}`;
    
    const response = await fetch(url, {
      method: 'POST',
      headers,
      body: JSON.stringify(body),
    });
    return handleResponse(response);
  },

  put: async (url, body, token) => {
    const headers = { 'Content-Type': 'application/json' };
    if (token) headers['Authorization'] = `Bearer ${token}`;

    const response = await fetch(url, {
      method: 'PUT',
      headers,
      body: JSON.stringify(body),
    });
    return handleResponse(response);
  }
};
