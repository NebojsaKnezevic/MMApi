import { Box, Typography, Button, Grid } from "@mui/material";
import { Colors } from "../../../constants/constants";
import { useDispatch, useSelector } from "react-redux";
import { IUser } from "../../../store/login-modal/login-modal";
import { RootState } from "../../../store/store";
import { CreateaHealthAssesment, GetHealthAssesment } from "../../../store/survey/survey-actions";
import surveySlice from "../../../store/survey/survey-slice";

export interface ISurveyStartProps {
    setCurrentPage: React.Dispatch<React.SetStateAction<number>>;
 }

 export interface IHealthAssesment{
    id: number | null,
    userId: number | null,
    startedOn: Date | null,
    completedOn: Date | null
 }

export const SurveyStart: React.FunctionComponent<ISurveyStartProps> = (props) => {
    const {setCurrentPage} = props;
    const dispatch: any = useDispatch();
    const userData: IUser = useSelector((state: RootState)=>state.loginModal.userData)

    const handleStartNewAssesment = () =>{
        if(userData.id != undefined)
        {
            // dispatch(surveySlice.actions.setAnswers([]))
            dispatch(CreateaHealthAssesment(fetch, userData.id))
        }   
        else
            console.error("User data is corrupted or missing completely");
    }

    const handleContinueWithLatestHealthAssesment = () => {
        if(userData.id != undefined)
            dispatch(GetHealthAssesment(fetch, userData.id))
        else
            console.error("User data is corrupted or missing completely");
    }


    return (
        <Box
            sx={{
                minHeight: '80vh',
                display: 'flex',
                flexDirection: 'column',
                justifyContent: 'center',
                alignItems: 'center',
                textAlign: 'center',
                px: 2, 
            }}
        >
            <Typography
                variant="h5"
                component="p"
                sx={{ pb: 2 }}
            >
                Dobrodošli u našu anketu!
            </Typography>
            <Typography
                variant="body1"
                component="p"
                sx={{ pb: 4, maxWidth: '600px' }}
            >
                Cenimo vaše vreme i učešće u ovoj anketi. Vaši odgovori nam pomažu da unapredimo naše usluge. Anketa će vam oduzeti oko 10 minuta.
            </Typography>
            {/* <Typography
                variant="h4"
                component="h2"
                sx={{ pb: 2, mb: 0, borderColor: 'divider' }}
            >
                Upitnik za sistematski pregled
            </Typography> */}


            <Grid container spacing={0} xs={12} sx={{ mt: 3, width: '80%', justifyContent: 'center' }}>
                <Grid item md={6}  sx={{ display: 'flex', justifyContent: 'center', mt:2, width:'100%' }}>
                <Button sx={{backgroundColor: Colors.MMYellow1, color: 'black', border: '1px solid lightgrey',mx:5
                        , '&:hover': {
                            backgroundColor: 'black', 
                            borderColor: 'gray', 
                            color: 'white', 
                        },
                    }} type="submit" variant="contained" fullWidth  onClick={() => {setCurrentPage(1); handleStartNewAssesment() }}>
                        ZAPOCNI NOVU
                    </Button>
                </Grid>
                <Grid item md={6}  sx={{ display: 'flex', justifyContent: 'center', mt:2, width:'100%' }}>
                <Button sx={{backgroundColor: Colors.MMYellow1, color: 'black', border: '1px solid lightgrey',mx:5
                        , '&:hover': {
                            backgroundColor: 'black', 
                            borderColor: 'gray', 
                            color: 'white', 
                        },
                    }} type="submit" variant="contained" fullWidth  onClick={() => {setCurrentPage(1); handleContinueWithLatestHealthAssesment()}}>
                        NASTAVI
                    </Button>
                </Grid>
            </Grid>

        </Box>
    );
}
