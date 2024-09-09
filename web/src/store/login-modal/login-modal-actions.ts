
import { Dispatch } from 'react'
import { Action } from 'redux' 
import {  IModal, setUserData } from './login-modal';

// import { Action } from "@reduxjs/toolkit";


export const RegisterUser = (fetchExecute: any, email: string, pwd: string) => {
    return(async (dispatch: Dispatch<Action>)=>{
        try {
            const response = await fetchExecute(`${process.env.REACT_APP_API_ENDPOINT}/Survey/Command/RegisterUser`,{
                method: 'POST',
                headers: {'Content-Type':'application/json'},
                body: JSON.stringify({
                    Email: email,
                    Pwd: pwd
                })
            })
            const data = await response.json();
            // console.log(data)
        } catch (error) {
            console.error('RegisterUser',error)
        }
    });
}

export const LoginUser = (fetchExecute: any, email: string, pwd: string) => {
    return(async (dispatch: Dispatch<Action>) => {
        try {
            const response = await fetchExecute(`${process.env.REACT_APP_API_ENDPOINT}/Survey/Command/LoginUser`,
                {
                    method: 'POST',
                    headers: {'Content-Type':'application/json'},
                    body: JSON.stringify({
                        Email: email, Pwd: pwd
                    }),
                    credentials: 'include'
                }
                
            )
            const data = await response.json();
            // console.log(data)
            const res: IModal = {IsAuthenticated: true, userData:data.Data }
            dispatch(setUserData(res))
        } catch (error) {
            console.log("LoginUser",error)
            dispatch(setUserData({IsAuthenticated:false, userData:{}}))
        }
    });

    
}

export const CookieLoginUser = (fetchExecute: any) => {
    return(async(dispatch: Dispatch<Action>)=>{
        try {
            const response = await fetchExecute(`${process.env.REACT_APP_API_ENDPOINT}/Survey/Command/CookieLoginUser`,{
                method: 'POST',
                headers: {'Content-Type':'application/json'},
                credentials: 'include'
            });
            const data = await response.json();
            const res: IModal = {IsAuthenticated: true, userData: data.Data}
            dispatch(setUserData(res));
            // console.log("CookieLoginUser",res)
        } catch (error) {
            console.error("CookieLoginUser",error)
            dispatch(setUserData({IsAuthenticated: false, userData: {}}));
        }
    });
}