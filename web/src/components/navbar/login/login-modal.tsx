import React, { useState } from 'react';
import GoogleIcon from '@mui/icons-material/Google';
import FacebookIcon from '@mui/icons-material/Facebook';
import XIcon from '@mui/icons-material/X';
import InstagramIcon from '@mui/icons-material/Instagram';
import { Box, Button, IconButton, InputAdornment, TextField, Typography, Tab, Tabs, Link } from '@mui/material';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';
import { useDispatch, useSelector } from 'react-redux';
import { setUserData } from '../../../store/login-modal/login-modal';
import { RegisterUser, LoginUser } from '../../../store/login-modal/login-modal-actions';
import { RootState } from '../../../store/store';
import { Colors } from '../../../constants/constants';

export interface ILoginPageProps {
    id?: string;
    header?: React.ReactNode;
    body?: React.ReactNode;
    footer?: React.ReactNode;
}

export const LoginModal: React.FunctionComponent<ILoginPageProps> = (props) => {
    const [activeTab, setActiveTab] = useState<string>('login');
    const [showPassword, setShowPassword] = useState<boolean>(false);
    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [repeatPassword, setRepeatPassword] = useState<string>('');
    const [errors, setErrors] = useState<{ email?: string; password?: string; repeatPassword?: string }>({});
    const dispatch: any = useDispatch();
   
    const userData = useSelector((state: RootState) => state.loginModal)

    const handleClickShowPassword = () => {
        setShowPassword(!showPassword);
    };

    const handleTabClick = (event: React.SyntheticEvent, newValue: string) => {
        setActiveTab(newValue);
    };

    const validateEmail = (email: string) => {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    };

    const validatePassword = (password: string) => {
        return password.length >= 8;
    };

    const handleLoginButton = (event: React.MouseEvent<HTMLButtonElement>) => {
        event.preventDefault();
        dispatch(LoginUser(fetch, email, password))
        dispatch(setUserData({...userData,IsAuthenticated:true}))
    }

    const handleRegisterButton = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const newErrors: { email?: string; password?: string; repeatPassword?: string } = {};

        if (!validateEmail(email)) {
            newErrors.email = 'Please enter a valid email address.';
        }

        if (!validatePassword(password)) {
            newErrors.password = 'Password must be at least 8 characters long.';
        }

        if (password !== repeatPassword) {
            newErrors.repeatPassword = 'Passwords do not match.';
        }

        if (Object.keys(newErrors).length === 0) {
            dispatch(RegisterUser(fetch, email, password))
            dispatch(setUserData({...userData,IsAuthenticated:false}));
            setActiveTab('login')
        } else {
            setErrors(newErrors);
        }
    };

    const signUpOptions = (
        <div className="text-center mb-3">
            <Typography color="primary" sx={{color: 'black'}}><p>Sign up with:</p> </Typography>
            <Button 
                variant="contained"
                color="primary"
                sx={{
                    display: 'flex',
                    flexDirection: 'row',
                    justifyContent: 'center', 
                    backgroundColor: Colors.MMYellow1,
                    border: '1px solid lightgrey',
                    gap: 0,
                    width: '100%',
                    p: 0,
                    color: 'black',
                    '&:hover': {
                        backgroundColor: 'black', 
                        borderColor: 'gray', 
                        color: 'white', 
                    },
                }}>
                <GoogleIcon sx={{ mr: 0 }}  />
                <strong className='mt-2'>OOGLE (disabled)</strong>
            </Button>
            <Typography color="primary" sx={{mt:2, color: 'black'}}><p>Or:</p> </Typography>
        </div>
    );

    return (
        <Box p={0.5}>
            <Tabs
                value={activeTab}
                onChange={handleTabClick}
                variant="fullWidth"
                sx={{
                    mb: 3,
                    '& .MuiTabs-indicator': {
                        backgroundColor: 'black', 
                    },
                }}
                centered
            >
                <Tab
                    label="Login"
                    value="login"
                    sx={{
                        color: 'black',
                        '&.Mui-selected': {
                            color: 'black', 
                        },
                    }}
                />
                <Tab
                    label="Register"
                    value="register"
                    sx={{
                        color: 'black',
                        '&.Mui-selected': {
                            color: 'black', 
                        },
                    }}
                />
            </Tabs>


            {activeTab === 'login' && (
                <Box component="form" onSubmit={() => {}}>
                    {signUpOptions}
                    <Box mb={2}>
                        <TextField 

                            fullWidth 
                            label="Email" 
                            size="small" 
                            value={email} 
                            onChange={(e) => setEmail(e.target.value)} 
                            InputLabelProps={{
                                shrink: true, 
                            }}
                            // InputProps={{
                            //     style: {
                            //         color: email ? 'white' : 'white'
                            //     },
                            // }} 
                            sx={{ 
                                '& .MuiOutlinedInput-root': {
                                    '& fieldset': {
                                        borderColor: 'lightgrey',
                                    },
                                    '&:hover fieldset': {
                                        borderColor: 'black',
                                    },
                                    '&.Mui-focused fieldset': {
                                        borderColor: 'lightgrey',
                                    },
                                },
                            }}
                        />
                    </Box>
                    <Box mb={2}>
                        <TextField
                            fullWidth
                            label="Password"
                            type={showPassword ? 'text' : 'password'}
                            size="small"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            InputLabelProps={{
                                shrink: true, 
                            }}
                            // InputProps={{
                            //     style: {
                            //         color: email ? 'white' : 'white'
                            //     },
                            // }} 
                            sx={{ 
                                '& .MuiOutlinedInput-root': {
                                    '& fieldset': {
                                        borderColor: 'lightgrey',
                                    },
                                    '&:hover fieldset': {
                                        borderColor: 'black',
                                    },
                                    '&.Mui-focused fieldset': {
                                        borderColor: 'lightgrey',
                                    },
                                },
                            }}
                            InputProps={{
                                endAdornment: (
                                    <InputAdornment position="end">
                                        <IconButton
                                            aria-label="toggle password visibility"
                                            onClick={handleClickShowPassword}
                                            edge="end"
                                        >
                                            {showPassword ? <VisibilityOff /> : <Visibility />}
                                        </IconButton>
                                    </InputAdornment>
                                ),
                            }}
                        />
                    </Box>
                    <Button sx={{backgroundColor: Colors.MMYellow1, color: 'black', border: '1px solid lightgrey'
                        , '&:hover': {
                            backgroundColor: 'black', 
                            borderColor: 'gray', 
                            color: 'white', 
                        },
                    }} type="submit" variant="contained" fullWidth  onClick={(e) => handleLoginButton(e)}>
                        Sign in
                    </Button>
                </Box>
            )}

            {activeTab === 'register' && (
                <Box component="form" onSubmit={handleRegisterButton}>
                    {signUpOptions}
                    {/* <Typography textAlign="center">or:</Typography> */}
                    <Box mb={2}>
                        <TextField
                            fullWidth
                            label="Email"
                            size="small"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            error={!!errors.email}
                            helperText={errors.email}
                            InputLabelProps={{
                                shrink: true, 
                            }}
                            sx={{ 
                                '& .MuiOutlinedInput-root': {
                                    '& fieldset': {
                                        borderColor: 'lightgrey',
                                    },
                                    '&:hover fieldset': {
                                        borderColor: 'black',
                                    },
                                    '&.Mui-focused fieldset': {
                                        borderColor: 'lightgrey',
                                    },
                                },
                            }}
                        />
                    </Box>
                    <Box mb={2}>
                        <TextField
                            fullWidth
                            label="Password"
                            type={showPassword ? 'text' : 'password'}
                            size="small"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            error={!!errors.password}
                            helperText={errors.password}
                            InputLabelProps={{
                                shrink: true, 
                            }}
                            sx={{ 
                                '& .MuiOutlinedInput-root': {
                                    '& fieldset': {
                                        borderColor: 'lightgrey',
                                    },
                                    '&:hover fieldset': {
                                        borderColor: 'black',
                                    },
                                    '&.Mui-focused fieldset': {
                                        borderColor: 'lightgrey',
                                    },
                                },
                            }}
                            InputProps={{
                                endAdornment: (
                                    <InputAdornment position="end">
                                        <IconButton
                                            aria-label="toggle password visibility"
                                            onClick={handleClickShowPassword}
                                            edge="end"
                                        >
                                            {showPassword ? <VisibilityOff /> : <Visibility />}
                                        </IconButton>
                                    </InputAdornment>
                                ),
                            }}
                        />
                    </Box>
                    <Box mb={2} >
                        <TextField
                            fullWidth
                            label="Repeat Password"
                            type={showPassword ? 'text' : 'password'}
                            size="small"
                            value={repeatPassword}
                            onChange={(e) => setRepeatPassword(e.target.value)}
                            error={!!errors.repeatPassword}
                            helperText={errors.repeatPassword}
                            InputLabelProps={{
                                shrink: true, 
                            }}
                            sx={{ 
                                '& .MuiOutlinedInput-root': {
                                    '& fieldset': {
                                        borderColor: 'lightgrey',
                                    },
                                    '&:hover fieldset': {
                                        borderColor: 'black',
                                    },
                                    '&.Mui-focused fieldset': {
                                        borderColor: 'lightgrey',
                                    },
                                },
                            }}
                            InputProps={{
                                endAdornment: (
                                    <InputAdornment position="end">
                                        <IconButton
                                            aria-label="toggle password visibility"
                                            onClick={handleClickShowPassword}
                                            edge="end"
                                        >
                                            {showPassword ? <VisibilityOff /> : <Visibility />}
                                        </IconButton>
                                    </InputAdornment>
                                ),
                            }}
                        />
                    </Box>
                    <Button sx={{backgroundColor: Colors.MMYellow1, color: 'black', border: '1px solid lightgrey'
                        , '&:hover': {
                            backgroundColor: 'black', 
                            borderColor: 'gray', 
                            color: 'white', 
                        },
                    }} type="submit" variant="contained" fullWidth color="primary">
                        Sign up
                    </Button>
                </Box>
            )}
        </Box>
    );
};
