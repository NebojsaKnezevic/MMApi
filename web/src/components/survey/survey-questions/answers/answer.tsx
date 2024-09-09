// import { useDispatch, useSelector } from "react-redux"
// import { RootState } from "../../../../store/store"
// import { useEffect, useState } from "react"
// import { FormControl, FormControlLabel, Radio, RadioGroup, Typography, Grid, Box } from '@mui/material';
// import { IQuestion } from "../survey-questions";
// import surveySlice from "../../../../store/survey/survey-slice";
// import { IHealthAssesment } from "../../survey-start/survey-start";
// import { SubmitAnswersInDatabase } from "../../../../store/survey/survey-actions";
// import { CheckBox } from "@mui/icons-material";

// export interface IAnswerSelectionType {
//     answerId: number,
//     isSelected: boolean
// }

// export interface ISubmitAnswersModel {
//     questionId: number,
//     healthAssesmentId: number,
//     answers: IAnswerSelectionType[]
// }

// export interface IAnswer {
//     id: number,
//     questionId: number,
//     orderNo: number,
//     text: string,
//     isActive: boolean,
//     isSelected: boolean,
//     isSavedInDatabase: boolean
// }

// export interface IAnswerProps {
//     question: IQuestion
// }

// export const Answer: React.FunctionComponent<IAnswerProps> = (props) => {
//     const { question } = props;
//     const answers: IAnswer[] = useSelector((state: RootState) =>
//         state.survey.Answers
//     );
//     const healthAssesment: IHealthAssesment = useSelector((state: RootState) => state.survey.HealthAssesment);
//     const dispatch: any = useDispatch();

//     const filteredAnswers = answers.filter(item => item.questionId === question.id)
//     // console.log(filteredAnswers.filter(x => x.isSelected))
//     // const filteredAnswers1 = answers.filter(item => item.questionId === question.id)
//     // const question: IQuestions | undefined = useSelector((state: RootState)=> state.survey.Questions.find(q => q.id === questionId))

//     const [selectedAnswerId, setSelectedAnswerId] = useState<number | null>(null);
//     const [selectedAnswersIds, setSelectedAnswersIds] = useState<Set<number>>(new Set());

//     const handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
//         const answerId = Number(event.target.value);
//         // console.log(answerId)

//     setSelectedAnswersIds(prevSelected => {
//         const newSelected = new Set(prevSelected); 
//         // console.log(selectedAnswersIds)
//         // event.target.checked = false
//         console.log('DELETE ELEMENT')
//         if (newSelected.has(answerId)) {
//             console.log('DELETE ELEMENT')
//             newSelected.delete(answerId); 
            
//         } else {
//             newSelected.add(answerId); 
//         }

//         return newSelected;
//     });

//     };
    

//     const handleRadioChange = (event: React.ChangeEvent<HTMLInputElement>) => {
//         setSelectedAnswerId(Number(event.target.value));
//     };

//     const handleMultipleSelectChange = (): void => {
//         if(selectedAnswersIds.size > 0){
//             const updatedAnswers = filteredAnswers.map(answer => {
//                 return (selectedAnswersIds.has(answer.id)
//                     ? { ...answer, isSelected: true }
//                     : { ...answer, isSelected: false });
//             });
//             const databasePackage: ISubmitAnswersModel = {
//                 questionId: question.id!,
//                 healthAssesmentId: healthAssesment.id!,
//                 answers: updatedAnswers.map(answer => ({
//                     answerId: answer.id,
//                     isSelected: answer.isSelected
//                 }))
//             };
//             console.log('updatedAnswers',updatedAnswers) 

//             dispatch(surveySlice.actions.setAnswers(
//                 answers.map(answer => answer.questionId === question.id ?
//                     updatedAnswers.find(a => a.id === answer.id) || answer
//                     : answer
//                 )
//             ))
//         }

       
//     }

//     useEffect(() => {
//         handleMultipleSelectChange()
//         console.log(answers)
//     }, [selectedAnswersIds])

//     const handleSingleSelectChange = (): void => {
//         if (selectedAnswerId !== null) {
//             const updatedAnswers = filteredAnswers.map(answer => {
//                 return (answer.id === selectedAnswerId
//                     ? { ...answer, isSelected: true }
//                     : { ...answer, isSelected: false });
//             });

