import axios from 'axios';

//const urlBase = 'https://localhost:44351/api/'

axios.defaults.baseURL = 'https://localhost:5001/api/'

//const token = window.localStorage.getItem('token');
const token = '';
axios.interceptors.request.use((config) => {
    if(token){
        config.headers.Autorization = `Bearer ${token}`
        return config;
    }
}, error => {
    return Promise.reject(error)
})

const generitRequests = {
    get : (url) => axios.get(url),
    post : (url, body) => axios.post(url, body),
    put : (url, body) => axios.put(url, body),
    delete : (url) => axios.delete(url),
}

export default generitRequests;