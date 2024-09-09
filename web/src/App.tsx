import React, { useEffect, useState } from 'react';
import logo from './MMC-Logo-40.png';
import './App.css';
import { Card, Container, Nav, Navbar } from 'react-bootstrap';
import { QuestionGroups } from './components/survey/survey-question-groups';
// import { NavbarMain } from './components/navbar/navbar';
import { Route, Routes } from 'react-router-dom';
import { SurveyPage } from './pages/survey-page/survey-page';
import { LayoutPage } from './pages/layout-page';
import { useDispatch, useSelector } from 'react-redux';
import { setBrowserSize } from './store/general/general-slice'
import { CookieLoginUser } from './store/login-modal/login-modal-actions';
import modalSlice from './store/login-modal/login-modal';
import { RootState } from './store/store';

export interface AppProps { }

export const App: React.FunctionComponent<AppProps> = props => {
  // const [navbarOpen, setNavbarOpen] = useState<boolean>(false)
  const dispatch: any = useDispatch();
  const IsAuthenticated: boolean = useSelector((state: RootState)=> state.loginModal.IsAuthenticated)

  useEffect(() => {
    dispatch(CookieLoginUser(fetch));
  },[]);

  useEffect(() => {
    const handleResize = () => {
      dispatch(setBrowserSize({ browserWidth : window.innerWidth, browserHeight: window.innerHeight }));
    }
    window.addEventListener('resize', handleResize);

    handleResize();

    return () => {
      window.removeEventListener('resize', handleResize);
    };
  }, [])

  return (
    <div className='fixScrollBarProblem'>
<Routes>
      <Route element={<LayoutPage />}>
        <Route path='*' element={<SurveyPage />} />
      </Route>
      <Route path='VerifyMailPage' element={<div>Thank you for registering, you can now log it.</div>}/>
      {/* <Route path='Test' element={<div style={{ width: '100%', height: '100vh' }}>
      <iframe
        src="https://majamayo.com/"
        style={{ width: '100%', height: '100%', border: 'none' }}
        title="Embedded Page"
      />
    </div>} /> */}
    </Routes>
    </div>
    
  );
}