//             const databasePackage: ISubmitAnswersModel = {
//                 questionId: question.id!,
//                 healthAssesmentId: healthAssesment.id!,
//                 answers: updatedAnswers.map(answer => ({
//                     answerId: answer.id,
//                     isSelected: answer.isSelected
//                 }))
//             };

//             dispatch(surveySlice.actions.setAnswers(
//                 answers.map(answer =>
//                     answer.questionId === question.id
//                         ? updatedAnswers.find(a => a.id === answer.id) || answer
//                         : answer
//                 )
//             ));

//             dispatch(SubmitAnswersInDatabase(fetch, databasePackage))
//         }

//     }

//     useEffect(() => {
//         handleSingleSelectChange()
//     }, [selectedAnswerId])
    

//     if (question.isMultipleSelect) {
//         return (
//             <Box sx={{
//                 backgroundColor: 'white', p: 1, border: '1px solid lightgrey', borderRadius: 2, width: '100%',
//                 display: 'flex', justifyContent: 'end', alignItems: 'center'
//             }}>
//                 <FormControl component="fieldset" >
//                     <Grid container spacing={0} >
//                         {filteredAnswers.map((answer: IAnswer) => (
//                             <Grid item xs={12} sm={12} md={6} key={answer.id} >
//                                 <FormControlLabel
//                                     key={answer.id}
//                                     value={answer.id}
                                    
//                                     control={<CheckBox color="success" 
                                  
                                        
//                                         />}
//                                     label={<Typography variant="body2">{answer.text}</Typography>}
//                                 />
//                             </Grid>
//                         ))}
//                     </Grid>
//                 </FormControl>
//             </Box>
//         );
//     }

//     return (
//         <Box sx={{
//             backgroundColor: 'white', p: 1, border: '1px solid lightgrey', borderRadius: 2, width: '100%',
//             display: 'flex', justifyContent: 'start', alignItems: 'center'
//         }}>
//             <FormControl component="fieldset" >
//                 <RadioGroup
//                     // value={filteredAnswers}
//                     onChange={handleRadioChange}
//                     aria-label="answers"
//                     name="answers"
//                 >
//                     <Grid container spacing={0}>
//                         {filteredAnswers.map((answer: IAnswer) => (
//                             <Grid item xs={12} sm={12} md={6} key={answer.id} >
//                                 <FormControlLabel
//                                     key={answer.id}
//                                     value={answer.id}
//                                     checked={answer.isSelected}
//                                     control={<Radio color="success" />}
//                                     label={<Typography variant="body2">{answer.text}</Typography>}
//                                 />
//                             </Grid>
//                         ))}
//                     </Grid>
//                 </RadioGroup>
//             </FormControl>
//         </Box>
//     );
// };

import { useDispatch, useSelector } from "react-redux";
import { RootState } from "../../../../store/store";
import { useEffect, useState } from "react";
import { IQuestion } from "../survey-questions";
import surveySlice from "../../../../store/survey/survey-slice";
import { IHealthAssesment } from "../../survey-start/survey-start";
import { SubmitAnswersInDatabase } from "../../../../store/survey/survey-actions";
import { Form, Row, Col } from 'react-bootstrap';
import '../answers/answer.css'

export interface IAnswerSelectionType {
    answerId: number,
    isSelected: boolean
}

export interface ISubmitAnswersModel {
    questionId: number,
    healthAssesmentId: number,
    answers: IAnswerSelectionType[]
}

export interface IAnswer {
    id: number,
    questionId: number,
    orderNo: number,
    text: string,
    isActive: boolean,
    isSelected: boolean,
    isSavedInDatabase: boolean
}

export interface IAnswerProps {
    question: IQuestion
}

