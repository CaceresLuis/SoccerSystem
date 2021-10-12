import { get, post } from '../Services/HttpClient';

export const getUser = () => {
    return new Promise ((resolve, eject) => {
       get('/Team/1').then( response => {
           resolve(response)
       })
    });
};

export const AddUser = (user) => {
    return new Promise ((resolve, eject) => {
       post('/Account/', user).then( response => {
           resolve(response)
       })
    });
};