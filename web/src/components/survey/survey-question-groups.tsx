import React, { useEffect, useState } from 'react';
import "./survey-question-groups.css";
import { IQuestion, SurveyQuestions } from './survey-questions/survey-questions';
import KeyboardDoubleArrowDownIcon from '@mui/icons-material/KeyboardDoubleArrowDown';
import KeyboardDoubleArrowUpIcon from '@mui/icons-material/KeyboardDoubleArrowUp';
import KeyboardDoubleArrowRightIcon from '@mui/icons-material/KeyboardDoubleArrowRight';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../../store/store';
import { fetchQuestionGroups, fetchQuestions } from '../../store/survey/survey-actions';
import Alergije from '../../assets/question-groups/Alergije.jpg'
import Anestezija from '../../assets/question-groups/Anestezija.jpg'
import Lekovi from '../../assets/question-groups/Lekovi.jpg'
import PrevencijaiScreening from '../../assets/question-groups/prevencijaiscreening.webp'
import SimptomiBolesti from '../../assets/question-groups/simptomiBolesti.jpg'
import Srce from '../../assets/question-groups/Srce.jpg'
import Pluca from '../../assets/question-groups/Pluca.avif'
import Stetoskop from '../../assets/question-groups/Stetoskop.jpg'
import Endokrini from '../../assets/question-groups/Endokrini.jpg'
import { Colors } from '../../constants/constants';
import { Accordion, AccordionDetails, AccordionSummary, Box, Grid, IconButton, Typography } from '@mui/material';
import { ExpandLess, ExpandMore, Margin, Widgets } from '@mui/icons-material';
import { IAnswer } from './survey-questions/answers/answer';
// import Pluca from '../../assets/question-groups/Pluca.avif'



// export interface IUserAnswer {
//     id: number,
//     value: string
// }

export interface IQuestionGroup {
    id: number | undefined,
    name: string | undefined,
    inLevel: number | undefined,
    parentId: number | undefined,
    questions: any[] | undefined,
    subgroups: any[] | undefined,
    url: string | undefined
}

export interface IApiResponse<T> {
    Data: T,
    IsSuccess: boolean,
    Message: string,
    StatusCode: number
}

export interface IQuestionGroupProps {
    questionGroups?: IQuestionGroup[],
    questions?: IQuestion[],
    groupName?: string
}

export const QuestionGroups: React.FunctionComponent<IQuestionGroupProps> = props => {
    const { groupName, questionGroups, questions } = props
    const [expandedCategory, setExpandedCategory] = useState<number | null>();
    const numberOfAnsweredQuestions: number[] = useSelector((state: RootState) =>
        Array.from(
            new Set(
                state.survey.Answers
                    .filter((x: IAnswer) => x.isAnswered)
                    .map((x: IAnswer) => x.questionId)
            )
        )
    );
    // let toExpand = false;
    // const [toExpand, setToExpand] = useState<boolean>(false);
    
    // console.log('numberOfAnsweredQuestions', numberOfAnsweredQuestions)

    const handleCategoryClick = (index: number): void => {
        if (expandedCategory == index) {
            setExpandedCategory(null);
        }
        else {
            setExpandedCategory(index);
        }

    }

    type ImageMap = {
        [key: string]: string;
    };

    const imgs: ImageMap = {
        'Alergije': Alergije,
        'Anestezija': Anestezija,
        'Lekovi': Lekovi,
        'Prevencija i screening': PrevencijaiScreening,
        'Simptomi bolesti': SimptomiBolesti,
        'KARDIOVASKULARNI SISTEM': Srce,
        'RESPIRATORNI SISTEM': Pluca,
        'OSTALA MEDICINSKA ISTORIJA': Stetoskop,
        'ENDOKRINI': Endokrini
    };

    
    
    const Options = (): JSX.Element[] => {
        if (!questionGroups) return [];
    
        return questionGroups.map((group, i) => {
            const imgStyle = {
                width: 60,
                height: 60,
                backgroundImage: `url(${imgs[group.name || 'def']})`,
                backgroundSize: 'cover',
                backgroundPosition: 'center',
                borderRadius: '50%',
                mr: 2,
                border: '1px solid lightgrey'
            };
    
            const imgStyle2 = {
                width: 310,
                height: 300,
                backgroundImage: `url(${imgs[group.name || 'def']})`,
                backgroundSize: 'cover',
                backgroundPosition: 'center',
                borderRadius: '50%',
                mr: 0,
                border: '1px dotted lightgrey',
                mb: 2
            };

            //Ovde se odredjuje broj odgovorenih pitanja i ukupan broj pitanja po kategorijama
            // toExpand = expandedCategory === i;
            let toExpand = expandedCategory === i;

            const numberOfQuestions = questions ? questions
            .filter(x => x.questionGroupId === group.id)
            .map(q => q.questionGroupId) : [];

            const numberOfAnswered = questions ? questions
            .filter(question => question.questionGroupId === group.id && numberOfAnsweredQuestions.includes(question.id ? question.id : -1)) : [];

            const completedGroup = (numberOfAnswered ? numberOfAnswered.length : 0) == numberOfQuestions.length
    
            return (
                <div className={`${'image-container'} p-0`} key={i}>
                    <Accordion
                        expanded={toExpand}
                        onChange={() => handleCategoryClick(i)}
                        sx={{ mb: 1, p: 0, backgroundColor: completedGroup ? Colors.SuccessColor : Colors.MMYellow2lighter }} //accord color
                    >
                        <AccordionSummary
                            expandIcon={toExpand ? <ExpandLess /> : <ExpandMore />}
                            aria-controls={`panel${i}a-content`}
                            id={`panel${i}a-header`}
                           
                            // onClick={()=> setToExpand(!toExpand)}
                        >
                            <div className={`ThisShouldDissapearWhenAccordionOpensDetails ${toExpand ? 'hidden' : 'fade-out'}`}>
                                <Box sx={{ display: 'flex', alignItems: 'center', width: '100%' }}>
                                    <Box sx={imgStyle} />
                                    <Box sx={{ flexGrow: 0,  }}>
                                        <Typography  sx={{fontSize: '13px'}}
                                        >{group.name?.toUpperCase()}</Typography>
                                        <Typography variant="body2" sx={{fontSize: '13px'}}
                                        >
                                            {numberOfAnswered ? numberOfAnswered.length : 0}/{numberOfQuestions.length}
                                        </Typography>
                                    </Box>
                                </Box>
                            </div>
                        </AccordionSummary>
                        <AccordionDetails sx={{ p: 0 }}>
                            <Box>
                                <Typography variant="h4" sx={{ textAlign: 'center', mb: 2 }}>
                                {group.name}
                                </Typography>
                            </Box>
                            <Grid item xs={12} md={4} sx={{ width: '100%', display: 'flex', justifyContent: 'center', borderBottom: '1px dotted lightgrey' }}>
                                <Box sx={imgStyle2} />
                            </Grid>
    
                            <SurveyQuestions questionsFiltered={questions ? questions.filter(x => x.questionGroupId === group.id) : []} 
                                completedGroup={completedGroup}/>
                            <Box sx={{ display: 'flex', justifyContent: 'flex-end', mt: 0 }}>
                                <IconButton onClick={() => setExpandedCategory(null)}>
                                    <Typography variant="body2">ZATVORI</Typography>
                                </IconButton>
                            </Box>
                        </AccordionDetails>
                    </Accordion>
                </div>
            );
        });
    };
    

    return (
        <div >
            <h3 className='text-center' >{groupName}</h3>
            {Options()}
        </div>
    );

};
