import React from 'react';
import style from '../../theme/style';
import AccountCircle from '@mui/icons-material/AccountCircle';
import { Avatar, Container, Typography } from '@mui/material';

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
            </div>
        </Container>
    );
};

export default Login;