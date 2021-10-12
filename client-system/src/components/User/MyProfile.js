import { Button, Container, Grid, Typography } from '@mui/material';
import React from 'react';
import style from '../../theme/style';

const MyProfile = () => {
    return (
        <Container component='main' maxWidth='md' justify="center">
            <div style={style.papper}>
                <Typography component='h1' variant='h5' >
                    My Profile
                </Typography>
            </div>
            <Grid container spacing={2}>
                <Grid item xs={12} md={6}>
                    <label>Your email</label>
                </Grid>
                <Grid item xs={12} md={6}>
                <label>Your Fullname</label>
                </Grid>
                <Grid item xs={12} md={6}>
                    <label>Your document</label>
                </Grid>
                <Grid item xs={12} md={6}>
                    <label>Your address</label>
                </Grid>
                <Grid item xs={12} md={12}>
                <Button type='submit' fullWidth variant='contained' size='large' color='primary' style={style.submit} >Update</Button>
                </Grid>
            </Grid>
        </Container>
    );
};

export default MyProfile;