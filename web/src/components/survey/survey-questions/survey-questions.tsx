import React from 'react';
import { useSelector } from 'react-redux';
import { RootState } from '../../../store/store';
import { Answer } from './answers/answer';
import { Card, CardContent, Typography, Grid, useTheme } from '@mui/material';
import { Colors, Sizes } from '../../../constants/constants';

export interface INewSurveyProps {
  questionsFiltered: IQuestion[] | any[];
}

export interface IQuestion {
    id: number | undefined,
    text: string | undefined,
    additionalComment?: string | undefined,
    questionGroupId: number | undefined,
    image?: string | undefined,
    // answers?: IUserAnswer[] | undefined,
    userAnswered?: number[] | undefined,
    isMultipleSelect: boolean,
    maxSelect: number
}

export const SurveyQuestions: React.FunctionComponent<INewSurveyProps> = ({ questionsFiltered }) => {
  const browserWidth = useSelector((state: RootState) => state.general.browserWidth) || window.innerWidth;
  const theme = useTheme(); // For theme-based styling if needed


  return (
      <Card sx={{ backgroundColor: Colors.MMYellow2lighter, padding: 0, border: 'none', boxShadow: 'none', zIndex: 1000 }}>
          {questionsFiltered.map((question: IQuestion, i: number) => {
              if (question.id === undefined) return null;
              let flexDirection = browserWidth > Sizes.BreakPoint ? 'row' : 'column'
              let justContent = browserWidth > Sizes.BreakPoint ? 'end' : 'start'

              return (
                  <Card key={question.id} sx={{ marginBottom: 1, borderRadius: 0, borderBottom: '1px dotted', borderColor: 'lightgrey', boxShadow: 'none', backgroundColor: Colors.MMYellow2lighter }}>
                      <CardContent sx={{ display: 'flex', border: 'none', flexDirection: flexDirection, alignItems: 'center' }}>
                          <Grid container spacing={5} alignItems="center">
                              <Grid item xs={12} md={6} >
                                  <Typography align="center" sx={{ backgroundColor: Colors.MMYellow2lighter }}>
                                      <strong >{question.text}</strong>
                                  </Typography>
                              </Grid>
                              <Grid item xs={12} md={6} sx={{ display: 'flex', justifyContent: justContent, alignItems: 'center' }}>
                                  <Answer question={question}  />
                              </Grid>
                          </Grid>
                      </CardContent>
                  </Card>
              );
          })}
      </Card>
  );
};
