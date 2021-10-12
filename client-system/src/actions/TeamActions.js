import HttpClient from '../Services/HttpClient'
//import axios from 'axios'

// const instance = axios.create()
// instance.CancelToken = axios.CancelToken
// instance.isCancel = axios.isCancel

export const addTeam = (team) => {
    return new Promise((resolve, eject) => {
        HttpClient.post('Team', team).then((response) => {
            resolve(response)
        })
    })
}