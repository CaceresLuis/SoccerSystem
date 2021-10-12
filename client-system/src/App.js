import './App.css';
import theme from './theme/theme'
//import Login from './components/User/Login';
//import MyProfile from './components/MyProfile';
 import AddUser from './components/User/AddUser';
import { ThemeProvider } from '@emotion/react';


function App() {
  return (
    <div className="App">
      <ThemeProvider theme={theme}>
        <h1>Soccer System</h1>
        <AddUser/>
        {/* <Login/> */}
        {/* <MyProfile/> */}
      </ThemeProvider>
    </div>
  );
}

export default App;