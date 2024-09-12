
import Textarea from '@mui/joy/Textarea';
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
    isSavedInDatabase: boolean,
    isAnswered: boolean
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
    const [selectedAnswerId, setSelectedAnswerId] = useState<number | null>(filteredAnswers.find(x => x.isSelected)?.id || null);
    const [selectedAnswersIds, setSelectedAnswersIds] = useState<Set<number>>(new Set(filteredAnswers.filter(x => x.isSelected).map(x => x.id)));

    useEffect(() => {
        // console.log(selectedAnswerId)
        // console.log(selectedAnswersIds)
        // console.log(filteredAnswers)
        if (question.isMultipleSelect) handleMultipleSelectChange();
        else handleSingleSelectChange();
    }, [selectedAnswerId, selectedAnswersIds]);

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
        // if (selectedAnswersIds.size >= 0) {
            const reduxPackage = filteredAnswers.map(answer => {
                return selectedAnswersIds.has(answer.id)
                    ? { ...answer, isSelected: true, isAnswered: true }
                    : { ...answer, isSelected: false, isAnswered: false };
            });

            const databasePackage: ISubmitAnswersModel = {
                questionId: question.id!,
                healthAssesmentId: healthAssesment.id!,
                answers: reduxPackage.map(answer => ({
                    answerId: answer.id,
                    isSelected: answer.isSelected
                }))
            };
            console.log('databasePackage',databasePackage)
            let newAnswers: IAnswer[] = answers.map(answer => 
                answer.questionId === question.id ?
                reduxPackage.find(a => a.id === answer.id) || answer
                : answer)
                dispatch(surveySlice.actions.setAnswers(newAnswers)
            );
            // console.log(question.id)
            dispatch(SubmitAnswersInDatabase(fetch, databasePackage));
        // }
        // console.log('answers to check', answers.filter(x => x.questionId == question.id))
        };

    // useEffect(() => {
    //     handleMultipleSelectChange();

    // }, [selectedAnswersIds]);

    const handleSingleSelectChange = (): void => {
        if (selectedAnswerId !== null) {
            const updatedAnswers = filteredAnswers.map(answer => {
                return answer.id === selectedAnswerId
                    ? { ...answer, isSelected: true, isAnswered: true }
                    : { ...answer, isSelected: false, isAnswered: false };
            });

            const databasePackage: ISubmitAnswersModel = {
                questionId: question.id!,
                healthAssesmentId: healthAssesment.id!,
                answers: updatedAnswers.map(answer => ({
                    answerId: answer.id,
                    isSelected: answer.isSelected
                }))
            };

            let newAnswers: IAnswer[] = answers.map(answer =>
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
    if (filteredAnswers.length == 0) {

        return (
            <div className="bg-white p-0 border rounded w-100">
                <Row sx={{ width: '100%  !important', margin: 0 }}>
                    <Col xs={12} sm={12} md={12} sx={{ padding: 0, width: '100% !important' }}>
                        <Textarea
                            sx={{ width: '100% !important', backgroundColor: 'white' }} //l
                            color="neutral"
                            disabled={false}
                            minRows={3}
                            size="lg"
                            variant="plain"
                            placeholder="Odgovor..."
                        />
                    </Col>
                </Row>
            </div>
        );
    }


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
                                    label={`${answer.id}`}
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
                                    label={`${answer.id}`}
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

