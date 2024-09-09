
import { Dispatch } from "react";
import { Action } from "@reduxjs/toolkit";
import { surveyActions } from "./survey-slice";
import { IAnswer, ISubmitAnswersModel } from "../../components/survey/survey-questions/answers/answer";
import { IHealthAssesment } from "../../components/survey/survey-start/survey-start";



export const fetchQuestionGroups = (fetchExecute : any) => {

    return async (dispatch: Dispatch<Action>) => {
            // dispatch()

            try {
                const response = await fetchExecute(`${process.env.REACT_APP_API_ENDPOINT}/Survey/Query/GetQiestionGroups`, {
                                    method: 'GET',
                                    headers: {
                                        'Content-Type': 'application/json'
                                    },
                                    credentials: 'include'
                                })
                const data = await response.json()
                // console.log(data)
                dispatch(surveyActions.setQuestionGroups(data.Data));
            } catch (error) {
                console.error(error);
            }
        }
};


export const fetchQuestions = (fetchExecute : any) => {
    return( async (dispatch: Dispatch<Action>) =>{
        try {
            const response = await fetchExecute(`${process.env.REACT_APP_API_ENDPOINT}/Survey/Query/GetQuestions`,{
                method: "GET",
                headers: {
                    'Content-Type':'application/json'
                },
                credentials: 'include'
            })
            const data = await response.json()
            dispatch(surveyActions.setQuestions(data.Data));
        } catch (error) {
            console.error(error);
        }
    });
}

export const fetchAnswers = (fetchExecute: any, userId: number = 0, healthAssesmentId: number = 0) => {
    return(async (dispatch: Dispatch<Action>) => {
        try {
            const response = await fetchExecute(`${process.env.REACT_APP_API_ENDPOINT}/Survey/Query/GetAnswers?UserId=${userId}&HealthAssesmentId=${healthAssesmentId}`,{
                method: 'GET',
                headers: {
                    'Content-Type':'application/json'
                },
                credentials: 'include'
            })
            console.log(`${process.env.REACT_APP_API_ENDPOINT}/Survey/Query/GetAnswers?userId${userId}&healthAssesmentId=${healthAssesmentId}`)
            const data = await response.json()
            let result: IAnswer[] = data.Data
            let x: IAnswer[] = result.map((answer:IAnswer) => {
                let z: IAnswer = {...answer, isSavedInDatabase: false};
                return z;
            })
            dispatch(surveyActions.setAnswers(x))
        } catch (error) {
            console.error(error);
        }
    });


    
}

export const CreateaHealthAssesment = (fetchExecute: any, userId: number) => {
    return(async(dispatch: Dispatch<Action>)=>{
        try {
            const response = await fetchExecute(`${process.env.REACT_APP_API_ENDPOINT}/Survey/Command/CreateNewHealthAssesment/${userId}`,{
                method: 'POST',
                headers: {'Content-Type':'application/json'},
                credentials: 'include'
            });

            const data = await response.json();
            const HealthAssesment: IHealthAssesment = data.Data;
            dispatch(surveyActions.setHealthAssesment(HealthAssesment))
        } catch (error) {
            console.error(error);
        }
    });
}

export const GetHealthAssesment = (fetchExecute: any, userId: number, healthAssesmentId: number = 0) => {
    return(async(dispatch: Dispatch<Action>) => {
        try {
            const response = await fetchExecute(`${process.env.REACT_APP_API_ENDPOINT}/Survey/Query/GetHealthAssesment?userId=${userId}&healthAssesmentId=${healthAssesmentId}`,{
                method: 'GET',
                headers: {'Content-Type':'application/json'},
                credentials: 'include'
            });
            const data = await response.json()
            // console.log(data)
            const healthAssesment: IHealthAssesment = data.Data;
            dispatch(surveyActions.setHealthAssesment(healthAssesment))
        } catch (error) {
            console.error(error);
        }
    });
}

export const SubmitAnswersInDatabase = (fetchExecute:any, answers: ISubmitAnswersModel) => {
    return(async (dispatch: Dispatch<Action>)=> {
        try {
            const response = await fetchExecute(`${process.env.REACT_APP_API_ENDPOINT}/Survey/Command/SubmitAnswers`,{
                method: 'POST',
                headers: {'Content-Type':'application/json'},
                credentials: 'include',
                body: JSON.stringify(
                    answers
                )
            });
        } catch (error) {
            console.error(error);
        }
    });
}


