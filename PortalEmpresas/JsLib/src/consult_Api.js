import axios from 'axios';

async function getJsonFromUrl(url, headers = {}) {
  try {
    const response = await axios.get(url, {
      headers: headers
    });
    return response.data;
  } catch (error) {
    console.error('Error al hacer GET:', error.message);
    throw error;
  }
}

async function getJsonFromUrlOtherTimeOut(url, headers = {}) {
  try {
    const response = await axios.get(url, {
      headers,
      timeout: 1800000 // 30 minutos
    });
    return response.data;
  } catch (error) {
    console.error('Error en GET:', error.message);
    throw error;
  }
}

async function postJsonToUrl(url, data, headers = {}) {
  try {
    const response = await axios.post(url, data, {
      headers: headers
    });
    return response.data;
  } catch (error) {
    console.error('Error al hacer POST:', error.message);
    throw error;
  }
}
async function postJsonToUrlOtherTimeOut(url, data, headers = {}) {
  try {
    const response = await axios.post(url, data, {
      headers: headers,
      timeout: 1800000 // 30 minutos

    });
    return response.data;
  } catch (error) {
    console.error('Error al hacer POST:', error.message);
    throw error;
  }
}

export {
  getJsonFromUrl,
  getJsonFromUrlOtherTimeOut,
  postJsonToUrl,
  postJsonToUrlOtherTimeOut
};