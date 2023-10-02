import { FC, ReactElement } from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import userManager, { loadUser, signinRedirect, signoutRedirect } from './auth/user-service';
import AuthProvider from './auth/auth-provider';
import SignInOidc from './auth/SigninOidc';
import SignOutOidc from './auth/SignoutOidc';
import HabitList from './habits/HabitList';      

const App: FC<{}> = (): ReactElement => {
    loadUser();
    return (
        <div className="App">
            <header className="App-header"> 
                <div className = "header">   
                    <div className = "header__body">
                        <nav className = "header__menu">
                                <ul className = "header__list">
                                    <li>
                                        <a onClick={() => signinRedirect()} className = "header__link">  
                                            Login
                                        </a>
                                    </li>
                                    <li>
                                        <a onClick={() => signoutRedirect()}className = "header__link">
                                            Logout
                                        </a>
                                    </li>
                                </ul>
                        </nav>                        
                    </div> 
                </div>
                <AuthProvider userManager={userManager}>
                    <Router>    
                        <Routes>
                            <Route path="/" element={<HabitList/>} />
                            <Route path="/signout-oidc"element={<SignOutOidc/>}/>
                            <Route path="/signin-oidc" element={<SignInOidc/>} />
                        </Routes>
                    </Router>
                </AuthProvider>
            </header> 
        </div>
    );
};

export default App;