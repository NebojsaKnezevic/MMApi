import { Box, Typography, Button } from "@mui/material";

export interface ISurveyEndProps {}

export const SurveyEnd: React.FunctionComponent<ISurveyEndProps> = (props) => {
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
                variant="h4"
                component="h2"
                sx={{ pb: 2, mb: 0, borderColor: 'divider' }}
            >
                Hvala vam što ste popunili anketu!
            </Typography>
            <Typography
                variant="body1"
                component="p"
                sx={{ pb: 4, maxWidth: '600px' }}
            >
                
            </Typography>
            <Button variant="contained" color="primary" sx={{ mt: 3 }}>
                POŠALJI
            </Button>
        </Box>
    );
}
