// import { useEffect, useState } from 'react';
import FormImg from '../../../assets/question-groups/Form.avif'
import './survey-form.css'
import { useSelector } from 'react-redux';
import { RootState } from '../../../store/store';
import {
    Box,
    Card,
    CardContent,
    Divider,
    FormControl,
    FormControlLabel,
    FormGroup,
    FormLabel,
    Radio,
    RadioGroup,
    TextField,
    Typography,
    Checkbox,
    // Button,
} from '@mui/material';
import { useTheme } from '@mui/material/styles';
import { useMediaQuery } from '@mui/material';
import { Colors, Sizes } from '../../../constants/constants';



export interface ISurveyFormProps {

}

export const SurveyForm: React.FunctionComponent<ISurveyFormProps> = props => {
    const browserWidth = useSelector((state: RootState) => state.general.browserWidth) || window.innerWidth
    const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down('sm'));

    return (
        <Box sx={{backgroundColor: 'white'}}>
        <div className={`mx-0 mx-sm-auto mt-0 border-0 d-flex justify-content-center ${browserWidth <= Sizes.BreakPoint ? 'flex-column' : ''}`}>
            <div className=' w-100 d-flex align-items-center' style={{ backgroundColor: 'rgba(219,239,239)' }}>
                <img
                    src={FormImg}
                    alt="Form Image"
                    className="img-fluid w-100 circular-image px-1"

                />
            </div>

            <div className="card border-0 w-100" style={{ backgroundColor: Colors.MMYellow2lighter }}> 
                <Box
                    sx={{
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                        justifyContent: 'center',
                        width: '100%',
                    }}
                >
                    <Card sx={{ width: '100%',border:'none', padding: 0, margine: 0, backgroundColor: Colors.MMYellow2lighter,   boxShadow: '0 0 0 0px rgba(0,0,0,0.1)',}}>
                        <CardContent sx={{ borderTop: 'none', borderRight: 'none', borderBottom: 'none', borderLeft: 'none', backgroundColor:Colors.MMYellow2lighter }}>
                            <Box sx={{ textAlign: 'center', mb: 2 }}>
                                <i className="far fa-file-alt fa-4x mb-3 text-primary"></i>
                                <Typography variant="h6">
                                    <strong>OPŠTI PODACI</strong>
                                </Typography>
                            </Box>

                            <Divider sx={{ mb: 2 }} />

                            <form className='border-0'>
                                <TextField
                                        fullWidth
                                        label="Ime"
                                        variant="outlined"
                                        margin="normal"
                                        placeholder="Unesite ime i prezime"
                                        id="fullName"
                                        size="small"
                                        InputLabelProps={{
                                            shrink: true, 
                                        }}
                                    />

                                <TextField
                                        fullWidth
                                        label="prezime"
                                        variant="outlined"
                                        margin="normal"
                                        placeholder="Unesite ime i prezime"
                                        id="fullName"
                                        size="small"
                                        InputLabelProps={{
                                            shrink: true, 
                                        }}
                                    />


                                <TextField
                                    fullWidth
                                    label="Datum rođenja"
                                    type="date"
                                    InputLabelProps={{ shrink: true }}
                                    variant="outlined"
                                    margin="normal"
                                    id="birthDate"
                                    size='small'
                                />

                                <FormControl component="fieldset" margin="normal">
                                    <FormLabel component="legend">Pol</FormLabel>
                                    <RadioGroup row aria-labelledby="gender" name="gender" >
                                        <FormControlLabel value="male" control={<Radio />} label="Muški" />
                                        <FormControlLabel value="female" control={<Radio />} label="Ženski" />
                                    </RadioGroup>
                                </FormControl>

                                <TextField
                                    fullWidth
                                    label="Kontakt telefon"
                                    type="tel"
                                    variant="outlined"
                                    margin="normal"
                                    placeholder="Unesite kontakt telefon"
                                    id="phone"
                                    size='small'
                                    InputLabelProps={{ shrink: true }}
                                />

                                <TextField
                                    fullWidth
                                    label="Email adresa"
                                    type="email"
                                    variant="outlined"
                                    margin="normal"
                                    placeholder="Unesite email adresu"
                                    id="email"
                                    size='small'
                                    InputLabelProps={{ shrink: true }}
                                />

                                <Divider sx={{ my: 2 }} />

                                <Typography variant="h6" sx={{ textAlign: 'center', mb: 2 }}>
                                    <strong>PORODIČNA ISTORIJA</strong>
                                </Typography>

                                <FormControl component="fieldset" margin="normal">
                                    <FormLabel component="legend">
                                        Da li u vašoj porodici postoji istorija sledećih bolesti? (Označite ispod koju)
                                    </FormLabel>
                                    <FormGroup>
                                        <FormControlLabel control={<Checkbox />} label="Kardiovaskularne bolesti" />
                                        <FormControlLabel control={<Checkbox />} label="Dijabetes" />
                                        <FormControlLabel control={<Checkbox />} label="Kancer (ukoliko da navedite koja vrsta kancera)" />
                                        <TextField
                                            fullWidth
                                            multiline
                                            rows={3}
                                            variant="outlined"
                                            placeholder="Navedi..."
                                            sx={{ mb: 2 }}
                                            size='small'
                                        />
                                        <FormControlLabel control={<Checkbox />} label="Visok krvni pritisak" />
                                        <FormControlLabel control={<Checkbox />} label="Ostalo (navedite):" />
                                        <TextField
                                            fullWidth
                                            multiline
                                            rows={3}
                                            variant="outlined"
                                            placeholder="Navedi..."
                                            sx={{ mb: 2 }}
                                            size='small'
                                        />
                                    </FormGroup>
                                </FormControl>

                                <FormControl component="fieldset" margin="normal">
                                    <FormLabel component="legend">
                                        Da li je neko od vaših bliskih rođaka imao ozbiljnu mentalnu bolest? (npr. depresija, šizofrenija)
                                    </FormLabel>
                                    <RadioGroup row aria-labelledby="mentalIllness">
                                        <FormControlLabel value="yes" control={<Radio />} label="Da" />
                                        <FormControlLabel value="no" control={<Radio />} label="Ne" />
                                    </RadioGroup>
                                </FormControl>
                                
                            </form>
                        </CardContent>
                    </Card>
                </Box>
            </div>
        </div>
        </Box>
    );
}

