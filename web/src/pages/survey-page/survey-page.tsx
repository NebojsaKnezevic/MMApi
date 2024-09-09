import { Routes, Route, Outlet, useNavigate, useLocation } from 'react-router-dom';
import { QuestionGroups } from "../../components/survey/survey-question-groups";
import { useDispatch, UseSelector, useSelector } from 'react-redux';
import { RootState } from '../../store/store';
import { useEffect, useState } from 'react';
import { fetchAnswers, fetchQuestionGroups, fetchQuestions } from '../../store/survey/survey-actions';
import { SurveyForm } from '../../components/survey/survey-form/survey-form';
import { Colors, Sizes } from '../../constants/constants';
import { IAnswer } from '../../components/survey/survey-questions/answers/answer';
import { Box, Button, dividerClasses, Stack, Typography } from '@mui/material';
import { SurveyStart } from '../../components/survey/survey-start/survey-start';
import { SurveyEnd } from '../../components/survey/survey-end/survey-end';

export interface ISurveyPage {

}

export const SurveyPage: React.FunctionComponent<ISurveyPage> = props => {
    const dispatch: any = useDispatch();
    const questionGroups = useSelector((state: RootState) => state.survey.QuestionGroups)
    const questions = useSelector((state: RootState) => state.survey.Questions)
    const answers: IAnswer[] = useSelector((state: RootState) => state.survey.Answers);
    const navigate = useNavigate();
    const location = useLocation();
    const [currentPage, setCurrentPage] = useState<number>(0);
    const browserWidth = useSelector((state: RootState) => state.general.browserWidth)
    const userData = useSelector((state: RootState) => state.loginModal)
    const healthAssesment = useSelector((state: RootState) => state.survey.HealthAssesment)
    // Navigate to the next page
    console.log(userData.userData)
    console.log(healthAssesment)
    const nextPage = () => {
        setCurrentPage(currentPage + 1)
    };
    // console.log(answers)
    // Navigate to the previous page
    const prevPage = () => {
        setCurrentPage(currentPage - 1)
    };

    useEffect(() => {
        if (currentPage == 0) {
            navigate(`/`)
        }
        else {
            navigate(`/${currentPage}`)
        }

    }, [currentPage]);

    useEffect(() => {
        // fetchQuestionGroups();
        // console.log(questionGroups)
        if (healthAssesment.id != null && questionGroups.length === 0) {
            dispatch(fetchQuestionGroups(fetch));
        }

        if (healthAssesment.id != null && questions.length === 0) {
            dispatch(fetchQuestions(fetch));
        }
        if (healthAssesment.id != null && answers.length === 0) {
            console.log('PRETEBANI ANSWERI')
            dispatch(fetchAnswers(fetch, userData.userData.id, healthAssesment.id));
        }
        // console.log('Exec sta trba')
        // fetchQuestions();
    }, [healthAssesment])
    // console.log('questionGroups', questionGroups)
    let boxWidth = {}
    if (browserWidth != undefined && browserWidth > Sizes.BreakPoint) boxWidth = { width: '75%' }
    if (browserWidth != undefined && browserWidth < Sizes.BreakPoint) boxWidth = { width: '100%' }

    const lastPage = 2 + questionGroups.filter(x => x.inLevel === 1).length;
    return (
        <Box sx={boxWidth}>
            <Box
                sx={{
                    my: 4,
                    p: 1,
                    borderRadius: 2,
                    bgcolor: Colors.MMYellow,
                    boxShadow: "0 4px 8px rgba(0, 0, 0, 0.3)",

                }}
            >


                <Routes>
                    <Route path='/' element={<SurveyStart setCurrentPage={setCurrentPage}/>}
                    />
                    <Route path='/1' element={<SurveyForm />} />

                    {questionGroups
                        .filter(x => x.inLevel === 1)
                        .map((group, index) => (
                            <Route
                                key={index}
                                path={`/${index + 2}`}
                                element={
                                    <QuestionGroups
                                        groupName={group.name}
                                        questionGroups={questionGroups.filter(x => x.parentId === group.id)}
                                        questions={questions}
                                    />
                                }
                            />
                        ))}

                    <Route path={`/${lastPage}`} element={<SurveyEnd />} />

                </Routes>

                <Outlet />

                {currentPage > 0 &&
                    <Stack direction="row" spacing={2} mt={2} justifyContent="space-between">
                        <Button
                            onClick={prevPage}
                            disabled={currentPage === 0}
                            sx={{
                                backgroundColor: 'black',
                                color: Colors.MMYellow1,
                                '&:hover': {
                                    backgroundColor: 'rgba(0, 0, 0, 0.8)',
                                },
                                '&.Mui-disabled': {
                                    backgroundColor: 'lightgrey',
                                    color: 'grey',
                                },
                            }}
                        >
                            Previous
                        </Button>
                        <Button
                            onClick={nextPage}
                            disabled={currentPage == lastPage}
                            sx={{
                                backgroundColor: 'black',
                                color: Colors.MMYellow1,
                                '&:hover': {
                                    backgroundColor: 'rgba(0, 0, 0, 0.8)',
                                },
                                '&.Mui-disabled': {
                                    backgroundColor: 'lightgrey',
                                    color: 'grey',
                                },
                            }}
                        >
                            Next
                        </Button>
                    </Stack>
                }



            </Box>
        </Box>
    );
}