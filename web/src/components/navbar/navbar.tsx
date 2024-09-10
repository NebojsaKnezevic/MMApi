
// import { useState } from 'react';
import logo from '../../MMC-Logo-40.png';
// import {Container, Nav, Navbar, NavLink } from 'react-bootstrap';
// import { Link } from 'react-router-dom';
import { LoginModal } from './login/login-modal';
import { CustomModal } from '../modal/modal';

// interface INavbar{

// }
// export const NavbarMain: React.FunctionComponent<INavbar> = (prop) =>  {
//     const [navbarOpen, setNavbarOpen] = useState<boolean>(false)
//     return(
//         <Navbar bg='white mb-2' expand='md' variant='dark' expanded={navbarOpen} className='m-0 p-0 '>
//         <Container className=''>
//           <Navbar.Brand className='text-black' >
//           <img
//                   src={logo}
//                   // width="30"
//                   height="35"
//                   className="d-inline-block align-top"
//                   alt="MajaMayo logo"
//               />
//           </Navbar.Brand>
//           <Navbar.Toggle onClick={() => setNavbarOpen(!navbarOpen)} className='bg-dark'/>
//             <div className='d-flex justify-content-end w-100'>
//             <Navbar.Collapse >
//             {/* <Nav className='me-auto'></Nav> */}
//             {/* <ul>
//                 <li>
//                     <Link to="/" onClick={() => setNavbarOpen(false)}>Survey1</Link>
//                 </li>
//                 <li>
//                     <Link to="/s" onClick={() => setNavbarOpen(false)}>Survey1</Link>
//                 </li>
//             </ul> */}
//             {/* <Nav.Link className='' as={NavLink} to="/" > */}

//               {/* <Link to="/" onClick={() => setNavbarOpen(false)}>Survey</Link>
//               <button type="button" data-mdb-button-init data-mdb-ripple-init className="btn btn-primary" data-mdb-modal-init data-mdb-target="#staticBackdrop1">
//                 Launch modal login form
//               </button> */}

//             {/* </Nav.Link> */}
//             {/* <Nav.Link className='' as={NavLink} to="/s" onClick={() => setNavbarOpen(false)}>
//               <Link to="/s" onClick={() => setNavbarOpen(false)}>Survey1</Link>
//             </Nav.Link> */}
//           </Navbar.Collapse>
//             </div>
//         </Container>
//         {/* <LoginModal/> */}
//         <CustomModal body={<LoginModal/>}/>
//       </Navbar>
//     );
// }
import * as React from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Menu from '@mui/material/Menu';
import MenuIcon from '@mui/icons-material/Menu';
import Container from '@mui/material/Container';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import Tooltip from '@mui/material/Tooltip';
import MenuItem from '@mui/material/MenuItem';
import AdbIcon from '@mui/icons-material/Adb';
import { useDispatch, useSelector } from 'react-redux';
import { IUser, setUserData } from '../../store/login-modal/login-modal';
import { RootState } from '../../store/store';
import { Colors } from '../../constants/constants';
import LogoutIcon from '@mui/icons-material/Logout';






