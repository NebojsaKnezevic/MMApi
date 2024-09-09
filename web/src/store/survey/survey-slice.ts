import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { IQuestionGroup } from "../../components/survey/survey-question-groups";
import { IAnswer } from "../../components/survey/survey-questions/answers/answer";
import { IHealthAssesment } from "../../components/survey/survey-start/survey-start";
import { IQuestion } from "../../components/survey/survey-questions/survey-questions";
// import { fetchQuestionGroups } from "./survey-actions";

const initialState: ISurveyState = {
    QuestionGroups: [],
    Questions: [],
    Answers: [],
    HealthAssesment: {
        id: null,
        userId: null,
        startedOn: null, 
        completedOn: null
    }
}

type ISurveyState = {
    QuestionGroups: IQuestionGroup[],
    Questions: IQuestion[],
    Answers: IAnswer[],
    HealthAssesment: IHealthAssesment
}

const surveySlice = createSlice({
    name: "survey",
    initialState,
    reducers: {
        setQuestionGroups: (state, action: PayloadAction<IQuestionGroup[]>) => {
            state.QuestionGroups = action.payload;
        },
        setQuestions: (state, action: PayloadAction<IQuestion[]>) => {
            state.Questions = action.payload;
        },
        setAnswers: (state, action: PayloadAction<IAnswer[]>) => {
            state.Answers = action.payload
        },
        setHealthAssesment: (state, action: PayloadAction<IHealthAssesment>) => {
            state.HealthAssesment = action.payload
        }
    }
});

export const surveyActions = surveySlice.actions;
export default surveySlice;