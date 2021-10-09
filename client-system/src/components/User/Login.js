import React from 'react';
import style from '../../theme/style';
import AccountCircle from '@mui/icons-material/AccountCircle';
import { Avatar, Button, Container, TextField, Typography } from '@mui/material';

const Login = () => {
    return (
        <Container maxWidth='xs'>
            <div style={style.papper}>
                <Avatar style={style.avatar}>
                    <AccountCircle style={style.icon} />
                </Avatar>
                <Typography component='h1' variant='h5'>
                    User Login
                </Typography>
                <form style={style.form}>
                    <TextField variant='outlined' label='Ener your Email' name='email' fullWidth margin='normal' />
                    <TextField variant='outlined' label='Ener your passsword' name='password' fullWidth margin='normal'/>
                    <Button type='submit' fullWidth variant='contained' color='primary' style={style.submit} >Login</Button>
                </form>
            </div>
        </Container>
    );
};

export default Login;