function NavbarMain() {
  const UserData = useSelector((state: RootState) => state.loginModal)
  const HealthAssesment = useSelector((state: RootState) => state.survey.HealthAssesment)
  const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(null);
  const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);
  const dispatch: any = useDispatch();
  // console.log(UserData)
  

  const pages = [''];
  const settings = [ 
    UserData.userData.email != undefined ? UserData.userData.email : ''
    ,UserData.userData.id != null ? `User ID: ${UserData.userData.id}`  : ''
    ,HealthAssesment != null ? `HealthAssesment ID: ${HealthAssesment.id}`  : ''
  ,"Logout"];

  const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElNav(event.currentTarget);
  };
  const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElUser(event.currentTarget);
  };

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };

  const handleCloseUserMenu = () => {
    setAnchorElUser(null);
  };

  const handleLogout = (setting: string) => {
    if (setting === "Logout") dispatch(setUserData({ ...UserData, IsAuthenticated: false }))
  }

  const handleLogoutUser = async (): Promise<void> => {
    try {
      const response = await fetch(`${process.env.REACT_APP_API_ENDPOINT}/Survey/Command/LogoutUser`,{
        method: 'POST',
        headers: {'Content-Type':'application/json'},
        credentials: 'include'
      })
      console.log(await response.json())
      dispatch(setUserData( {
        IsAuthenticated: false,
        userData: {
            id: 0, firstName: '', lastName: '', email: ''
        }
        
    }))
    window.location.reload();
    } catch (error) {
      
    }
  }

  return (
    <AppBar position="static"
      sx={{
        backgroundColor: Colors.MMYellow1,
        color: 'black',
        border: '0.5px solid white',
        // boxShadow: '0px 4px 8px rgba(0, 0, 0, 0.2)',
        mb: 1,
        me: 0,
        pe: 0,
        display: 'fixed'
      }}>
      <Container maxWidth="xl">
        <Toolbar disableGutters >
          {/* Logo for larger screens */}
          <Box sx={{ display: { xs: 'none', md: 'flex' }, mr: 1 }}>
            <img src={logo} height="35" alt="MajaMayo logo" />
          </Box>
          {/* Logo for smaller screens */}
          <Box sx={{ display: { xs: 'flex', md: 'none' }, mr: 1 }}>
            <img src={logo} height="35" alt="MajaMayo logo" />
          </Box>
          {/* Navbar Toggle Button for mobile */}
          <Box sx={{ flexGrow: 1, display: { xs: 'flex', md: 'none' } }}>
            <IconButton
              size="large"
              aria-label="open menu"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={handleOpenNavMenu}
              color="inherit"
            >
              <MenuIcon />
            </IconButton>
            <Menu
              id="menu-appbar"
              anchorEl={anchorElNav}
              anchorOrigin={{
                vertical: 'bottom',
                horizontal: 'left',
              }}
              keepMounted
              transformOrigin={{
                vertical: 'top',
                horizontal: 'left',
              }}
              open={Boolean(anchorElNav)}
              onClose={handleCloseNavMenu}
              sx={{
                display: { xs: 'block', md: 'none' },
              }}
            >
              {pages.map((page) => (
                <MenuItem key={page} onClick={handleCloseNavMenu}>
                  <Typography textAlign="center" sx={{ color: 'black' }}>{page}</Typography>
                </MenuItem>
              ))}
            </Menu>
          </Box>

          {/* Navigation Buttons for larger screens */}
          <Box sx={{ flexGrow: 1, display: { xs: 'none', md: 'flex' } }}>
            {pages.map((page) => (
              <Button
                key={page}
                onClick={handleCloseNavMenu}
                sx={{ my: 2, color: 'black', display: 'block' }}
              >
                {page}
              </Button>
            ))}
          </Box>

          {/* User Menu */}
          <Box sx={{ flexGrow: 0 }}>
            <Tooltip title="Open settings">
              <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>

                <Avatar sx={{ border: '1px solid' }} alt={UserData.IsAuthenticated ? UserData.userData.email : "user"} src="/static/images/avatar/2.jpg" />
              </IconButton>
            </Tooltip>
            <Menu
              sx={{ mt: '45px' }}
              id="menu-appbar"
              anchorEl={anchorElUser}
              anchorOrigin={{
                vertical: 'top',
                horizontal: 'right',
              }}
              keepMounted
              transformOrigin={{
                vertical: 'top',
                horizontal: 'right',
              }}
              open={Boolean(anchorElUser)}
              onClose={handleCloseUserMenu}
            >
              {settings.map((setting) => (
                <MenuItem key={setting} onClick={handleCloseUserMenu}>
                  <Typography textAlign="center"sx={{display: 'flex', alignItems: 'center', justifyContent: 'start'}} onClick={() => { handleLogout(setting) }}>
                    {setting == "Logout" 
                    ? 
                    <Box onClick={handleLogoutUser}><LogoutIcon sx={{height: '18px'}}/>{setting}</Box> 
                    : 
                    <Box>{setting}</Box>}
                    </Typography>
                </MenuItem>
              ))}
            </Menu>
          </Box>
        </Toolbar>
      </Container>
      <CustomModal body={<LoginModal />} />
    </AppBar>
  );
}

export default NavbarMain;