export const Answer: React.FunctionComponent<IAnswerProps> = (props) => {
    const { question } = props;
    const answers: IAnswer[] = useSelector((state: RootState) =>
        state.survey.Answers
    );
    const healthAssesment: IHealthAssesment = useSelector((state: RootState) => state.survey.HealthAssesment);
    const dispatch: any = useDispatch();

    // const filteredAnswers = answers.filter(item => item.questionId === question.id);

    const [filteredAnswers, setFilteredAnswers] = useState<IAnswer[]>(answers.filter(item => item.questionId === question.id));
    const [selectedAnswerId, setSelectedAnswerId] = useState<number | null>(filteredAnswers.find( x => x.isSelected)?.id || null);
    const [selectedAnswersIds, setSelectedAnswersIds] = useState<Set<number>>(new Set(filteredAnswers.filter(x => x.isSelected).map(x => x.id)));

    useEffect(()=>{
        // console.log(selectedAnswerId)
        // console.log(selectedAnswersIds)
        console.log(filteredAnswers)
        if(question.isMultipleSelect) handleMultipleSelectChange();
        else handleSingleSelectChange();
    },[selectedAnswerId, selectedAnswersIds]);
    
    const handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const answerId = Number(event.target.value);

        setSelectedAnswersIds(prevSelected => {
            const newSelected = new Set(prevSelected);
            if (newSelected.has(answerId)) {
                newSelected.delete(answerId);
            } else {
                newSelected.add(answerId);
            }
            return newSelected;
        });
    };

    const handleRadioChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSelectedAnswerId(Number(event.target.value));
    };

    const handleMultipleSelectChange = (): void => {
        if (selectedAnswersIds.size > 0) {
            const updatedAnswers = filteredAnswers.map(answer => {
                return selectedAnswersIds.has(answer.id)
                    ? { ...answer, isSelected: true }
                    : { ...answer, isSelected: false };
            });
            const databasePackage: ISubmitAnswersModel = {
                questionId: question.id!,
                healthAssesmentId: healthAssesment.id!,
                answers: updatedAnswers.map(answer => ({
                    answerId: answer.id,
                    isSelected: answer.isSelected
                }))
            };
            // console.log(databasePackage)
            let newAnswers: IAnswer[] = answers.map(answer => answer.questionId === question.id ?
                updatedAnswers.find(a => a.id === answer.id)  || answer
                : answer)
            dispatch(surveySlice.actions.setAnswers(
                newAnswers
                )
            );
            // console.log(question.id)
            dispatch(SubmitAnswersInDatabase(fetch, databasePackage));
        }
        // console.log('answers to check', answers.filter(x => x.questionId == question.id))
    };

    // useEffect(() => {
    //     handleMultipleSelectChange();
        
    // }, [selectedAnswersIds]);

    const handleSingleSelectChange = (): void => {
        if (selectedAnswerId !== null) {
            const updatedAnswers = filteredAnswers.map(answer => {
                return answer.id === selectedAnswerId
                    ? { ...answer, isSelected: true }
                    : { ...answer, isSelected: false };
            });

            const databasePackage: ISubmitAnswersModel = {
                questionId: question.id!,
                healthAssesmentId: healthAssesment.id!,
                answers: updatedAnswers.map(answer => ({
                    answerId: answer.id,
                    isSelected: answer.isSelected
                }))
            };

            let newAnswers: IAnswer[] =  answers.map(answer =>
                answer.questionId === question.id
                    ? updatedAnswers.find(a => a.id === answer.id) || answer
                    : answer
            )

            dispatch(surveySlice.actions.setAnswers(
                newAnswers
            ));

            dispatch(SubmitAnswersInDatabase(fetch, databasePackage));
        }
    };

    // useEffect(() => {
    //     handleSingleSelectChange();
    // }, [selectedAnswerId]);


    if (question.isMultipleSelect) {
        return (
            <div className="bg-white p-3 border rounded ">
            {/* {question.id} */}
                    <Row>
                        {filteredAnswers
                        .map((answer: IAnswer) => (
                            <Col xs={12} sm={12} md={6} key={answer.id}>
                                <Form.Check
                                    type="checkbox"
                                    color=""
                                    label={`- ${answer.text}`}
                                    value={answer.id}
                                    checked={selectedAnswersIds.has(answer.id)}
                                    onChange={handleCheckboxChange}
                                />
                            </Col>
                        ))}
                    </Row>
              
            </div>
        );
    }

    return (
        <div className="bg-white p-3 border rounded w-100">
            {/* {question.id} */}
            <Form >
          
                    <Row >
                        {filteredAnswers
                        .map((answer: IAnswer) => (
                            <Col xs={12} sm={12} md={12} key={answer.id}>
                                
                                <Form.Check
                                    type="checkbox"
                                    label={`- ${answer.text}`}
                                    name="answers"
                                    value={answer.id}
                                    checked={selectedAnswerId == answer.id}
                                    onChange={handleRadioChange}
                                />
                            </Col>
                        ))}
                    </Row>
              
            </Form>
        </div>
    );
};

